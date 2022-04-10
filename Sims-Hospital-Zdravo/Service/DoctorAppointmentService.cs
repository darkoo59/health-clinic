/***********************************************************************
 * Module:  DoctorAppointmentService.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Service.DoctorAppointmentService
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;

namespace Service
{
   public class DoctorAppointmentService
   {
      public void Create(Appointment appointment)
      {
         // TODO: implement
      }
      
      public bool DeleteByID(Appointment appointment)
      {
         // TODO: implement
         return false;
      }
      
      public List <Appointment> GetAllByDoctorID(Doctor doctor)
      {
         // TODO: implement
         return null;
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
   
      public Repository.DoctorAppointmentRepository doctorAppointmentRepository;
   
   }
}