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
        public List<Patient> patients;
        public PatientDataHandler patientDataHandler;

        public AppointmentRepository(AppointmentDataHandler appDataHandler)
        {
            this.appointmentDataHandler = appDataHandler;
            this.appointments = appDataHandler.ReadAll();
        }
        protected void Create(Model.Appointment appointment)
        {
            // TODO: implement
            appointments.Add(appointment);
            //appointmentDataHandler.Write(appointments);
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
            Console.WriteLine("xmsmsjss");
            foreach(Appointment app in appointments)
            {
                Console.WriteLine(app._Doctor._Name);
                Console.WriteLine(app._Doctor._Surname);
                Console.WriteLine("dnsdsjnd");

            }
            Console.WriteLine("prazna lista");

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

        //

        /// <pdGenerated>default getter</pdGenerated>

        public ref List<Patient> GetPatients()
        {
            return ref patients;
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

        public DataHandler.AppointmentDataHandler appointmentDataHandler;
        


    }
}