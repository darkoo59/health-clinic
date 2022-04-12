/***********************************************************************
 * Module:  DoctorAppointmentService.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Service.DoctorAppointmentService
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Service
{
   public class DoctorAppointmentService
   {
      public void Create(Appointment appointment)
      {
         // TODO: implement
         doctorAppointmentRepository.Create(appointment);
      }
      
      public bool DeleteByID(Appointment appointment)
      {
         // TODO: implement
         return doctorAppointmentRepository.DeleteByID(appointment);
         
      }
      
      public ObservableCollection <Appointment> GetAllByDoctorID(Doctor doctor)
      {
            // TODO: implement
            return doctorAppointmentRepository.GetAllByDoctorID(doctor);
         //return null;
      }
      
      public void Update(Appointment appointment)
      {
         // TODO: implement
         doctorAppointmentRepository.Update(appointment);
      }
      
      public Appointment GetByID(Appointment appointment)
      {
         // TODO: implement
        return doctorAppointmentRepository.GetByID(appointment);
         //return null;
      }
   
      public Repository.DoctorAppointmentRepository doctorAppointmentRepository;
   
   }
}