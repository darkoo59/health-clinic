/***********************************************************************
 * Module:  AppointmentPatientController.cs
 * Author:  I
 * Purpose: Definition of the Class Controller.AppointmentPatientController
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
   public class AppointmentPatientController
   {
      public void Create(Appointment appointment)
      {
            appointmentPatientService.Create(appointment);
      }
      
      public void Update(Appointment appointment)
      {
            appointmentPatientService.Update(appointment);
      }
      
      public void Delete(Appointment appointment)
      {
            appointmentPatientService.Delete(appointment);
      }
      
      public ObservableCollection<Appointment> FindByPatientID(int id)
        {
            return appointmentPatientService.FindByPatientID(id);
        }
   
      public Service.AppointmentPatientService appointmentPatientService;
   
   }
}