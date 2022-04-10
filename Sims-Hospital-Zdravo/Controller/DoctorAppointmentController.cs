/***********************************************************************
 * Module:  DoctorAppointmentController.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Controller.DoctorAppointmentController
 ***********************************************************************/

using System;

namespace Controller
{
   public class DoctorAppointmentController
   {
      public void Create(Appointmnent appointment)
      {
         // TODO: implement
      }
      
      public void Update(Appointment appointment)
      {
         // TODO: implement
      }
      
      public Appointment GetByID(Appointment appointment)
      {
         // TODO: implement
         return null;
      }
      
      public List<Appointment> GetByDoctorID(Doctor doctor)
      {
         // TODO: implement
         return null;
      }
      
      public bool DeleteByID(Appointment appointment)
      {
         // TODO: implement
         return false;
      }
   
      public Service.DoctorAppointmentService doctorAppointmentService;
   
   }
}