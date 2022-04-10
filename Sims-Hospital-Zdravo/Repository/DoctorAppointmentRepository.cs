/***********************************************************************
 * Module:  DoctorAppointmentRepository.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Repository.DoctorAppointmentRepository
 ***********************************************************************/

using System;

namespace Repository
{
   public class DoctorAppointmentRepository : AppointmentRepository
   {
      public new void Create(Appointment appointment)
      {
         // TODO: implement
      }
      
      public void DeleteByID(Appointment appointment)
      {
         // TODO: implement
      }
      
      public new void Update(Appointment appointment)
      {
         // TODO: implement
      }
      
      public List<Appointment> GetAllByDoctorID(Doctor doctor)
      {
         // TODO: implement
         return null;
      }
      
      public Appointment GetByID(Appointment app)
      {
         // TODO: implement
         return null;
      }
   
   }
}