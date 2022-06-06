using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class TherapySchedule
    {
        private string _time;
        private string _monday;
        private string _tuesday;
        private string _wednesday;
        private string _thursday;
        private string _friday;
        private string _saturday;        
        private string _sunday;
        public TherapySchedule()
        { 
        
        }
        public TherapySchedule(string Time, string Monday, string Tuesday, string Wednesday, string Thursday, string Friday, string Saturday, string Sunday) 
        {
            this.Time = Time;
            this.Monday = Monday;
            this.Tuesday = Tuesday;
            this.Wednesday = Wednesday;
            this.Thursday = Thursday;
            this.Friday = Friday;
            this.Saturday = Saturday;
            this.Sunday = Sunday;
        }
        public string Time { get { return _time; } set { _time = value; } }
        public string Monday { get { return _monday; } set { _monday = value; } }
        public string Tuesday { get { return _tuesday; } set { _tuesday = value; } }
        public string Wednesday { get { return _wednesday; } set { _wednesday = value; } }
        public string Thursday { get { return _thursday; } set { _thursday = value; } }
        public string Friday { get { return _friday; } set { _friday = value; } }
        public string Saturday { get { return _saturday; } set { _saturday = value; } }
        public string Sunday { get { return _sunday; } set { _sunday = value; } }
    }
}
