/***********************************************************************
 * Module:  DoctorAppointmentController.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Controller.DoctorAppointmentController
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class DoctorAppointmentController
   {
      public void Create(Appointment appointment)
      {
         // TODO: implement
         doctorAppointmentService.Create(appointment);
      }
      
      public void Update(Appointment appointment)
      {
         // TODO: implement
         doctorAppointmentService.Update(appointment);
      }
      
      public Appointment GetByID(Appointment appointment)
      {
         // TODO: implement
         //return null;
         return doctorAppointmentService.GetByID(appointment);
      }
      
      public List<Appointment> GetByDoctorID(Doctor doctor)
      {
         // TODO: implement
         //return null;
         return doctorAppointmentService.GetAllByDoctorID(doctor);
      }
      
      public bool DeleteByID(Appointment appointment)
      {
         // TODO: implement
         //return false;
         return doctorAppointmentService.DeleteByID(appointment);
      }
   
      public Service.DoctorAppointmentService doctorAppointmentService;
   
   }
}