using Model;
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
        public AppointmentPatientValidator(AppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        public void ValidateAppointment(Appointment appointment) 
        {
            CheckIfHospitalWorks(appointment.Time);
            CheckIfMinutesAreGood(appointment.Time);
            CheckIfTimeIsInPast(appointment.Time);
            CheckIfAppointmentExists(appointment);
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
            CheckIfTimeIntervalInAvailableTime(appointment, timeInterval);
            CheckIfTimeIntervalInOneDayBeforeAppointment(appointment);
        }
        private void CheckIfTimeIntervalInOneDayBeforeAppointment(Appointment appointment)
        {
            DateTime dateTime = GetAppointmentTime(appointment);
            dateTime = dateTime.AddDays(-1);
            if (dateTime.CompareTo(DateTime.Now) < 0)
            {
                throw new Exception("You cant change appointment less than 24 hours before appointment");
            }
        }
        private void CheckIfTimeIntervalInAvailableTime(Appointment appointment, TimeInterval timeInterval)
        {
            DateTime startInterval = GetAppointmentTime(appointment).AddDays(-3);
            DateTime endInterval = GetAppointmentTime(appointment).AddDays(3);
            if (timeInterval.Start.CompareTo(startInterval) < 0 || timeInterval.Start.CompareTo(endInterval) > 0)
            {
                throw new Exception("You cant move appointment more or less than 2 days from original day");
            }
        }
        private DateTime GetAppointmentTime(Appointment appointment)
        {
            DateTime dateTime = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dateTime = dateTime.AddHours(appointment.Time.Start.Hour).AddMinutes(appointment.Time.Start.Minute);
            return dateTime;
        }
        private void CheckIfAppointmentExists(Appointment appointment)
        {
            foreach (Appointment app in appointmentRepository.FindAll())
            {
                if (appointment.Doctor._Id == app.Doctor._Id)
                {
                    if (app.CheckIfTimeIntervalInAppointment(appointment.Time))
                    {
                        throw new Exception("Appointment already exists");
                    }
                }
            }
        }
        private void CheckIfRescheduledAppointmentExists(Appointment appointment, TimeInterval timeInterval)
        {
            foreach (Appointment app in appointmentRepository.FindAll())
            {
                if (appointment.Id != app.Id)
                {
                    if (appointment.Doctor._Id == app.Doctor._Id)
                    {
                        if (app.CheckIfTimeIntervalInAppointment(timeInterval))
                        {
                            throw new Exception("Appointment already exists");
                        }                    
                    }
                }
            }
        }
        private void CheckIfHospitalWorks(TimeInterval timeInterval)
        {
            if (timeInterval.Start.Hour < 8 || timeInterval.Start.Hour > 11)
            {
                throw new Exception("Hospital works between 8h and 12h");
            }
        }
        private void CheckIfMinutesAreGood(TimeInterval timeInterval)
        {
            if (timeInterval.Start.Minute != 0 && timeInterval.Start.Minute != 30)
            {
                throw new Exception("Minutes must be 0 or 30");
            }
        }
        private void CheckIfTimeIsInPast(TimeInterval timeInterval)
        {
            DateTime now = DateTime.Now;
            if (now.CompareTo(timeInterval.Start) > 0)
            {
                throw new Exception("You can not time travel");
            }
        }
    }
}
