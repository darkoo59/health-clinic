using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Controller;
using Model;
using Repository;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Messaging;

namespace Sims_Hospital_Zdravo.Utils
{
    class TaskScheduleTimer : INotificationObservable
    {
        private List<INotificationObserver> observers;
        public static Timer timer;
        private EquipmentTransferController _relocationController;
        private RenovationController _renovationController;
        private PrescriptionController _prescriptionController;
        private DoctorAppointmentController _doctorAppointmentController;
        private NotificationController _notificationController;
        private SuppliesController _suppliesController;
        private AccountController _accountController;
        private bool isRenovationAppointmentInProgress;
        private bool isRelocationAppointmentInProgress;
        private bool isManagerNotificationInProgress;

        public TaskScheduleTimer(EquipmentTransferController relocationController, RenovationController renovationController, DoctorAppointmentController doctorAppointmentController,
            PrescriptionController prescriptionController, NotificationController notificationController, SuppliesController suppliesController, AccountController accountController)
        {
            _relocationController = relocationController;
            _renovationController = renovationController;
            _prescriptionController = prescriptionController;
            _notificationController = notificationController;
            _doctorAppointmentController = doctorAppointmentController;
            _suppliesController = suppliesController;
            _accountController = accountController;
            isRenovationAppointmentInProgress = false;

            foreach (Prescription prescription in _prescriptionController.ReadAll())
            {
                prescription.Flag = true;
            }

            observers = new List<INotificationObserver>();
            SetTimer();
        }


        private void SetTimer()
        {
            timer = new Timer(2000);
            timer.Elapsed += FireScheduledTask;
            timer.AutoReset = true;
            timer.Enabled = true;
        }


        private void FireScheduledTask(Object source, ElapsedEventArgs e)
        {
            CheckIfRelocationAppointmentDone();
            CheckIfRenovationAppointmentDone();
            CheckIfSuppliesAcquisitionDone();
            CheckIfThereShouldBeNotification();
            CheckNotificationForManager();
            CheckNotificationForDoctor();
            CheckNotificationForSecretary();
            //AppointmentDone();
        }

        private void CheckIfRelocationAppointmentDone()
        {
            List<RelocationAppointment> appointments = _relocationController.FindAll();
            foreach (RelocationAppointment app in appointments.ToList())
            {
                if (app.Scheduled.End.CompareTo(DateTime.Now) < 0)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate { _relocationController.FinishRelocationAppointment(app.Id); });
                }
            }
        }

        private void CheckIfRenovationAppointmentDone()
        {
            List<RenovationAppointment> renovations = new List<RenovationAppointment>(_renovationController.FindAll());
            foreach (RenovationAppointment renovation in renovations)
            {
                if (renovation.Time.End.Date.CompareTo(DateTime.Now.Date) <= 0 && !isRenovationAppointmentInProgress)
                {
                    isRenovationAppointmentInProgress = true;
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        _renovationController.FinishRenovationAppointment(renovation.Id);
                        isRenovationAppointmentInProgress = false;
                    });
                }
            }
        }

        private void CheckIfSuppliesAcquisitionDone()
        {
            ObservableCollection<SuppliesAcquisition> suppliesAcquisitions =
                new ObservableCollection<SuppliesAcquisition>(_suppliesController.ReadAllSuppliesAcquisitions());
            foreach (SuppliesAcquisition acquisition in suppliesAcquisitions)
            {
                if (acquisition.Time.End.Date.CompareTo(DateTime.Now.Date) <= 0 && acquisition.Time.End.TimeOfDay.CompareTo(DateTime.Now.TimeOfDay) <= 0 && acquisition.AcquistionDone == false)
                {
                    acquisition.AcquistionDone = true;
                    _suppliesController.Update(acquisition);
                    _suppliesController.FinishSuppliesAcquisition(acquisition.Id);
                }
            }
        }


        private void CheckNotificationForManager()
        {
            User account = _accountController.GetLoggedAccount();
            if (account == null) return;
            if (!account._Role.Equals(RoleType.MANAGER)) return;

            List<Notification> notifications = _notificationController.ReadAllManagerMedicineNotifications();
            foreach (Notification notification in notifications)
            {
                Notify(notification);
            }

            List<Notification> meetingNotifications = _notificationController.ReadAllManagerMeetingsNotifications(account._Id);
            foreach (Notification notification in meetingNotifications)
            {
                Notify(notification);
            }
        }

        public void CheckNotificationForDoctor()
        {
            User account = _accountController.GetLoggedAccount();
            if (account == null) return;
            if (!account._Role.Equals(RoleType.DOCTOR)) return;

            List<Notification> notifications = _notificationController.ReadAllDoctorMedicineNotifications(account._Id);
            foreach (Notification notification in notifications)
            {
                Notify(notification);
            }

            List<Notification> meetingNotifications = _notificationController.ReadAllDoctorMeetingsNotifications(account._Id);
            foreach (Notification notification in meetingNotifications)
            {
                Notify(notification);
            }

            List<Notification> freeDaysNotifications = _notificationController.ReadAllDoctorFreeDaysNotifications(account._Id);
            foreach (Notification notification in freeDaysNotifications)
            {
                Notify(notification);
            }
        }

        public void CheckNotificationForSecretary()
        {
            User account = _accountController.GetLoggedAccount();
            if (account == null) return;
            if (!account._Role.Equals(RoleType.SECRETARY)) return;

            List<Notification> meetingNotifications = _notificationController.ReadAllSecretaryMeetingsNotifications(account._Id);
            foreach (Notification notification in meetingNotifications)
            {
                Notify(notification);
            }
        }

        public void AppointmentDone()
        {
            ObservableCollection<Appointment> appointments = _doctorAppointmentController.GetByDoctorID(2);
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Time.Start.CompareTo(DateTime.Now) < 0)
                {
                    _doctorAppointmentController.DeleteByID(appointment);
                }
            }
        }

        private void CheckIfThereShouldBeNotification()
        {
            DateTime dateTime = DateTime.Now;
            DateTime dateTime1 = DateTime.Now.AddSeconds(10);
            ObservableCollection<Prescription> prescriptions = _prescriptionController.ReadAll();
            foreach (Prescription prescription in prescriptions)
            {
                int frequency = GetFrequency(prescription);
                DateTime dt = GetDateTime(prescription);
                for (int i = 0; i < GetQuantity(prescription); i++)
                {
                    if (dt.CompareTo(dateTime) > 0 && dt.CompareTo(dateTime1) < 0)
                    {
                        if (prescription.Flag)
                        {
                            prescription.Flag = false;
                            Notify(new Notification("You need to take " + prescription.Medicine.Name + ".", _notificationController.GenerateId()));
                        }
                    }
                    else
                    {
                        prescription.Flag = true;
                        dt = dt.AddHours(frequency);
                    }
                }
            }
        }

        public int GetFrequency(Prescription prescription)
        {
            string[] s = prescription.Dosage.Split('x');
            return Int32.Parse(s[1]) * 24 / Int32.Parse(s[0]);
        }

        public DateTime GetDateTime(Prescription prescription)
        {
            DateTime dt = new DateTime(prescription._PrescriptionDate.Year, prescription._PrescriptionDate.Month, prescription._PrescriptionDate.Day);
            dt = dt.AddHours(prescription._PrescriptionDate.Hour);
            dt = dt.AddMinutes(prescription._PrescriptionDate.Minute);
            return dt;
        }

        public int GetQuantity(Prescription prescription)
        {
            string[] s = prescription.Dosage.Split('x');
            int p = prescription.TimeInterval.End.DayOfYear - prescription.TimeInterval.Start.DayOfYear;
            p = p * Int32.Parse(s[0]);
            return p;
        }

        public void AddObserver(INotificationObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(INotificationObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(Notification notification)
        {
            foreach (INotificationObserver observer in observers)
            {
                observer.Notify(notification);
            }
        }
    }
}