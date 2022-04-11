/***********************************************************************
 * Module:  AppointmentRepositoryPatient.cs
 * Author:  I
 * Purpose: Definition of the Class Repository.AppointmentRepositoryPatient
 ***********************************************************************/

using DataHandler;
using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
   public class AppointmentRepositoryPatient : AppointmentRepository
   {

        public List<Appointment> FindByPatientID(int id)
      {
            List<Appointment> appointments = new List<Appointment>();
            foreach (Appointment app in GetAppointment()) {
                if (app._Id == id) 
                appointments.Add(app);
            }
            return appointments;
      }
      
      public new void Create(Model.Appointment appointment)
      {
            AddAppointment(appointment);
      }
      
      public new void Update(Model.Appointment appointment)
      {
            foreach (Appointment app in GetAppointment()) 
            {
                if (app._Id==appointment._Id) 
                {
                   RemoveAppointment(app);
                    AddAppointment(appointment);
                    break;
                }
            }
      }
      
      public new void Delete(Model.Appointment appointment)
      {
            RemoveAppointment(appointment);
      }
   
   }
}