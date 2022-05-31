/***********************************************************************
 * Module:  AppointmentPatientService.cs
 * Author:  I
 * Purpose: Definition of the Class Service.AppointmentPatientService
 ***********************************************************************/

using Model;
using Repository;
using Sims_Hospital_Zdravo.Repository;
using Sims_Hospital_Zdravo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Service
{
    public class AppointmentPatientService
    {
        private AppointmentPatientValidator appointmentPatientValidator;
        public AccountRepository accountRepository;

        public AppointmentPatientService(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository, AccountRepository accountRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
            this.appointmentPatientValidator = new AppointmentPatientValidator(appointmentRepository);
            this.accountRepository = accountRepository;
        }

        public void Create(Appointment appointment)
        {
            appointment.Id = GenerateId();
            appointmentRepository.Create(appointment);
        }


        public void Update(Appointment appointment)
        {
            CheckIfPatientNotBlocked();
            appointmentRepository.Update(appointment);
        }

        public void Delete(Appointment appointment)
        {
            CheckIfPatientNotBlocked();
            appointmentRepository.Delete(appointment);
        }
        public ObservableCollection<Appointment> FindByPatientIdOld(int id)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (Appointment app in FindByPatientId(id)) 
            {
                if (app.Time.End.CompareTo(DateTime.Now) < 0) appointments.Add(app);
            }
            return appointments;
        }
        public ObservableCollection<Appointment> FindByPatientIdNew(int id)
        {
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (Appointment app in FindByPatientId(id))
            {
                if (app.Time.End.CompareTo(DateTime.Now) > 0) appointments.Add(app);
            }
            return appointments;
        }
        public ref ObservableCollection<Appointment> FindByPatientId(int id)
        {
            return ref appointmentRepository.FindByPatientId(id);
        }

        public ObservableCollection<Doctor> ReadDoctors()
        {
            return doctorRepository.ReadAll();
        }

        public ObservableCollection<Appointment> FindAll()
        {
            return appointmentRepository.FindAll();
        }

        public void ValidateAppointment(Appointment appointment)
        {
            appointmentPatientValidator.ValidateAppointment(appointment);
        }

        public void ValidateReschedule(Appointment appointment, TimeInterval timeInterval)
        {
            appointmentPatientValidator.RescheduleAppointment(appointment, timeInterval);
        }
        public void CheckIfPatientNotBlocked()
        {
            appointmentPatientValidator.CheckIfPatientNotBlocked(accountRepository);
        }
        public ObservableCollection<Appointment> InitializeList(Appointment appointment, string priority)
        {
            bool free = true;
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            ObservableCollection<DateTime> dates = AddingTimes(appointment);
            ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>();
            foreach (Doctor d in ReadDoctors())
            {
                doctors.Add(d);
            }

            if (priority.Equals("Date"))
            {
                foreach (Appointment app in FindAll())
                {
                    if (app.Doctor._Id == appointment.Doctor._Id)
                    {
                        if (app.Time.Start.Year == appointment.Time.Start.Year && app.Time.Start.DayOfYear == appointment.Time.Start.DayOfYear)
                        {
                            if ((appointment.Time.Start.Hour * 60 + appointment.Time.Start.Minute >= app.Time.Start.Hour * 60 + app.Time.Start.Minute) &&
                                (appointment.Time.End.Hour * 60 + appointment.Time.End.Minute <= app.Time.End.Hour * 60 + app.Time.End.Minute))
                            {
                                free = false;
                            }
                        }
                    }

                    if (app.Time.Start.Year == appointment.Time.Start.Year && app.Time.Start.DayOfYear == appointment.Time.Start.DayOfYear)
                    {
                        if ((appointment.Time.Start.Hour * 60 + appointment.Time.Start.Minute >= app.Time.Start.Hour * 60 + app.Time.Start.Minute) &&
                            (appointment.Time.End.Hour * 60 + appointment.Time.End.Minute <= app.Time.End.Hour * 60 + app.Time.End.Minute))
                        {
                            foreach (Doctor d in ReadDoctors())
                            {
                                if (d._Id == app.Doctor._Id)
                                {
                                    doctors.Remove(d);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (priority.Equals("Doctor"))
            {
                foreach (Appointment app in FindAll())
                {
                    if (app.Doctor._Id == appointment.Doctor._Id)
                    {
                        if (app.Time.Start.Year == appointment.Time.Start.Year && app.Time.Start.DayOfYear == appointment.Time.Start.DayOfYear)
                        {
                            if ((appointment.Time.Start.Hour * 60 + appointment.Time.Start.Minute >= app.Time.Start.Hour * 60 + app.Time.Start.Minute) &&
                                (appointment.Time.End.Hour * 60 + appointment.Time.End.Minute <= app.Time.End.Hour * 60 + app.Time.End.Minute))
                            {
                                free = false;
                            }

                            dates.Remove(app.Time.Start);
                        }
                    }
                }
            }

            if (free)
            {
                appointments.Add(appointment);
            }
            else if (priority.Equals("Date"))
            {
                foreach (Doctor d in doctors)
                {
                    Appointment app = new Appointment(null, d, appointment.Patient, appointment.Time, AppointmentType.EXAMINATION);
                    appointments.Add(app);
                }
            }
            else
            {
                foreach (DateTime d in dates)
                {
                    TimeInterval ti = new TimeInterval(d, d.AddMinutes(30));
                    Appointment app = new Appointment(null, appointment.Doctor, appointment.Patient, ti, AppointmentType.EXAMINATION);
                    appointments.Add(app);
                }
            }

            return appointments;
        }

        public ObservableCollection<DateTime> AddingTimes(Appointment appointment)
        {
            ObservableCollection<DateTime> dates = new ObservableCollection<DateTime>();
            DateTime dt = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dt = dt.AddHours(8);
            dates.Add(dt);
            DateTime dt4 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dt4 = dt4.AddHours(8);
            dt4 = dt4.AddMinutes(30);
            dates.Add(dt4);
            DateTime dt1 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dt1 = dt1.AddHours(9);
            dates.Add(dt1);
            DateTime dt5 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dt5 = dt5.AddHours(9);
            dt5 = dt5.AddMinutes(30);
            dates.Add(dt5);
            DateTime dt2 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dt2 = dt2.AddHours(10);
            dates.Add(dt2);
            DateTime dt6 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dt6 = dt6.AddHours(10);
            dt6 = dt6.AddMinutes(30);
            dates.Add(dt6);
            DateTime dt3 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dt3 = dt3.AddHours(11);
            dates.Add(dt3);
            DateTime dt7 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
            dt7 = dt7.AddHours(11);
            dt7 = dt7.AddMinutes(30);
            dates.Add(dt7);
            return dates;
        }

        public AppointmentRepository appointmentRepository;
        public DoctorRepository doctorRepository;

        public int GenerateId()
        {
            ObservableCollection<Appointment> appointments = appointmentRepository.FindAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Appointment app in appointments)
            {
                ids.Add(app.Id);
            }

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }
    }
}