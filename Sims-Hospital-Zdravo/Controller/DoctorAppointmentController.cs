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
        public ref List<Patient> getPatients()
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

        public DoctorAppointmentController(DoctorAppointmentService AppService)
        {
            this.doctorAppointmentService = AppService;
        }
    }
}