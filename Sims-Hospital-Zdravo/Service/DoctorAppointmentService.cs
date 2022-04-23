/***********************************************************************
 * Module:  DoctorAppointmentService.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Service.DoctorAppointmentService
 ***********************************************************************/

using Model;
using Repository;
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

        public ObservableCollection<Appointment> ReadAll(int id)
        {
            // TODO: implement
            return doctorAppointmentRepository.ReadAll(id);
        }
        public ObservableCollection <Appointment> GetAllByDoctorID(int id)
      {
            // TODO: implement
            return doctorAppointmentRepository.GetAllByDoctorID(id);
         //return null;
      }

        public ref List<Patient> getPatients()
        {
            return ref doctorAppointmentRepository.GetPatients();
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

        public DoctorAppointmentService(DoctorAppointmentRepository doctorAppointmentRepo)
        {
            this.doctorAppointmentRepository = doctorAppointmentRepo;
        }
    }
}