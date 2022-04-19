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

        public AppointmentRepository(AppointmentDataHandler appDataHandler)
        {
            this.appointmentDataHandler = appDataHandler;
            appointments = appDataHandler.ReadAll();
        }
        protected void Create(Model.Appointment appointment)
        {
            // TODO: implement
            appointments.Add(appointment);
            appointmentDataHandler.Write(appointments);
        }

        protected void Update(Model.Appointment appointment)
        {
            // TODO: implement
        }

        protected void Delete(Model.Appointment appointment)
        {
            // TODO: implement
        }

        protected ObservableCollection<Appointment> FindByDoctorId(int id)
        {
            // TODO: implement
            ObservableCollection<Appointment> doctorsApps = new ObservableCollection<Appointment>();

            foreach(Appointment app in appointments)
            {
                if(app._Doctor._Id == id)
                {
                    doctorsApps.Add(app);
                }
            }

            return doctorsApps;
        }

        protected System.Collections.Generic.List<Appointment> FindByPatientId(int id)
        {
            // TODO: implement
            return null;
        }

        protected System.Collections.Generic.List<Appointment> FindAll()
        {
            // TODO: implement
            return null;
        }

        public ObservableCollection<Appointment> appointments;

        /// <pdGenerated>default getter</pdGenerated>
        
        public DataHandler.AppointmentDataHandler appointmentDataHandler;
        //public ObservableCollection<Appointment> appointments;


    }
}