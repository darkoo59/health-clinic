/***********************************************************************
 * Module:  AppointmentPatientService.cs
 * Author:  I
 * Purpose: Definition of the Class Service.AppointmentPatientService
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
   public class AppointmentPatientService
   {
      public void Create(Appointment appointment)
      {
            appointmentRepositoryPatient.Create(appointment);
      }
      
      public void Update(Appointment appointment)
      {
            appointmentRepositoryPatient.Update(appointment);
      }
      
      public void Delete(Appointment appointment)
      {
            appointmentRepositoryPatient.Delete(appointment);
      }
      
      public ObservableCollection<Appointment> FindByPatientID(int id)
      {
            return appointmentRepositoryPatient.FindByPatientID(id);
      }
   
      public Repository.AppointmentRepositoryPatient appointmentRepositoryPatient;
   
   }
}