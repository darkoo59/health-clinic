/***********************************************************************
 * Module:  AppointmentPatientController.cs
 * Author:  I
 * Purpose: Definition of the Class Controller.AppointmentPatientController
 ***********************************************************************/

using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
   public class AppointmentPatientController
   {

        public AppointmentPatientController(AppointmentPatientService appointmentPatientService)
        {
            this.appointmentPatientService = appointmentPatientService;
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
      
      public ref ObservableCollection<Appointment> FindByPatientID(int id)
        {
            return ref appointmentPatientService.FindByPatientID(id);
        }

        public ref ObservableCollection<Doctor> ReadDoctors()
        {
            return ref appointmentPatientService.ReadDoctors();
        }

        public ObservableCollection<Appointment> FindAll()
        {
            return appointmentPatientService.FindAll();
        }

        public ObservableCollection<Appointment> InitializeList(Appointment appointment, string priority) 
        {
            return appointmentPatientService.InitializeList(appointment,priority);
        }

        public void ValidateAppointment(Appointment appointment) 
        {
            appointmentPatientService.ValidateAppointment(appointment);
        }
        public void ValidateReshedule(Appointment appointment, TimeInterval timeInterval)
        {
            appointmentPatientService.ValidateReschedule(appointment, timeInterval);
        }
        public Service.AppointmentPatientService appointmentPatientService;
   
   }
}