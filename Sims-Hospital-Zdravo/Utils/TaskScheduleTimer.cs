using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
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

        public TaskScheduleTimer(EquipmentTransferController relocationController, RenovationController renovationController)
        {
            this._relocationController = relocationController;
            this._renovationController = renovationController;
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
            Notify("Ovo je poruka za notifikacije!");
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