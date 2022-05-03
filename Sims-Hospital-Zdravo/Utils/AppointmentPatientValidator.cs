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
        public void ValidateAppointment(Appointment appointment) 
        {
            Console.WriteLine(appointment._Time.Start.Hour);
            if (appointment._Time.Start.Hour < 8 || appointment._Time.Start.Hour > 11) 
            {
                throw new Exception("Hospital works between 8h and 12h");
            }
            if (appointment._Time.Start.Minute != 0 && appointment._Time.Start.Minute != 30)
            {
                throw new Exception("Minutes must be 0 or 30");
            }
            DateTime now = DateTime.Now;
            if (now.CompareTo(appointment._Time.Start) > 0) 
            {
                throw new Exception("You can not time travel");
            }
        }
        public void RescheduleAppointment(Appointment appointment, TimeInterval timeInterval) 
        {
            if (timeInterval.Start.Hour < 8 || timeInterval.Start.Hour > 11)
            {
                throw new Exception("Hospital works between 8h and 12h");
            }
            if (timeInterval.Start.Minute != 0 && timeInterval.Start.Minute != 30)
            {
                throw new Exception("Minutes must be 0 or 30");
            }
            DateTime now = DateTime.Now;
            if (now.CompareTo(timeInterval.Start) > 0)
            {
                throw new Exception("You can not time travel");
            }
            int i;
            int j = 365;
            int z = 365;
            if (timeInterval.Start.Year % 4 == 0)
            {
                if (timeInterval.Start.Year % 100 == 0)
                {
                    if (timeInterval.Start.Year % 400 == 0)
                    {
                        z = 366;
                    }
                }
                else z = 366;
            }
            foreach (Appointment app in appointmentRepository.FindByPatientId(1)) 
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
                    if (timeInterval.Start.Year == app._Time.Start.Year)
                    {
                        if (timeInterval.Start.DayOfYear - app._Time.Start.DayOfYear > 2 || app._Time.Start.DayOfYear - timeInterval.Start.DayOfYear > 2)
                        {
                            throw new Exception("You cant move appointment more than 2 days from original day");
                        }
                    }
                    else if (timeInterval.Start.Year - app._Time.Start.Year == 1)
                    {
                        i = timeInterval.Start.DayOfYear;
                        i = i + j;
                        if (i - app._Time.Start.DayOfYear > 2)
                        {
                            throw new Exception("You cant move appointment more than 2 days from original day");
                        }
                    }
                    else if (app._Time.Start.Year - timeInterval.Start.Year == 1)
                    {
                        i = app._Time.Start.DayOfYear;
                        i = i + z;
                        if (i - timeInterval.Start.DayOfYear > 2)
                        {
                            throw new Exception("You cant move appointment more than 2 days from original day");
                        }
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
                else
                {
                    if (timeInterval.Start.CompareTo(app._Time.Start) == 0)
                    {
                        if (appointment._Doctor._Id == app._Doctor._Id)
                        {
                            if (appointment._Patient._Id != app._Patient._Id)
                            {
                                throw new Exception("Appointment already exists");
                            }
                        }
                    }
                }
            }
        }
    }
}
