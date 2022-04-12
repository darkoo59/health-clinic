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
        private ObservableCollection<Appointment> appointments;
        public AppointmentRepositoryPatient(AppointmentDataHandler appointmentDataHandler, DoctorDataHandler doctorDataHandler)
        {
            this.appointments = new ObservableCollection<Appointment>();
            this.doctors = new ObservableCollection<Doctor>();
            this.appointmentDataHandler = appointmentDataHandler;
            this.doctorDataHandler = doctorDataHandler;
            loadDataFromFiles();
        }

        public ref ObservableCollection<Appointment> FindByPatientID(int id)
      {
            return ref appointments;
      }
      
      public new void Create(Model.Appointment appointment)
      {
            appointments.Add(appointment);
            loadDataToFile();
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
      public Model.Appointment FindByID(int id)
      {
          // TODO: implement
          return null;
      }
        public void loadDataFromFiles()
        {
            appointments = appointmentDataHandler.ReadAll();
            doctors = doctorDataHandler.ReadAll();
        }

        public void loadDataToFile()
        {
            appointmentDataHandler.Write(appointments);
        }

        public ref ObservableCollection<Doctor> ReadDoctors() 
        {
            return ref this.doctors;
        }

        public DataHandler.DoctorDataHandler doctorDataHandler;
        public ObservableCollection<Doctor> doctors;
    }
}