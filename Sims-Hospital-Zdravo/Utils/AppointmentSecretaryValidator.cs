using Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Utils
{
    public class AppointmentSecretaryValidator
    {

        public AppointmentSecretaryValidator()
        {
        }



        public void SchedulingAppointmentInWrongTime(TimeInterval interval)
        {
            if (interval.Start < DateTime.Now)
            {
                throw new Exception("You can't make appointment because date is from past!");
            }
            if(interval.Start > interval.End)
            {
                throw new Exception("Appointment need to start before it's ending!");
            }
        }
        public void ValidateCreate(Appointment appointment)
        {
            SchedulingAppointmentInWrongTime(appointment._Time);
        }

        public void ValidateRescheduling(Appointment appointment)
        {
            SchedulingAppointmentInWrongTime(appointment._Time);
        }
    }
}
