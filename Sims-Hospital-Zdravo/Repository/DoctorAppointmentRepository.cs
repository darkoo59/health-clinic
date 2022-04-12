/***********************************************************************
 * Module:  DoctorAppointmentRepository.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Repository.DoctorAppointmentRepository
 ***********************************************************************/

using DataHandler;
using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
   public class DoctorAppointmentRepository : AppointmentRepository
   {
        public DoctorAppointmentRepository(AppointmentDataHandler appointmentDataHandler)
        {
            this.appointmentDataHandler = appointmentDataHandler;
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
      
      public List<Appointment> GetAllByDoctorID(Doctor doctor)
      {
         // TODO: implement
         List<Appointment> backup=new List<Appointment>();
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
            this.appointments = appointmentDataHandler.ReadAll();
        }
        public DataHandler.AppointmentDataHandler DataHandler;
        List<Appointment> appointments;
   
   }
}