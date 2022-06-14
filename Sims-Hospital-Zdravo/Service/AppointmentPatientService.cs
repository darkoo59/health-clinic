/***********************************************************************
 * Module:  AppointmentPatientService.cs
 * Author:  I
 * Purpose: Definition of the Class Service.AppointmentPatientService
 ***********************************************************************/

using Model;
using Repository;
using Sims_Hospital_Zdravo;
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
        private AccountRepository accountRepository;

        public AppointmentPatientService(AccountRepository accountRepository)
        {
            this.appointmentRepository = new AppointmentRepository();
            this.doctorRepository = new DoctorRepository();
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
        public List<Appointment> FindByPatientIdOld(int id)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment app in FindByPatientId(id))
            {
                if (app.Time.End.CompareTo(DateTime.Now) < 0) appointments.Add(app);
            }
            return appointments;
        }
        public List<Appointment> FindByPatientIdNew(int id)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment app in FindByPatientId(id))
            {
                if (app.Time.End.CompareTo(DateTime.Now) > 0) appointments.Add(app);
            }
            return appointments;
        }
        public List<Appointment> FindByPatientId(int id)
        {
            return appointmentRepository.FindByPatientId(id);
        }

        public List<Doctor> ReadDoctors()
        {
            return doctorRepository.FindAll();
        }

        public List<Appointment> FindAll()
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
        public List<Appointment> InitializeList(Appointment appointment, string priority)
        {
            if (CheckIfFree(appointment))
            {
                List<Appointment> appointments = new List<Appointment>();
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
        private List<Appointment> GetAppointmentsByDoctorPriority(Appointment appointment)
        {
            List<DateTime> dates = AddingTimes(appointment);
            List<Appointment> appointments = new List<Appointment>();
            DeleteBusyTimes(appointment, dates);
            foreach (DateTime d in dates)
            {
                Appointment app = new Appointment(null, appointment.Doctor, appointment.Patient, new TimeInterval(d, d.AddMinutes(30)), AppointmentType.EXAMINATION);
                appointments.Add(app);
            }
            return appointments;
        }
        private List<Appointment> GetAppointmentsByDatePriority(Appointment appointment)
        {
            List<Doctor> doctors = AddingDoctors();
            List<Appointment> appointments = new List<Appointment>();
            DeleteBusyDoctors(appointment, doctors);
            foreach (Doctor d in doctors)
            {
                Appointment app = new Appointment(null, d, appointment.Patient, appointment.Time, AppointmentType.EXAMINATION);
                appointments.Add(app);
            }
            return appointments;
        }
        private List<Doctor> AddingDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
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
        private List<DateTime> AddingTimes(Appointment appointment)
        {
            List<DateTime> dates = new List<DateTime>();
            for (int i = 8; i < 12; i++)
            {
                DateTime dt = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
                dates.Add(dt.AddHours(i));
                DateTime dt2 = new DateTime(appointment.Time.Start.Year, appointment.Time.Start.Month, appointment.Time.Start.Day);
                dates.Add(dt2.AddHours(i).AddMinutes(30));
            }
            return dates;
        }
        private void DeleteBusyTimes(Appointment appointment, List<DateTime> dateTimes)
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
        private void DeleteBusyDoctors(Appointment appointment,List<Doctor> doctors)
        {
            foreach (Appointment app in FindAll())
            {
                if (app.CheckIfTimeIntervalInAppointment(appointment.Time))
                {
                    DeleteDoctorById(appointment.Doctor.Id, doctors);
                }
            }
        }
        private void DeleteDoctorById(int id, List<Doctor> doctors)
        {
            foreach (Doctor doctor in doctors)
            {
                if (doctor.Id == id)
                {
                    doctors.Remove(doctor);
                    break;
                }
            }
        }
        public void SetAppointmentRated(Appointment appointment) 
        {
            appointmentRepository.SetAppointmentRated(appointment);
        }

        public AppointmentRepository appointmentRepository;
        public DoctorRepository doctorRepository;

        public int GenerateId()
        {
            List<Appointment> appointments = appointmentRepository.FindAll();
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