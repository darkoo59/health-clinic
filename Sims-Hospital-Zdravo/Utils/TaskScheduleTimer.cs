using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Timers;
using Controller;
using Model;
using Repository;
namespace Sims_Hospital_Zdravo.Utils
{
    class TaskScheduleTimer
    {

        public static Timer timer;
        private EquipmentTransferController relocationController;
        public TaskScheduleTimer(EquipmentTransferController relocationController)
        {
            this.relocationController = relocationController;
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
            Console.WriteLine("Running check");
            ObservableCollection<RelocationAppointment> appointments = relocationController.ReadAll(); 
            foreach(RelocationAppointment app in appointments.ToList())
            {
                Console.WriteLine(app._Scheduled.End.CompareTo(DateTime.Now));
                Console.WriteLine(app._Scheduled.End);
                Console.WriteLine(DateTime.Now);
                if(app._Scheduled.End.CompareTo(DateTime.Now) < 0)
                {
                    relocationController.FinishRelocationAppointment(app._Id);
                }
            }
        }


    }
}
