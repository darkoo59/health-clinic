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

namespace Repository
{
    public class AppointmentRepository
    {
        public ObservableCollection<Appointment> appointments;
        public AppointmentDataHandler appointmentDataHandler;



        public AppointmentRepository(AppointmentDataHandler appDataHandler)
        {
            this.appointmentDataHandler = appDataHandler;
            this.appointments = appDataHandler.ReadAll();
            //Console.WriteLine(this);
            //Console.WriteLine("Id doktora je " + appointments[0]._Doctor._Id);
        }
        public void Create(Model.Appointment appointment)
        {
            appointments.Add(appointment);
            loadDataToFile();
        }

        public void Update(Model.Appointment appointment)
        {
            //Console.WriteLine(appointment._DateAndTime.ToString());
            foreach (Appointment app in appointments)
            {
                if (app._Id == appointment._Id)
                {
                    app._DateAndTime = appointment._DateAndTime;
                    app._Doctor = appointment._Doctor;
                    app._Patient = appointment._Patient;
                    app._Room = appointment._Room;
                    loadDataToFile();
                    return;
                }

            }
        }

        public void Delete(Model.Appointment appointment)
        {
            appointments.Remove(appointment);
            loadDataToFile();
        }

        public ObservableCollection<Appointment> FindByDoctorId(int id)
        {
            // TODO: implement
            ObservableCollection<Appointment> doctorsApps = new ObservableCollection<Appointment>();
            Console.WriteLine(this.appointments.Count + " duzina");
            foreach(Appointment app in this.appointments)
            {
                if(app._Doctor._Id == id)
                {
                    doctorsApps.Add(app);
                }
            }
            return doctorsApps;
        }
       

        public ObservableCollection<Appointment> FindByPatientId(int id)
        {
            ObservableCollection<Appointment> patientApps = new ObservableCollection<Appointment>();
            foreach (Appointment app in this.appointments)
            {
                if (app._Patient._Id == id)
                {
                    patientApps.Add(app);
                }
            }
            return patientApps;
        }

        public ObservableCollection<Appointment> FindAll()
        {
            // TODO: implement
            return null;
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

        

        


    }
}