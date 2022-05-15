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

namespace Repository
{
    public class AppointmentRepository
    {
        private ObservableCollection<Appointment> appointments;
        private AppointmentDataHandler appointmentDataHandler;

        public AppointmentRepository(AppointmentDataHandler appDataHandler)
        {
            this.appointmentDataHandler = appDataHandler;
            this.patientApps = new ObservableCollection<Appointment>();
            this.appointments = appDataHandler.ReadAll();
        }

        public void Create(Model.Appointment appointment)
        {
            appointments.Add(appointment);
            patientApps.Add(appointment);
            loadDataToFile();
        }

        public void Update(Model.Appointment appointment)
        {
            foreach (Appointment app in appointments)
            {
                if (app._Id == appointment._Id)
                {
                    app._Time = appointment._Time;
                    app._Doctor = appointment._Doctor;
                    app._Patient = appointment._Patient;
                    app._Room = appointment._Room;
                    app._Type = appointment._Type;

                    loadDataToFile();
                }
            }
        }

        public void Delete(Model.Appointment appointment)
        {
            appointments.Remove(appointment);
            patientApps.Remove(appointment);
            loadDataToFile();
        }

        public ObservableCollection<Appointment> FindByDoctorId(int id)
        {
            ObservableCollection<Appointment> doctorsApps = new ObservableCollection<Appointment>();
            foreach (Appointment app in this.appointments)
            {
                if (app._Doctor == null) continue;
                if (app._Doctor._Id == id)
                {
                    doctorsApps.Add(app);
                }
            }

            var doctorsapps = doctorsApps.OrderBy(i => i._Time.Start.Date).ToList();
            doctorsApps = new ObservableCollection<Appointment>(doctorsapps);
            return doctorsApps;
        }

        public List<Appointment> FindByDoctorSpecialityBeforeDate(SpecialtyType type,DateTime endDate)
        {
            List<Appointment> doctorsApps = new List<Appointment>();
            foreach (Appointment app in this.appointments)
            {
                if (app._Doctor == null) continue;
                if (app._Doctor._Specialty == type && app._Time.Start < endDate)
                {
                    doctorsApps.Add(app);
                }
            }
            return doctorsApps;
            
        }

        ObservableCollection<Appointment> patientApps;

        public ref ObservableCollection<Appointment> FindByPatientId(int id)
        {
            patientApps = new ObservableCollection<Appointment>();
            foreach (Appointment app in this.appointments)
            {
                if (app._Patient._Id == id)
                {
                    patientApps.Add(app);
                }
            }

            return ref patientApps;
        }

        public ref ObservableCollection<Appointment> FindAll()
        {
            // TODO: implement
            return ref appointments;
        }

        public void loadDataFromFiles()
        {
            appointments = appointmentDataHandler.ReadAll();
        }

        public void loadDataToFile()
        {
            appointmentDataHandler.Write(appointments);
        }

        public Appointment GetByID(Appointment app)
        {
            // TODO: implement

            foreach (Appointment appoi in appointments)
            {
                if (appoi._Id == app._Id)
                {
                    return appoi;
                    ;
                }
            }

            return null;
        }


        public List<TimeInterval> GetTimeIntervalsForRoom(Room room)
        {
            if (room == null) return new List<TimeInterval>();
            List<TimeInterval> timeIntervals = new List<TimeInterval>();
            foreach (Appointment app in appointments)
            {
                if (app._Room.Id == room.Id)
                {
                    timeIntervals.Add(new TimeInterval(app._Time.Start, app._Time.End));
                }
            }

            return timeIntervals;
        }

        public List<TimeInterval> GetTimeIntervalsForDoctor(int id)
        {
            List<TimeInterval> timeIntervals = new List<TimeInterval>();
            foreach (Appointment app in appointments)
            {
                if (app._Doctor._Id == id)
                {
                    timeIntervals.Add(new TimeInterval(app._Time.Start, app._Time.End));
                }
            }
            foreach(TimeInterval timeInterval in timeIntervals)
            {
                Console.WriteLine(timeInterval.Start + "sshshhs" + timeInterval.End);
            }
            return timeIntervals;
        }

        public List<Appointment> FindByRoomId(int roomId)
        {
            List<Appointment> apps = new List<Appointment>();
            foreach (Appointment app in appointments)
            {
                if (app._Room.Id == roomId)
                {
                    apps.Add(app);
                }
            }

            return apps;
        }

        public List<TimeInterval> getTimeIntervalsForDoctor(Doctor doctor)
        {
            List<TimeInterval> timeIntervals = new List<TimeInterval>();
            ObservableCollection<Appointment> appointments = FindByDoctorId(doctor._Id);
            foreach(Appointment app in appointments)
            {
                timeIntervals.Add(app._Time);

            }
            return timeIntervals;
        }
        
        public ObservableCollection<Appointment> ReadAllAppointmentsForDate(DateTime date)
        {
            ObservableCollection<Appointment> appointmentsForDate = new ObservableCollection<Appointment>();
            foreach (Appointment app in FindAll())
            {
                if (app._Time.Start.Date == date.Date)
                    appointmentsForDate.Add(app);
            }

            return appointmentsForDate;
        }
    }
}