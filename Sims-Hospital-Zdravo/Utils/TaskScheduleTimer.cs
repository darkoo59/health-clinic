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
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Utils
{
    class TaskScheduleTimer
    {
        public static Timer timer;
        private EquipmentTransferController relocationController;
        private RenovationController renovationController;

        public TaskScheduleTimer(EquipmentTransferController relocationController, RenovationController renovationController)
        {
            this.relocationController = relocationController;
            this.renovationController = renovationController;
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
        }

        private void CheckIfRelocationAppointmentDone()
        {
            List<RelocationAppointment> appointments = relocationController.ReadAll();
            foreach (RelocationAppointment app in appointments.ToList())
            {
                if (app._Scheduled.End.CompareTo(DateTime.Now) < 0)
                {
                    relocationController.FinishRelocationAppointment(app._Id);
                }
            }
        }

        private void CheckIfRenovationAppointmentDone()
        {
            List<RenovationAppointment> renovations = renovationController.ReadAll();
            foreach (RenovationAppointment renovation in renovations)
            {
                if (renovation._Time.End.Date.CompareTo(DateTime.Now.Date) < 0)
                {
                    renovationController.FinishRenovationAppointment(renovation._Id);
                }
            }
        }
    }
}