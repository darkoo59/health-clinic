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

namespace Sims_Hospital_Zdravo.Utils
{
    class TaskScheduleTimer : INotificationObservable
    {
        private List<INotificationObserver> observers;
        public static Timer timer;
        private EquipmentTransferController _relocationController;
        private RenovationController _renovationController;
        private PrescriptionController _prescriptionController;
        private DateTime dateTime;
        private DateTime dateTime1;

        public TaskScheduleTimer(EquipmentTransferController relocationController, RenovationController renovationController, PrescriptionController prescriptionController)
        {
            this._relocationController = relocationController;
            this._renovationController = renovationController;
            this._prescriptionController = prescriptionController;
            foreach (Prescription prescription in prescriptionController.ReadAll())
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
            //CheckIfRelocationAppointmentDone();
            //CheckIfRenovationAppointmentDone();
            CheckIfThereShouldBeNotification();
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
            List<RenovationAppointment> renovations = _renovationController.ReadAll();
            foreach (RenovationAppointment renovation in renovations)
            {
                if (renovation.Time.End.Date.CompareTo(DateTime.Now.Date) < 0)
                {
                    _renovationController.FinishRenovationAppointment(renovation.Id);
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
                for (int i = 0; i < prescription._NumberOfDays; i++)
                {
                    if (dt.CompareTo(dateTime) > 0 && dt.CompareTo(dateTime1) < 0)
                    {
                        if (prescription._Flag)
                        {
                            prescription._Flag = false;
                            Notify("You need to take " + prescription._Medicine._Name + ".");
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

        public void Notify(string description)
        {
            foreach (INotificationObserver observer in observers)
            {
                observer.Notify(description);
            }
        }
    }
}