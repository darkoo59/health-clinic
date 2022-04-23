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

        public AppointmentRepository appointmentRepository;
        public PatientRepository patientRepository;
        /// <summary>
        /// Appointment repository patient zamijeniti sa patient handlerom ili sta vec bude trebalo
        /// </summary>
        /// <param name="appointmentRepo"></param>
        /// <param name="appRepoPatient"></param>

        public DoctorAppointmentService(AppointmentRepository appointmentRepository, PatientRepository patientRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
        }
    }
}