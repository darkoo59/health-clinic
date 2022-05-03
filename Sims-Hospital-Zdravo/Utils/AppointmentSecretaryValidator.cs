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

        /*public void IsDoctorAvailableForRescheduling(Appointment appointment)
        {
            if(!appointmentService.FindAvailableDoctorsForInterval(appointment._Time).Contains(appointment._Doctor))
                throw new Exception("Doctor isn't available at selected time!");
        }

        public void isPatientAvailableForRescheduling(Appointment appointment)
        {
            if (!appointmentService.FindAvailablePatientsForInterval(appointment._Time).Contains(appointment._Patient))
                throw new Exception("Patient isn't available at selected time!");
        }

        public void isRoomAvailableForRescheduling(Appointment appointment)
        {
            if (!appointmentService.FindAvailableRoomsForInterval(appointment._Time).Contains(appointment._Room))
                throw new Exception("Room isn't available at selected time!");
        }*/
        public void ValidateCreate(Appointment appointment)
        {
            SchedulingAppointmentInWrongTime(appointment._Time);
        }

        public void ValidateRescheduling(Appointment appointment)
        {
            SchedulingAppointmentInWrongTime(appointment._Time);
           // IsDoctorAvailableForRescheduling(appointment);
           // isPatientAvailableForRescheduling(appointment);
           // isRoomAvailableForRescheduling(appointment);
        }
    }
}
