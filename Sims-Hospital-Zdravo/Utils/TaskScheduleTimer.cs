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
        private MedicalRecordController _medicalRecordController;
        private DoctorAppointmentController _doctorAppointmentController;
        private NotificationController _notificationController;
        private SuppliesController _suppliesController;
        private AccountController _accountController;

        public TaskScheduleTimer(EquipmentTransferController relocationController, RenovationController renovationController, DoctorAppointmentController doctorAppointmentController,
            MedicalRecordController medicalRecordController, NotificationController notificationController, SuppliesController suppliesController, AccountController accountController)
        {
            this._relocationController = relocationController;
            this._renovationController = renovationController;
            this._medicalRecordController = medicalRecordController;
            this._notificationController = notificationController;
            this._doctorAppointmentController = doctorAppointmentController;
            this._suppliesController = suppliesController;
            this._accountController = accountController;

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
            if(_accountController.GetLoggedAccount() != null)CheckIfThereShouldBeNotification();
            CheckNotificationForManager();
            CheckNotificationForDoctor();
            //AppointmentDone();
        }

        private void CheckIfRelocationAppointmentDone()
        {
            List<RelocationAppointment> appointments = _relocationController.ReadAll();
            foreach (RelocationAppointment app in appointments.ToList())
            {
                if (app.Scheduled.End.CompareTo(DateTime.Now) < 0)
                {
                    _relocationController.FinishRelocationAppointment(app.Id);
                }
            }
        }

        private void CheckIfRenovationAppointmentDone()
        {
            ObservableCollection<RenovationAppointment> renovations = new ObservableCollection<RenovationAppointment>(_renovationController.ReadAll());
            foreach (RenovationAppointment renovation in renovations)
            {
                if (renovation.Time.End.Date.CompareTo(DateTime.Now.Date) <= 0)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate { _renovationController.FinishRenovationAppointment(renovation.Id); });
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
            foreach (Prescription prescription in _medicalRecordController.GetPrescriptionsByMedicalRecord(FindMedicalRecordByAccount()))
            {
                DateTime dateTime = prescription.GetDateTime();
                for (int i = 0; i < prescription.GetQuantity(); i++)
                {
                    if (!CheckIfPrescriptionDateTimeIsNow(dateTime,prescription))
                    {
                        dateTime = dateTime.AddHours(prescription.GetFrequency());
                    }
                }
            }
        }
        private MedicalRecord FindMedicalRecordByAccount()
        {
            foreach (MedicalRecord medicalRecord in _medicalRecordController.ReadAll())
            {
                if (_accountController.GetLoggedAccount() != null)
                {
                    if (medicalRecord.Patient._Id == _accountController.GetLoggedAccount()._Id) return medicalRecord;
                }
            }
            return null;
        }
        private bool CheckIfPrescriptionDateTimeIsNow(DateTime dateTime, Prescription prescription)
        {
            if (dateTime.CompareTo(DateTime.Now) > 0 && dateTime.CompareTo(DateTime.Now.AddSeconds(10)) < 0)
            {
                CheckIfMedicineAlreadyTaken(prescription);
                return true;
            }
            else
            {
                prescription.Flag = true;
                return false;
            }
        }
        private void CheckIfMedicineAlreadyTaken(Prescription prescription)
        {
            if (prescription.Flag)
            {
                prescription.Flag = false;
                Notify(new Notification("You need to take " + prescription.Medicine.Name + ".", _notificationController.GenerateId()));
            }
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