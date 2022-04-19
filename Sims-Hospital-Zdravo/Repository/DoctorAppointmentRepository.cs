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
        public ObservableCollection<Appointment> appointmentsList;
        public List<Patient> patients;
        public PatientDataHandler patientDataHandler;
        //private object appointmentDataHandler1;
        //private RoomDataHandler roomDataHandler;

        public DoctorAppointmentRepository(PatientDataHandler patientData, AppointmentDataHandler appointmentDataHandler) : base(appointmentDataHandler)
        {
            appDataHandler = appointmentDataHandler;
            appointmentsList = new ObservableCollection<Appointment>();
            patients = new List<Patient>();

        }

       

        public new void Create(Appointment appointment)
      {
            // TODO: implement
            base.Create(appointment);
            appointmentsList.Add(appointment);


      }
      
      public Boolean DeleteByID(Appointment appointment)
      {
         // TODO: implement
         foreach(  Appointment app in appointmentsList)
            {
                if (app._Id == appointment._Id)
                {
                    appointmentsList.Remove(app);
                    return true;
                }
            }
         return false;
      }
      
      public new void Update(Appointment appointment)
      {
         // TODO: implement
         foreach(Appointment app in appointmentsList)
            {
                if(app._Id == appointment._Id)
                {
                    app._DateAndTime = appointment._DateAndTime;
                    //app._Doctor = appointment._Doctor;
                    app._Patient._Id = appointment._Patient._Id;
                    app._Room = appointment._Room;
                }

            }
         

      }
      
      public ObservableCollection<Appointment> GetAllByDoctorID(int id)
      {
            // TODO: implement
             ObservableCollection<Appointment> backup = new ObservableCollection<Appointment>();
            foreach(Appointment appointment in appointmentsList)
            {
                if(appointment._Doctor._Id == id)
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
         
         foreach(Appointment appoi in appointmentsList)
            {
                if (appoi._Id == app._Id)
                {
                   
                    return appoi;
;
                }
            }
         
         return null;
      }

        public ObservableCollection<Appointment> ReadAll(int id)
        {
            return base.FindByDoctorId(id);
        }
    
        

        public List<Patient> GetPatients()
        {
            return patients;
        }

        public void LoadData(int id)
        {
            patients = patientDataHandler.ReadAll();
            //appointmentsList = appDataHandler.ReadAll();
            //appointmentsList = base.FindByDoctorId(id);
        }
        public void writeData()
        {
            //appDataHandler.Write(appointmentsList);
        }
        //public DataHandler.AppointmentDataHandler DataHandler;
        //public ObservableCollection<Appointment> appointments;
   
   }
}