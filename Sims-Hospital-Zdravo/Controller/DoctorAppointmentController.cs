/***********************************************************************
 * Module:  DoctorAppointmentController.cs
 * Author:  Cvetana
 * Purpose: Definition of the Class Controller.DoctorAppointmentController
 ***********************************************************************/

using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
    public class DoctorAppointmentController
    {
        public Doctor getDoctor(int id)
        {
            return doctorAppointmentService.GetDoctor(id);
        }

        public List<Doctor> ReadAllDoctors()
        {
            return doctorAppointmentService.ReadAllDoctors();
        }

        public void Create(Appointment appointment)
        {
            doctorAppointmentService.Create(appointment);
        }

        public void Update(Appointment appointment)
        {
            doctorAppointmentService.Update(appointment);
        }

        public Appointment GetByID(Appointment appointment)
        {
            return doctorAppointmentService.GetByID(appointment);
        }

        public ObservableCollection<Appointment> GetByDoctorID(int id)
        {
            return doctorAppointmentService.ReadAll(id);
        }

        public ref ObservableCollection<Patient> getPatients()
        {
            return ref doctorAppointmentService.GetPatients();
        }

        public void DeleteByID(Appointment appointment)
        {
            doctorAppointmentService.DeleteByID(appointment);
        }

        public ObservableCollection<Appointment> ReadAll(int id)
        {
            // TODO: implement
            return doctorAppointmentService.ReadAll(id);
        }

        public Service.DoctorAppointmentService doctorAppointmentService;
        //public DoctorAppointmentService doctorAppService;

        public int GenerateId()
        {
            return doctorAppointmentService.GenerateId();
        }

        public DoctorAppointmentController(DoctorAppointmentService AppService)
        {
            this.doctorAppointmentService = AppService;
        }

        public ObservableCollection<Appointment> FilterAppointmentsByDate(DateTime date)
        {
            return doctorAppointmentService.FilterAppointmentsByDate(date);
        }

        public void DeleteAfterExaminationIsDone(DateTime date, int id, Patient pat)
        {
            doctorAppointmentService.DeleteAfterExaminationIsDone(date, id, pat);
        }

        public ObservableCollection<Doctor> FindDoctorsBySpecalty(SpecaltyType specaltyType)
        {
            return doctorAppointmentService.FindDoctorsBySpecalty(specaltyType);
        }

        public void UrgentSurgery(Appointment appointment,double duration)
        {
            doctorAppointmentService.UrgentSurgery(appointment,duration);
        }

    }
}