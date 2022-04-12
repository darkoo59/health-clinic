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
        public DataHandler.AppointmentDataHandler appDataHandler;
        public ObservableCollection<Appointment> appointments;
        public DoctorAppointmentRepository(AppointmentDataHandler appointmentDataHandler)
        {
            appDataHandler = appointmentDataHandler;
            appointments = new ObservableCollection<Appointment>();

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
      
      public ObservableCollection<Appointment> GetAllByDoctorID(Doctor doctor)
      {
            // TODO: implement
             ObservableCollection<Appointment> backup = new ObservableCollection<Appointment>();
            foreach(Appointment appointment in appointments)
            {
                if(appointment._Doctor == doctor)
                {
                    backup.Add(appointment);
                }
            }
            if (backup.Count == 0)

                return null;
            else
                return backup;
      }
        /*public ObservableCollection<Appointment> GetAllByDoctorIDAndDate(Doctor doctor,DateTime date)
        {
            // TODO: implement
            ObservableCollection<Appointment> backup = new ObservableCollection<Appointment>();
            foreach (Appointment appointment in appointments)
            {
                if (appointment._Doctor == doctor )
                {
                    backup.Add(appointment);
                }
            }
            if (backup.Count == 0)

                return null;
            else
                return backup;
        }*/

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
    
        public void LoadData()
        {
            appointments = DataHandler.ReadAll();
        }
        public void writeData()
        {
            appDataHandler.Write(appointments);
        }
        //public DataHandler.AppointmentDataHandler DataHandler;
        //public ObservableCollection<Appointment> appointments;
   
   }
}