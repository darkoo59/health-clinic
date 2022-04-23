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
       
        //public ObservableCollection<Appointment> appointmentsList;
        public List<Patient> patients;
        public PatientDataHandler patientDataHandler;
       

        public DoctorAppointmentRepository(PatientDataHandler patientData, AppointmentDataHandler appointmentDataHandler) : base(appointmentDataHandler)
        {
            appDataHandler = appointmentDataHandler;
            this.appointments = new ObservableCollection<Appointment>();
            patients = new List<Patient>();
            patientDataHandler = patientData;

        }

       

        public new void Create(Appointment appointment)
      {
            // TODO: implement
            base.Create(appointment);
            appointments.Add(appointment);


      }


      
      public Boolean DeleteByID(Appointment appointment)
      {
         // TODO: implement
         foreach(  Appointment app in appointments)
            {
                if (app._Id == appointment._Id)
                {
                    appointments.Remove(app);
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
                    //app._Doctor = appointment._Doctor;
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

        public ObservableCollection<Appointment> ReadAll(int id)
        {
            return base.FindByDoctorId(id);
        }
    
        

        public  ref List<Patient> GetPatients()
        {
            return  ref patients;
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