/***********************************************************************
 * Module:  AppointmentPatientController.cs
 * Author:  I
 * Purpose: Definition of the Class Controller.AppointmentPatientController
 ***********************************************************************/

using Model;
using Service;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
    public class AppointmentPatientController
    {
        public AppointmentPatientController(AccountRepository accountRepository)
        {
            this.appointmentPatientService = new AppointmentPatientService(accountRepository);
        }

        public void Create(Appointment appointment)
        {
            appointmentPatientService.Create(appointment);
        }

        public void Update(Appointment appointment)
        {
            appointmentPatientService.Update(appointment);
        }

        public void Delete(Appointment appointment)
        {
            appointmentPatientService.Delete(appointment);
        }

        public List<Appointment> FindByPatientId(int id)
        {
            return appointmentPatientService.FindByPatientId(id);
        }
        public List<Appointment> FindByPatientIdOld(int id)
        {
            return appointmentPatientService.FindByPatientIdOld(id);
        }
        public List<Appointment> FindByPatientIdNew(int id)
        {
            return appointmentPatientService.FindByPatientIdNew(id);
        }
        public List<Doctor> ReadDoctors()
        {
            return appointmentPatientService.ReadDoctors();
        }

        public List<Appointment> FindAll()
        {
            return appointmentPatientService.FindAll();
        }

        public List<Appointment> InitializeList(Appointment appointment, string priority)
        {
            return appointmentPatientService.InitializeList(appointment, priority);
        }

        public void ValidateAppointment(Appointment appointment)
        {
            appointmentPatientService.ValidateAppointment(appointment);
        }

        public void ValidateReshedule(Appointment appointment, TimeInterval timeInterval)
        {
            appointmentPatientService.ValidateReschedule(appointment, timeInterval);
        }
        public void SetAppointmentRated(Appointment appointment)
        {
            appointmentPatientService.SetAppointmentRated(appointment);
        }

        public Service.AppointmentPatientService appointmentPatientService;
    }
}