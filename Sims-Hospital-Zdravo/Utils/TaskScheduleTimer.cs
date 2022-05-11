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
using Sims_Hospital_Zdravo.Service;

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
        private DateTime dateTime;
        private DateTime dateTime1;

        public TaskScheduleTimer(EquipmentTransferController relocationController, RenovationController renovationController, DoctorAppointmentController doctorAppointmentController,
            PrescriptionController prescriptionController, NotificationController notificationController)
        {
            this._relocationController = relocationController;
            this._renovationController = renovationController;
            this._prescriptionController = prescriptionController;
            this._notificationController = notificationController;
            this._doctorAppointmentController = doctorAppointmentController;

            foreach (Prescription prescription in _prescriptionController.ReadAll())
            {
                prescription._Flag = true;
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
            CheckIfThereShouldBeNotification();
            CheckNotificationForManager();
            AppointmentDone();
            dateTime = DateTime.Now;
            dateTime1 = DateTime.Now.AddSeconds(10);
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

        private void CheckNotificationForManager()
        {
            List<Notification> notifications = _notificationController.ReadAllManagerMedicineNotifications();
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
                if (appointment._Time.Start.CompareTo(DateTime.Now) < 0)
                {
                    _doctorAppointmentController.DeleteByID(appointment);
                }
            }
        }

        private void CheckIfThereShouldBeNotification()
        {
            ObservableCollection<Prescription> prescriptions = _prescriptionController.ReadAll();
            foreach (Prescription prescription in prescriptions)
            {
                string[] s = prescription._Dosage.Split('x');
                int frequency = Int32.Parse(s[1]) * 24 / Int32.Parse(s[0]);
                DateTime dt = new DateTime(prescription._PrescriptionDate.Year, prescription._PrescriptionDate.Month, prescription._PrescriptionDate.Day);
                dt = dt.AddHours(prescription._PrescriptionDate.Hour);
                dt = dt.AddMinutes(prescription._PrescriptionDate.Minute);
                Console.WriteLine(prescription._TimeInterval);
                int p = prescription._TimeInterval.End.DayOfYear - prescription._TimeInterval.Start.DayOfYear;
                p = p * Int32.Parse(s[0]);
                for (int i = 0; i < p; i++)
                {
                    if (dt.CompareTo(dateTime) > 0 && dt.CompareTo(dateTime1) < 0)
                    {
                        if (prescription._Flag)
                        {
                            prescription._Flag = false;
                            Notify(new Notification("You need to take " + prescription._Medicine._Name + ".", _notificationController.GenerateId()));
                        }
                    }
                    else
                    {
                        prescription._Flag = true;
                        dt = dt.AddHours(frequency);
                    }
                }
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