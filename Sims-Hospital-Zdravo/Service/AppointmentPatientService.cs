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
        public AppointmentPatientService(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
        }
        public void Create(Appointment appointment)
      {
            appointmentRepository.Create(appointment);
      }
      
      public void Update(Appointment appointment)
      {
            appointmentRepository.Update(appointment);
      }
      
      public void Delete(Appointment appointment)
      {
            appointmentRepository.Delete(appointment);
      }
      
      public ObservableCollection<Appointment> FindByPatientID(int id)
      {
            return appointmentRepository.FindByPatientId(id);
      }

        public ref ObservableCollection<Doctor> ReadDoctors()
        {
            return ref doctorRepository.ReadAll();
        }

      public AppointmentRepository appointmentRepository;
      public DoctorRepository doctorRepository;
   }
}