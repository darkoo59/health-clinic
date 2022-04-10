/***********************************************************************
 * Module:  AppointmentRepositoryPatient.cs
 * Author:  I
 * Purpose: Definition of the Class Repository.AppointmentRepositoryPatient
 ***********************************************************************/

using System;

namespace Repository
{
   public class AppointmentRepositoryPatient : AppointmentRepository
   {
      public List<Appointment> FindByPatientID(int id)
      {
         // TODO: implement
         return null;
      }
      
      public new void Create(Model.Appointment appointment)
      {
         // TODO: implement
      }
      
      public new void Update(Model.Appointment appointment)
      {
         // TODO: implement
      }
      
      public new void Delete(Model.Appointment appointment)
      {
         // TODO: implement
      }
   
   }
}