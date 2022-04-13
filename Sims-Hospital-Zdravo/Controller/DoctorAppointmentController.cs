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
         // TODO: implement
         doctorAppointmentService.Create(appointment);
      }
      
      public void Update(Appointment appointment)
      {
         // TODO: implement
         doctorAppointmentService.Update(appointment);
      }
      
      public Appointment GetByID(Appointment appointment)
      {
         // TODO: implement
         //return null;
         return doctorAppointmentService.GetByID(appointment);
      }
      
      public ObservableCollection<Appointment> GetByDoctorID(int id)
      {
            // TODO: implement
            //return null;
            return doctorAppointmentService.GetAllByDoctorID(id);
      }
      
      public bool DeleteByID(Appointment appointment)
      {
         // TODO: implement
         //return false;
         return doctorAppointmentService.DeleteByID(appointment);
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