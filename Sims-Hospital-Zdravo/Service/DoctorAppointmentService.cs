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
        public DoctorRepository doctorRepo;
        public AppointmentRepository appointmentRepository;
        public PatientRepository patientRepository;

        public DoctorAppointmentService(AppointmentRepository appointmentRepository, PatientRepository patientRepository, DoctorRepository docRepo)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.doctorRepo = docRepo;
        }
        public Doctor GetDoctor(int id)
        {
            return doctorRepo.FindDoctorById(id);
        }
        public void Create(Appointment appointment)
        {
            appointment._Id = generateId();
            appointmentRepository.Create(appointment);
        }

        public void DeleteByID(Appointment appointment)
        {
            appointmentRepository.Delete(appointment);

        }

        public ObservableCollection<Appointment> ReadAll(int id)
        {
            return appointmentRepository.FindByDoctorId(id);
        }

        Random random = new Random();
            public int generateId()
        {
            return random.Next(1,100000000);
        }
        public ref ObservableCollection<Patient> GetPatients()
        {
            return ref patientRepository.ReadAll();
            //Zamijeniti pravim funkcijama

        }

        public void Update(Appointment appointment)
        {
            // TODO: implement
            appointmentRepository.Update(appointment);
        }

        public Appointment GetByID(Appointment appointment)
        {
            // TODO: implement
            //return appointmentRepository.GetByID(appointment);
            return null;
            //return null;
        }

        
        /// <summary>
        /// Appointment repository patient zamijeniti sa patient handlerom ili sta vec bude trebalo
        /// </summary>
        /// <param name="appointmentRepo"></param>
        /// <param name="appRepoPatient"></param>

        
    }
}