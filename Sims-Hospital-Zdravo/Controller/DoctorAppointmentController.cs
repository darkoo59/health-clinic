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
        public Doctor GetDoctor(int id)
        {
            return _doctorAppointmentService.GetDoctor(id);
        }

        public List<Doctor> ReadAllDoctors()
        {
            return _doctorAppointmentService.ReadAllDoctors();
        }

        public void Create(Appointment appointment)
        {
            _doctorAppointmentService.Create(appointment);
        }

        public void Update(Appointment appointment)
        {
            _doctorAppointmentService.Update(appointment);
        }

        public Appointment GetByID(Appointment appointment)
        {
            return _doctorAppointmentService.GetByID(appointment);
        }

        public List<Appointment> GetByDoctorID(int id)
        {
            return _doctorAppointmentService.ReadAll(id);
        }

        public ref ObservableCollection<Patient> GetPatients()
        {
            return ref _doctorAppointmentService.GetPatients();
        }

        public void DeleteByID(Appointment appointment)
        {
            _doctorAppointmentService.DeleteByID(appointment);
        }

        public List<Appointment> ReadAll(int id)
        {
            // TODO: implement
            return _doctorAppointmentService.ReadAll(id);
        }

        public Service.DoctorAppointmentService _doctorAppointmentService;
        

        public int GenerateId()
        {
            return _doctorAppointmentService.GenerateId();
        }

        public DoctorAppointmentController(DoctorAppointmentService doctorAppointmentService)
        {
            this._doctorAppointmentService = doctorAppointmentService;
        }

        public List<Appointment> FilterAppointmentsByDate(DateTime date)
        {
            return _doctorAppointmentService.FilterAppointmentsByDate(date);
        }

        public void DeleteAfterExaminationIsDone(DateTime date, int id, Patient pat)
        {
            _doctorAppointmentService.DeleteAfterExaminationIsDone(date, id, pat);
        }

        public List<Doctor> FindDoctorsBySpecalty(SpecialtyType specaltyType)
        {
            return _doctorAppointmentService.FindDoctorsBySpecalty(specaltyType);
        }

        public void UrgentSurgery(Appointment appointment,double duration)
        {
            _doctorAppointmentService.UrgentSurgery(appointment,duration);
        }

    }
}