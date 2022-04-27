/***********************************************************************
 * Module:  AppointmentPatientService.cs
 * Author:  I
 * Purpose: Definition of the Class Service.AppointmentPatientService
 ***********************************************************************/

using Model;
using Repository;
using Sims_Hospital_Zdravo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class AppointmentPatientService
    {

        private AppointmentPatientValidator appointmentPatientValidator;
        public AppointmentPatientService(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
            this.appointmentPatientValidator = new AppointmentPatientValidator(appointmentRepository);
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
      
      public ref ObservableCollection<Appointment> FindByPatientID(int id)
      {
            return ref appointmentRepository.FindByPatientId(id);
      }

        public ref ObservableCollection<Doctor> ReadDoctors()
        {
            return ref doctorRepository.ReadAll();
        }

 


        public AppointmentRepository appointmentRepository;
        public DoctorRepository doctorRepository;
    }
}