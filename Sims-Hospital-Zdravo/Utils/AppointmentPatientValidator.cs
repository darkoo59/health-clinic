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
                    if (app._DateAndTime.Year % 4 == 0) 
                    {
                        if (app._DateAndTime.Year % 100 == 0)
                        {
                            if (app._DateAndTime.Year % 400 == 0)
                            {
                                j = 366;
                            }
                        }
                        else j = 366;
                    }
                    if (appointment._DateAndTime.Year == app._DateAndTime.Year)
                    {
                        if (appointment._DateAndTime.DayOfYear - app._DateAndTime.DayOfYear > 2)
                        {
                            throw new Exception("You cant move appointment more than 2 days from original day");
                        }
                    }
                    else if (appointment._DateAndTime.Year - app._DateAndTime.Year == 1)
                    {
                        i = appointment._DateAndTime.DayOfYear;
                        i = i + j;
                        if (i - app._DateAndTime.DayOfYear > 2)
                        {
                            throw new Exception("You cant move appointment more than 2 days from original day");
                        }
                    }
                    else
                    {
                        throw new Exception("You cant move appointment more than 2 days from original day");
                    }
                    DateTime dateTime = DateTime.Now;
                    if (dateTime.Year == app._DateAndTime.Year)
                    {
                        if (app._DateAndTime.DayOfYear - dateTime.DayOfYear < 1)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app._DateAndTime.DayOfYear - dateTime.DayOfYear == 1 && app._DateAndTime.Hour < dateTime.Hour)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app._DateAndTime.DayOfYear - dateTime.DayOfYear == 1 && app._DateAndTime.Hour == dateTime.Hour)
                        {
                            if (((app._DateAndTime.Minute * 60) + app._DateAndTime.Second) < ((dateTime.Minute * 60) + dateTime.Second))
                            {
                                throw new Exception("You cant change appointment less than 24 hours before appointment");
                            }
                        }
                    }
                    else if (dateTime.Year - app._DateAndTime.Year == 1 && dateTime.DayOfYear == j && app._DateAndTime.DayOfYear == 1)
                    {
                        if (app._DateAndTime.Hour < dateTime.Hour)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app._DateAndTime.Hour < dateTime.Hour)
                        {
                            if (((app._DateAndTime.Minute * 60) + app._DateAndTime.Second) < ((dateTime.Minute * 60) + dateTime.Second))
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
