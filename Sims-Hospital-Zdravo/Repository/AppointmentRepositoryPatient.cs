/***********************************************************************
 * Module:  AppointmentRepositoryPatient.cs
 * Author:  I
 * Purpose: Definition of the Class Repository.AppointmentRepositoryPatient
 ***********************************************************************/

using DataHandler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Repository
{
   public class AppointmentRepositoryPatient : AppointmentRepository
   {
        public AppointmentRepositoryPatient(AppointmentDataHandler appointmentDataHandler)
        {
            this.appointments = new ObservableCollection<Appointment>();
            this.appointmentDataHandler = appointmentDataHandler;
        }

        public ObservableCollection<Appointment> FindByPatientID(int id)
      {
            ObservableCollection<Appointment> appointments1 = new ObservableCollection<Appointment>();
            foreach (Appointment app in this.appointments) {
                if (app._Id == id) 
                appointments1.Add(app);
            }
            return appointments1;
      }
      
      public new void Create(Model.Appointment appointment)
      {
            this.appointments.Add(appointment);
      }
      
      public new void Update(Model.Appointment appointment)
      {
            foreach (Appointment app in this.appointments) 
            {
                if (app._Id==appointment._Id) 
                {
                    this.appointments.Remove(app);
                    this.appointments.Add(appointment);
                    break;
                }
            }
      }
      
      public new void Delete(Model.Appointment appointment)
      {
            this.appointments.Remove(appointment);
      }
   
   }
}