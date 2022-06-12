/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.AppointmentRepository
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataHandler;
using System.Linq;
using Sims_Hospital_Zdravo;
using Sims_Hospital_Zdravo.Interfaces;

namespace Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private List<Appointment> appointments;
        private AppointmentDataHandler appointmentDataHandler;

        public AppointmentRepository()
        {
            this.appointmentDataHandler = new AppointmentDataHandler();
            LoadDataFromFile();
        }

        public void Create(Model.Appointment appointment)
        {
            appointment.Id = GenerateId();
            appointments.Add(appointment);
            LoadDataToFile();
        }

        public void Update(Model.Appointment appointment)
        {
            LoadDataFromFile();
            foreach (Appointment app in appointments)
            {
                if (app.Id == appointment.Id)
                {
                    app.Time = appointment.Time;
                    app.Doctor = appointment.Doctor;
                    app.Patient = appointment.Patient;
                    app.Room = appointment.Room;
                    app.Type = appointment.Type;

                    LoadDataToFile();
                }
            }
        }

        public void DeleteById(int id)
        {
            LoadDataFromFile();
            foreach (var appointment in appointments.Where(appointment => appointment.Id == id))
            {
                appointments.Remove(appointment);
                LoadDataToFile();
                return;
            }
        }

        public void Delete(Appointment appointment)
        {
            LoadDataFromFile();
            DeleteById(appointment.Id);
            LoadDataToFile();
        }

        public List<Appointment> FindByDoctorId(int id)
        {
            LoadDataFromFile();
            List<Appointment> doctorsApps = new List<Appointment>();
            foreach (Appointment app in this.appointments)
            {
                if (app.Doctor == null) continue;
                if (app.Doctor.Id == id)
                {
                    doctorsApps.Add(app);
                }
            }

            var doctorsapps = doctorsApps.OrderBy(i => i.Time.Start.Date).ToList();
            doctorsApps = new List<Appointment>(doctorsapps);
            return doctorsApps;
        }

        public List<Appointment> FindByDoctorSpecialityBeforeDate(SpecialtyType type, DateTime endDate)
        {
            LoadDataFromFile();
            List<Appointment> doctorsApps = new List<Appointment>();
            foreach (Appointment app in this.appointments)
            {
                if (app.Doctor == null) continue;
                if (app.Doctor.Specialty == type && app.Time.Start < endDate)
                {
                    doctorsApps.Add(app);
                }
            }

            return doctorsApps;
        }

        public List<Appointment> FindByPatientId(int id)
        {
            LoadDataFromFile();
            List<Appointment> patientApps = new List<Appointment>();
            foreach (Appointment app in this.appointments)
            {
                if (app.Patient.Id == id)
                {
                    patientApps.Add(app);
                }
            }

            return patientApps;
        }

        public List<Appointment> FindAll()
        {
            LoadDataFromFile();
            return appointments;
        }

        public void LoadDataFromFile()
        {
            appointments = appointmentDataHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            appointmentDataHandler.Write(appointments);
        }

        public Appointment GetByID(Appointment app)
        {
            LoadDataFromFile();
            foreach (Appointment appoi in appointments)
            {
                if (appoi.Id == app.Id)
                {
                    return appoi;
                }
            }
            return null;
        }


        public List<TimeInterval> GetTimeIntervalsForRoom(Room room)
        {
            LoadDataFromFile();
            if (room == null) return new List<TimeInterval>();
            return (from app in appointments where app.Room.Id == room.Id select new TimeInterval(app.Time.Start, app.Time.End)).ToList();
        }

        public List<TimeInterval> GetTimeIntervalsForRoom(int roomId)
        {
            LoadDataFromFile();
            //return (from app in appointments where app.Room.Id == roomId select new TimeInterval(app.Time.Start, app.Time.End)).ToList();
            return (from app in appointments where app.Room?.Id == roomId select new TimeInterval(app.Time.Start, app.Time.End)).ToList();
        }

        public List<TimeInterval> GetTimeIntervalsForDoctor(int id)
        {
            LoadDataFromFile();
            List<TimeInterval> timeIntervals = new List<TimeInterval>();
            foreach (Appointment app in appointments)
            {
                if (app.Doctor.Id == id)
                {
                    timeIntervals.Add(new TimeInterval(app.Time.Start, app.Time.End));
                }
            }

            return timeIntervals;
        }

        public List<Appointment> FindByRoomId(int roomId)
        {
            LoadDataFromFile();
            List<Appointment> apps = new List<Appointment>();
            foreach (Appointment app in appointments)
            {
                if (app.Room.Id == roomId)
                {
                    apps.Add(app);
                }
            }

            return apps;
        }

        public List<TimeInterval> GetTimeIntervalsForDoctor(Doctor doctor)
        {
            LoadDataFromFile();
            List<TimeInterval> timeIntervals = new List<TimeInterval>();
            List<Appointment> appointments = FindByDoctorId(doctor.Id);
            foreach (Appointment app in appointments)
            {
                timeIntervals.Add(app.Time);
            }

            return timeIntervals;
        }

        public List<Appointment> ReadAllAppointmentsForDate(DateTime date)
        {
            LoadDataFromFile();
            List<Appointment> appointmentsForDate = new List<Appointment>();
            foreach (Appointment app in FindAll())
            {
                if (app.Time.Start.Date == date.Date)
                    appointmentsForDate.Add(app);
            }

            return appointmentsForDate;
        }
        public void SetAppointmentRated(Appointment appointment)
        {
            LoadDataFromFile();
            foreach (Appointment app in appointments)
            {
                if (app.Id == appointment.Id)
                {
                    app.Rated = true;
                    LoadDataToFile();
                    return;
                }
            }
        }
        
        public int GenerateId()
        {
            int id = 0;
            List<int> ids = appointments.Select(appointment => appointment.Id).ToList();
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }
    }
}