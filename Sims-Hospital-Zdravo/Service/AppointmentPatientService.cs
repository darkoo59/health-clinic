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
            if (CheckIfFree(appointment))
            {
                ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
                appointments.Add(appointment);
                return appointments;
            }
            else if (priority.Equals("Date"))
            {
                return GetAppointmentsByDatePriority(appointment);
            }
            else
            {
                return GetAppointmentsByDoctorPriority(appointment);
            }
        }
        private ObservableCollection<Appointment> GetAppointmentsByDoctorPriority(Appointment appointment)
        {
            ObservableCollection<DateTime> dates = AddingTimes(appointment);
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            DeleteBusyTimes(appointment, dates);
            foreach (DateTime d in dates)
            {
                Appointment app = new Appointment(null, appointment.Doctor, appointment.Patient, new TimeInterval(d, d.AddMinutes(30)), AppointmentType.EXAMINATION);
                appointments.Add(app);
            }
            return appointments;
        }
        private ObservableCollection<Appointment> GetAppointmentsByDatePriority(Appointment appointment)
        {
            ObservableCollection<Doctor> doctors = AddingDoctors();
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            DeleteBusyDoctors(appointment, doctors);
            foreach (Doctor d in doctors)
            {
                Appointment app = new Appointment(null, d, appointment.Patient, appointment.Time, AppointmentType.EXAMINATION);
                appointments.Add(app);
            }
            return appointments;
        }
        private ObservableCollection<Doctor> AddingDoctors() 
        {
            ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>();
            foreach (Doctor d in ReadDoctors())
            {
                doctors.Add(d);
            }
            return doctors;
        }
        private bool CheckIfFree(Appointment appointment)
        {
            foreach (Appointment app in appointmentRepository.FindAll())
            {
                if (appointment.Doctor.Id == app.Doctor.Id)
                {
                    if (app.CheckIfTimeIntervalInAppointment(appointment.Time))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private ObservableCollection<DateTime> AddingTimes(Appointment appointment)
        {
            ObservableCollection<DateTime> dates = new ObservableCollection<DateTime>();
            for (int i = 8; i < 12; i++)
            {
                DateTime dt = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
                dates.Add(dt.AddHours(i));
                DateTime dt2 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
                dates.Add(dt2.AddHours(i).AddMinutes(30));
            }
            return dates;
        }
        private void DeleteBusyTimes(Appointment appointment, ObservableCollection<DateTime> dateTimes)
        {
            foreach (Appointment app in FindAll())
            {
                foreach (DateTime dateTime in AddingTimes(appointment))
                {
                    if (app.CheckIfTimeIntervalInAppointment(new TimeInterval(dateTime, dateTime.AddMinutes(30))))
                    {
                        dateTimes.Remove(dateTime);
                    }
                }
            }
        }
        private void DeleteBusyDoctors(Appointment appointment,ObservableCollection<Doctor> doctors)
        {
            foreach (Appointment app in FindAll())
            {
                if (app.CheckIfTimeIntervalInAppointment(appointment.Time))
                {
                    doctors.Remove(app.Doctor);
                }
            }
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