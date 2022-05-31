﻿using Model;
using Repository;
using Service;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sims_Hospital_Zdravo.Utils
{
    public class AppointmentPatientValidator
    {
        private AppointmentRepository appointmentRepository;
        private int i;
        private int j = 365;
        private int z = 365;
        public AppointmentPatientValidator(AppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        public void ValidateAppointment(Appointment appointment) 
        {
            Console.WriteLine(appointment.Time.Start.Hour);
            if (appointment.Time.Start.Hour < 8 || appointment.Time.Start.Hour > 11) 
            {
                throw new Exception("Hospital works between 8h and 12h");
            }
            if (appointment.Time.Start.Minute != 0 && appointment.Time.Start.Minute != 30)
            {
                throw new Exception("Minutes must be 0 or 30");
            }
            DateTime now = DateTime.Now;
            if (now.CompareTo(appointment.Time.Start) > 0) 
            {
                throw new Exception("You can not time travel");
            }
        }
        public void CheckIfPatientNotBlocked(AccountRepository accountRepository)
        {
            List<DateTime> cancels = accountRepository.GetLoggedAccount().Cancels;
            if (cancels.Count > 3)
            {
                if (TooManyInThirtyDays(cancels))
                {
                    accountRepository.BlockLoggedAccount();
                    throw new Exception("You are blocked");
                }
            }
            accountRepository.GetLoggedAccount().Cancels.Add(DateTime.Now);
        }
        private bool TooManyInThirtyDays(List<DateTime> cancels)
        {
            return (DateTime.Now.DayOfYear - cancels.ElementAt(cancels.Count - 4).DayOfYear < 30);
        }
        public void RescheduleAppointment(Appointment appointment, TimeInterval timeInterval) 
        {
            CheckIfHospitalWorks(timeInterval);
            CheckIfMinutesAreGood(timeInterval);
            CheckIfTimeIsInPast(timeInterval);
            SetTimeIntervalIfLeapYear(timeInterval);
            foreach (Appointment app in appointmentRepository.FindByPatientId(1)) 
            {
                if (app.Id == appointment.Id)
                {
                    SetAppointmentIfLeapYear(app);
                    if (CheckIfYearIsRight(timeInterval, app) || CheckIf1(timeInterval, app) ||CheckIf2(timeInterval, app))
                    {
                        throw new Exception("You cant move appointment more than 2 days from original day");
                    }
                    DateTime dateTime = DateTime.Now;
                    if (dateTime.Year == app.Time.Start.Year)
                    {
                        if (app.Time.Start.DayOfYear - dateTime.DayOfYear < 1)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app.Time.Start.DayOfYear - dateTime.DayOfYear == 1 && app.Time.Start.Hour < dateTime.Hour)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app.Time.Start.DayOfYear - dateTime.DayOfYear == 1 && app.Time.Start.Hour == dateTime.Hour)
                        {
                            if (((app.Time.Start.Minute * 60) + app.Time.Start.Second) < ((dateTime.Minute * 60) + dateTime.Second))
                            {
                                throw new Exception("You cant change appointment less than 24 hours before appointment");
                            }
                        }
                    }
                    else if (dateTime.Year - app.Time.Start.Year == 1 && dateTime.DayOfYear == j && app.Time.Start.DayOfYear == 1)
                    {
                        if (app.Time.Start.Hour < dateTime.Hour)
                        {
                            throw new Exception("You cant change appointment less than 24 hours before appointment");
                        }
                        else if (app.Time.Start.Hour < dateTime.Hour)
                        {
                            if (((app.Time.Start.Minute * 60) + app.Time.Start.Second) < ((dateTime.Minute * 60) + dateTime.Second))
                            {
                                throw new Exception("You cant change appointment less than 24 hours before appointment");
                            }
                        }
                    }
                }
                else
                {
                    if (timeInterval.Start.CompareTo(app.Time.Start) == 0)
                    {
                        CheckIfAppointmentExists(appointment, app);
                    }
                }
            }
        }
        public bool CheckIfYearIsRight(TimeInterval timeInterval,Appointment app)
        {
            if (timeInterval.Start.Year == app.Time.Start.Year)
            {
                if (timeInterval.Start.DayOfYear - app.Time.Start.DayOfYear > 2 || app.Time.Start.DayOfYear - timeInterval.Start.DayOfYear > 2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckIf1(TimeInterval timeInterval, Appointment app)
        {
            if (timeInterval.Start.Year - app.Time.Start.Year == 1)
            {
                i = timeInterval.Start.DayOfYear;
                i = i + j;
                if (i - app.Time.Start.DayOfYear > 2)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckIf2(TimeInterval timeInterval, Appointment app)
        {
            if (app.Time.Start.Year - timeInterval.Start.Year == 1)
            {
                i = app.Time.Start.DayOfYear;
                i = i + z;
                if (i - timeInterval.Start.DayOfYear > 2)
                {
                    return true;
                }
            }
            return false;
        }
        public void CheckIfAppointmentExists(Appointment appointment, Appointment app)
        {
                if (appointment.Doctor._Id == app.Doctor._Id)
                {
                    if (appointment.Patient._Id != app.Patient._Id)
                    {
                        throw new Exception("Appointment already exists");
                    }
                }
        }
        public void CheckIfHospitalWorks(TimeInterval timeInterval)
        {
            if (timeInterval.Start.Hour < 8 || timeInterval.Start.Hour > 11)
            {
                throw new Exception("Hospital works between 8h and 12h");
            }
        }
        public void CheckIfMinutesAreGood(TimeInterval timeInterval)
        {
            if (timeInterval.Start.Minute != 0 && timeInterval.Start.Minute != 30)
            {
                throw new Exception("Minutes must be 0 or 30");
            }
        }
        public void CheckIfTimeIsInPast(TimeInterval timeInterval)
        {
            DateTime now = DateTime.Now;
            if (now.CompareTo(timeInterval.Start) > 0)
            {
                throw new Exception("You can not time travel");
            }
        }
        public void SetTimeIntervalIfLeapYear(TimeInterval timeInterval)
        {
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
        }
        public void SetAppointmentIfLeapYear(Appointment app)
        {
            if (app.Time.Start.Year % 4 == 0)
            {
                if (app.Time.Start.Year % 100 == 0)
                {
                    if (app.Time.Start.Year % 400 == 0)
                    {
                        j = 366;
                    }
                }
                else j = 366;
            }
        }
    }
}
