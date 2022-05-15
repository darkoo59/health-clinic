using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class EmergencyReschedule
    {
        private Appointment _appointment;
        private TimeInterval _rescheduledDate;

        public EmergencyReschedule(Appointment app, TimeInterval interval)
        {
            _appointment = app;
            _rescheduledDate = interval;
        }

        public Appointment Appointment
        {
            get { return _appointment; }
            set { _appointment = value; }
        }

        public TimeInterval RescheduledDate
        {
            get { return _rescheduledDate; }
            set { _rescheduledDate = value; }
        }
    }
}
