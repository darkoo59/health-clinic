using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Utils
{
    class AppointmentPatientValidator
    {
        private AppointmentRepository appointmentRepository;
        public AppointmentPatientValidator(AppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        public void rescheduleAppointment(Appointment appointment) 
        {
            int i;
            int j = 365; 
            foreach(Appointment app in appointmentRepository.FindByPatientId(1)) 
            {
                if (app._Id == appointment._Id) 
                {
                    if (app._Time.Start.Year % 4 == 0) 
                    {
                        if (app._Time.Start.Year % 100 == 0)
                        {
                            if (app._Time.Start.Year % 400 == 0)
                            {
                                j = 366;
                            }
                        }
                        else j = 366;
                    }
                    if (appointment._Time.Start.Year == app._Time.Start.Year)
                    {
                        if (appointment._Time.Start.DayOfYear - app._Time.Start.DayOfYear > 2)
                        {
                            throw new Exception("You cant move appointment more than 2 days from original day");
                        }
                    }
                    else if (appointment._Time.Start.Year - app._Time.Start.Year == 1)
                    {
                        i = appointment._Time.Start.DayOfYear;
                        i = i + j;
                        if (i - app._Time.Start.DayOfYear > 2)
                        {
                            throw new Exception("You cant move appointment more than 2 days from original day");
                        }
                    }
                    else
                    {
                        throw new Exception("You cant move appointment more than 2 days from original day");
                    }
                    DateTime dateTime = DateTime.Now;
                    if (dateTime.Year == app._Time.Start.Year)
                    {
                        if (app._Time.Start.DayOfYear - dateTime.DayOfYear < 1)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app._Time.Start.DayOfYear - dateTime.DayOfYear == 1 && app._Time.Start.Hour < dateTime.Hour)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app._Time.Start.DayOfYear - dateTime.DayOfYear == 1 && app._Time.Start.Hour == dateTime.Hour)
                        {
                            if (((app._Time.Start.Minute * 60) + app._Time.Start.Second) < ((dateTime.Minute * 60) + dateTime.Second))
                            {
                                throw new Exception("You cant change appointment less than 24 hours before appointment");
                            }
                        }
                    }
                    else if (dateTime.Year - app._Time.Start.Year == 1 && dateTime.DayOfYear == j && app._Time.Start.DayOfYear == 1)
                    {
                        if (app._Time.Start.Hour < dateTime.Hour)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app._Time.Start.Hour < dateTime.Hour)
                        {
                            if (((app._Time.Start.Minute * 60) + app._Time.Start.Second) < ((dateTime.Minute * 60) + dateTime.Second))
                            {
                                throw new Exception("You cant change appointment less than 24 hours before appointment");
                            }
                        }
                    }
                }   
            }
        }
    }
}
