/***********************************************************************
 * Module:  AppointmentPatientService.cs
 * Author:  I
 * Purpose: Definition of the Class Service.AppointmentPatientService
 ***********************************************************************/

using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
   public class AppointmentPatientService
   {
        public AppointmentPatientService(AppointmentRepositoryPatient appointmentRepositoryPatient)
        {
            this.appointmentRepositoryPatient = appointmentRepositoryPatient;
        }
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
      
      public ref ObservableCollection<Appointment> FindByPatientID(int id)
      {
            return ref appointmentRepositoryPatient.FindByPatientID(id);
      }

        public ref ObservableCollection<Doctor> ReadDoctors()
        {
            return ref appointmentRepositoryPatient.ReadDoctors();
        }

      public Repository.AppointmentRepositoryPatient appointmentRepositoryPatient;
   
   }
}