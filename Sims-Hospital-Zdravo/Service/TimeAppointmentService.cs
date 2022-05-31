using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Collections.ObjectModel;
using Model;

namespace Sims_Hospital_Zdravo.Service
{
    public class TimeAppointmentService
    {
        private AppointmentRepository appointmentRepository;
        public TimeAppointmentService(AppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public Appointment findAppointmentByDate(DateTime date,int id)
        {
            foreach(Appointment app in appointmentRepository.FindByDoctorId(id))
            {
                if (app.Time.Start.Date.Equals(date.Date))
                {
                    if(date.CompareTo(app.Time.Start.Date) >= 0)
                    {
                        return app;
                    }
                }
            }
            return null;

        }





    }
}
