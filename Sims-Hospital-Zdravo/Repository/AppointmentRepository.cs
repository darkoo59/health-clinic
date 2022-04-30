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
            Console.WriteLine("laalalalalal");
            Console.WriteLine(appointment._Id);
            //Console.WriteLine(appointment._DateAndTime.ToString());
            foreach (Appointment app in appointments)
            {
                Console.WriteLine(app._Id+"hahaha");
                if (app._Id == appointment._Id)
                {
                    
                    app._Time = appointment._Time;
                    app._Doctor = appointment._Doctor;
                    app._Patient = appointment._Patient;
                    app._Room = appointment._Room;
                    app._Type = appointment._Type;
                    Console.WriteLine(app._Patient._Name+"hshhsh");
                    loadDataToFile();
                    return;
                }
            }
            foreach (Appointment app in patientApps)
            {
                if (app._Id == appointment._Id)
                {
                    app._DateAndTime = appointment._DateAndTime;
                    app._Doctor = appointment._Doctor;
                    app._Patient = appointment._Patient;
                    app._Room = appointment._Room;
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
            // TODO: implement
            ObservableCollection<Appointment> doctorsApps = new ObservableCollection<Appointment>();
            Console.WriteLine(this.appointments.Count + " duzina");
            foreach(Appointment app in this.appointments)
            {
                if (app._Doctor == null) continue;
                if(app._Doctor._Id == id)
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
        public  ref ObservableCollection<Appointment> FindAll()
        {
            // TODO: implement
            return  ref appointments;
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
            foreach(Appointment app in appointments)
            {
                if(app._Room._Id == room._Id)
                {
                    timeIntervals.Add(new TimeInterval(app._DateAndTime, app._DateAndTime.AddMinutes(30)));
                }
            }

            return timeIntervals;
        }

        public List<Appointment> FindByRoomId(int roomId)
        {
            List<Appointment> apps = new List<Appointment>();
            foreach (Appointment app in appointments)
            {
                if (app._Room._Id == roomId)
                {
                    apps.Add(app);
                }
            }

            return apps;
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