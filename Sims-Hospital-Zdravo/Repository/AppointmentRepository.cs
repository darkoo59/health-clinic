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
        public DataHandler.AppointmentDataHandler appointmentDataHandler;


        public AppointmentRepository(AppointmentDataHandler appDataHandler)
        {
            this.appointmentDataHandler = appDataHandler;
            this.appointments = appDataHandler.ReadAll();
            Console.WriteLine(this);
            Console.WriteLine("Id doktora je " + appointments[0]._Doctor._Id);
        }
        public void Create(Model.Appointment appointment)
        {
            appointments.Add(appointment);
        }

        public void Update(Model.Appointment appointment)
        {
            // TODO: implement
        }

        public void Delete(Model.Appointment appointment)
        {
            // TODO: implement
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
            // TODO: implement
            return null;
        }

        public ObservableCollection<Appointment> FindAll()
        {
            // TODO: implement
            return null;
        }


    

        //

        /// <pdGenerated>default getter</pdGenerated>
        
        


    }
}