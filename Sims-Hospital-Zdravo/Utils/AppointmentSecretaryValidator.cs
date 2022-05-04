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
        SecretaryAppointmentService appointmentService;
        public AppointmentSecretaryValidator(SecretaryAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
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

        public void DoctorAlreadyHaveAppointment(Appointment appointment)
        {
            if(!appointmentService.IsDoctorFreeInIntervalWithoutSelectedAppointment(appointment._Doctor._Id,appointment))
                throw new Exception("Doctor isn't available at selected time!");
        }

        public void PatientAlreadyHaveAppointment(Appointment appointment)
        {
            if(!appointmentService.IsPatientFreeInIntervalWithoutSelectedAppointment(appointment._Patient._Id,appointment))
                throw new Exception("Patient isn't available at selected time!");
        }
        
        public void ValidateCreate(Appointment appointment)
        {
            SchedulingAppointmentInWrongTime(appointment._Time);
        }

        public void ValidateRescheduling(Appointment appointment)
        {
            SchedulingAppointmentInWrongTime(appointment._Time);
            DoctorAlreadyHaveAppointment(appointment);
            PatientAlreadyHaveAppointment(appointment);
        }
    }
}
