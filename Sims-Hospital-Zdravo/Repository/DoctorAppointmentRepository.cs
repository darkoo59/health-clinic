/***********************************************************************
 * Module:  DoctorAppointmentRepository.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Repository.DoctorAppointmentRepository
 ***********************************************************************/

using DataHandler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Repository
{
   public class DoctorAppointmentRepository : AppointmentRepository
   {
        public AppointmentDataHandler appDataHandler;
       //public PatientDataHandler patientHandler;
        public ObservableCollection<Appointment> appointments;
        public List<Patient> patients;
        public PatientDataHandler patientDataHandler;
        //private object appointmentDataHandler1;
        //private RoomDataHandler roomDataHandler;

        public DoctorAppointmentRepository( PatientDataHandler patientData, AppointmentDataHandler appointmentDataHandler)
        {
            appDataHandler = appointmentDataHandler;
            appointments = new ObservableCollection<Appointment>();
            patients = new List<Patient>();

        }

       

        public new void Create(Appointment appointment)
      {
            // TODO: implement
            AddAppointment(appointment);


      }
      
      public Boolean DeleteByID(Appointment appointment)
      {
         // TODO: implement
         foreach(  Appointment app in appointments)
            {
                if (app._Id == appointment._Id)
                {
                    RemoveAppointment(app);
                    return true;
                }
            }
         return false;
      }
      
      public new void Update(Appointment appointment)
      {
         // TODO: implement
         foreach(Appointment app in appointments)
            {
                if(app._Id == appointment._Id)
                {
                    app._DateAndTime = appointment._DateAndTime;
                    app._Doctor = appointment._Doctor;
                    app._Patient = appointment._Patient;
                    app._Room = appointment._Room;
                }

            }
         

      }
      
      public ObservableCollection<Appointment> GetAllByDoctorID(int id)
      {
            // TODO: implement
             ObservableCollection<Appointment> backup = new ObservableCollection<Appointment>();
            foreach(Appointment appointment in appointments)
            {
                if(appointment._Doctor._DoctorID == id)
                {
                    backup.Add(appointment);
                }
            }
            if (backup.Count == 0)

                return null;
            else
                return backup;
      }
        

        public Appointment GetByID(Appointment app)
      {
         // TODO: implement
         
         foreach(Appointment appoi in appointments)
            {
                if (appoi._Id == app._Id)
                {
                   
                    return appoi;
;
                }
            }
         
         return null;
      }

        public ref ObservableCollection<Appointment> ReadAll()
        {
            return ref appointments;
        }
    
        

        public List<Patient> GetPatients()
        {
            return patients;
        }

        public void LoadData()
        {
            patients = patientDataHandler.ReadAll();
            appointments = appDataHandler.ReadAll();
        }
        public void writeData()
        {
            appDataHandler.Write(appointments);
        }
        //public DataHandler.AppointmentDataHandler DataHandler;
        //public ObservableCollection<Appointment> appointments;
   
   }
}