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
        private NotesController _notesController;
        private bool isRenovationAppointmentInProgress;
        private bool isRelocationAppointmentInProgress;
        private bool isManagerNotificationInProgress;

        public TaskScheduleTimer(EquipmentTransferController relocationController, RenovationController renovationController, DoctorAppointmentController doctorAppointmentController,
            MedicalRecordController medicalRecordController, SuppliesController suppliesController, AccountController accountController)
        {
            _notesController = new NotesController();
            _relocationController = relocationController;
            _renovationController = renovationController;
            _medicalRecordController = medicalRecordController;
            _notificationController = notificationController;
            _doctorAppointmentController = doctorAppointmentController;
            _suppliesController = suppliesController;
            _accountController = accountController;
            isRenovationAppointmentInProgress = false;
            isRelocationAppointmentInProgress = false;
            isManagerNotificationInProgress = false;

            observers = new List<INotificationObserver>();
            SetTimer();
        }


        private void SetTimer()
        {
            timer = new Timer(10000);
            timer.Elapsed += FireScheduledTask;
            timer.AutoReset = true;
            timer.Enabled = true;
        }


        private void FireScheduledTask(Object source, ElapsedEventArgs e)
        {
            CheckIfRelocationAppointmentDone();
            CheckIfRenovationAppointmentDone();
            CheckIfSuppliesAcquisitionDone();
           // if(_accountController.GetLoggedAccount() != null)CheckIfThereShouldBeNotification();
            CheckNotificationForManager();
            //CheckNotificationForDoctor();
            CheckNotificationForSecretary();
            if (_accountController.GetLoggedAccount() != null) CheckNotesNotification();
            //AppointmentDone();
        }
        private void CheckNotesNotification()
        {
            foreach (Notes note in _notesController.FindAll())
            {
                DateTime dateTime = note.Reminder;
                if (dateTime.CompareTo(DateTime.Now) > 0 && dateTime.CompareTo(DateTime.Now.AddSeconds(10)) < 0)
                {
                    if (note.Flag)
                    {
                        _notesController.SetFlag(note);
                        Notify(new Notification(note.Text, _notificationController.GenerateId()));
                    }
                }
            }
        }

        private void CheckIfRelocationAppointmentDone()
        {
            List<RelocationAppointment> appointments = _relocationController.FindAll();
            foreach (RelocationAppointment app in appointments.ToList())
            {
                if (app.Scheduled.End.CompareTo(DateTime.Now) < 0 && !isRelocationAppointmentInProgress)
                {
                    isRelocationAppointmentInProgress = true;
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        _relocationController.FinishRelocationAppointment(app.Id);
                        isRelocationAppointmentInProgress = false;
                    });
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
            foreach (var notification in notifications.Where(notification => !isManagerNotificationInProgress))
            {
                isManagerNotificationInProgress = true;
                App.Current.Dispatcher.Invoke(delegate
                {
                    Notify(notification);
                    isManagerNotificationInProgress = false;
                });
            }

            CheckForManagerMeeting(account);
        }

        public void CheckForManagerMeeting(User account)
        {
            List<Notification> meetingNotifications = _notificationController.ReadAllManagerMeetingsNotifications(account.Id);
            foreach (Notification notification in meetingNotifications)
            {
                if (!isManagerNotificationInProgress)
                {
                    isManagerNotificationInProgress = true;
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        Notify(notification);
                        isManagerNotificationInProgress = false;
                    });
                }
            }
        }

        public void CheckNotificationForDoctor()
        {
            User account = _accountController.GetLoggedAccount();
            if (account == null) return;
            if (!account._Role.Equals(RoleType.DOCTOR)) return;

            List<Notification> notifications = _notificationController.ReadAllDoctorMedicineNotifications(account.Id);
            Console.WriteLine(notifications.Count);
            foreach (Notification notification in notifications)
            {
                Notify(notification);
            }

            List<Notification> meetingNotifications = _notificationController.ReadAllDoctorMeetingsNotifications(account.Id);
            Console.WriteLine(meetingNotifications.Count);

            foreach (Notification notification in meetingNotifications)
            {
                Console.WriteLine("lalalalallallalal");
                Notify(notification);
            }

            List<Notification> freeDaysNotifications = _notificationController.ReadAllDoctorFreeDaysNotifications(account.Id);
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

            List<Notification> meetingNotifications = _notificationController.ReadAllSecretaryMeetingsNotifications(account.Id);
            foreach (Notification notification in meetingNotifications)
            {
                Notify(notification);
            }
        }

        public void CheckNotificationForDoctorRequests()
        {
            User account = _accountController.GetLoggedAccount();
            if (account == null) return;
            if (!account._Role.Equals(RoleType.DOCTOR)) return;
            //List <Notification> notifications = 
        }

        public void AppointmentDone()
        {
            List<Appointment> appointments = _doctorAppointmentController.GetByDoctorID(2);
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
                    if (!CheckIfPrescriptionDateTimeIsNow(dateTime, prescription))
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
                    if (medicalRecord.Patient.Id == _accountController.GetLoggedAccount().Id) return medicalRecord;
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