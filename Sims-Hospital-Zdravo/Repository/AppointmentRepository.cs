/***********************************************************************
 * Module:  AppointmentRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.AppointmentRepository
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
   public class AppointmentRepository
   {
      public void Create(Model.Appointment appointment)
      {
         // TODO: implement
      }
      
      public void Update(Model.Appointment appointment)
      {
         // TODO: implement
      }
      
      public void Delete(Model.Appointment appointment)
      {
         // TODO: implement
      }
      
      public List<Appointment> FindByDoctorId(int id)
      {
         // TODO: implement
         return null;
      }
      
      public List<Appointment> FindByPatientId(int id)
      {
         // TODO: implement
         return null;
      }
      
      public List<Appointment> FindAll()
      {
         // TODO: implement
         return null;
      }
   
      public System.Collections.ArrayList appointment;
      
      /// <pdGenerated>default getter</pdGenerated>
      public System.Collections.ArrayList GetAppointment()
      {
         if (appointment == null)
            appointment = new System.Collections.ArrayList();
         return appointment;
      }
      
      /// <pdGenerated>default setter</pdGenerated>
      public void SetAppointment(System.Collections.ArrayList newAppointment)
      {
         RemoveAllAppointment();
         foreach (Model.Appointment oAppointment in newAppointment)
            AddAppointment(oAppointment);
      }
      
      /// <pdGenerated>default Add</pdGenerated>
      public void AddAppointment(Model.Appointment newAppointment)
      {
         if (newAppointment == null)
            return;
         if (this.appointment == null)
            this.appointment = new System.Collections.ArrayList();
         if (!this.appointment.Contains(newAppointment))
            this.appointment.Add(newAppointment);
      }
      
      /// <pdGenerated>default Remove</pdGenerated>
      public void RemoveAppointment(Model.Appointment oldAppointment)
      {
         if (oldAppointment == null)
            return;
         if (this.appointment != null)
            if (this.appointment.Contains(oldAppointment))
               this.appointment.Remove(oldAppointment);
      }
      
      /// <pdGenerated>default removeAll</pdGenerated>
      public void RemoveAllAppointment()
      {
         if (appointment != null)
            appointment.Clear();
      }
      public DataHandler.AppointmentDataHandler appointmentDataHandler;
   
   }
}