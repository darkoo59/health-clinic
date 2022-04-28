/***********************************************************************
 * Module:  AppointmentPatientService.cs
 * Author:  I
 * Purpose: Definition of the Class Service.AppointmentPatientService
 ***********************************************************************/

using Model;
using Repository;
using Sims_Hospital_Zdravo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class AppointmentPatientService
    {

        private AppointmentPatientValidator appointmentPatientValidator;
        public AppointmentPatientService(AppointmentRepository appointmentRepository, DoctorRepository doctorRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.doctorRepository = doctorRepository;
            this.appointmentPatientValidator = new AppointmentPatientValidator(appointmentRepository);
        }
        public void Create(Appointment appointment)
        {
            appointmentRepository.Create(appointment);
      }
      


        public void Update(Appointment appointment)
        {
            appointmentPatientValidator.rescheduleAppointment(appointment);
            appointmentRepository.Update(appointment);
        }

        public void Delete(Appointment appointment)
        {
            appointmentRepository.Delete(appointment);

      }
      
      public ref ObservableCollection<Appointment> FindByPatientID(int id)
      {
            return ref appointmentRepository.FindByPatientId(id);
      }

        public ref ObservableCollection<Doctor> ReadDoctors()
        {
            return ref doctorRepository.ReadAll();
        }

        public ObservableCollection<Appointment> FindAll() 
        {
            return appointmentRepository.FindAll();
        }

        public ObservableCollection<Appointment> InitializeList(Appointment appointment, string priority)
        {
            bool free = true;
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            ObservableCollection<DateTime> dates = new ObservableCollection<DateTime>();
            DateTime dt = new DateTime(appointment._Time.Start.Year, appointment._Time.Start.Month, appointment._Time.Start.Day);
            dt = dt.AddHours(8);
            dates.Add(dt);
            DateTime dt4 = new DateTime(appointment._Time.Start.Year, appointment._Time.Start.Month, appointment._Time.Start.Day);
            dt4 = dt4.AddHours(8);
            dt4 = dt4.AddMinutes(30);
            dates.Add(dt4);
            DateTime dt1 = new DateTime(appointment._Time.Start.Year, appointment._Time.Start.Month, appointment._Time.Start.Day);
            dt1 = dt1.AddHours(9);
            dates.Add(dt1);
            DateTime dt5 = new DateTime(appointment._Time.Start.Year, appointment._Time.Start.Month, appointment._Time.Start.Day);
            dt5 = dt5.AddHours(9);
            dt5 = dt5.AddMinutes(30);
            dates.Add(dt5);
            DateTime dt2 = new DateTime(appointment._Time.Start.Year, appointment._Time.Start.Month, appointment._Time.Start.Day);
            dt2 = dt2.AddHours(10);
            dates.Add(dt2);
            DateTime dt6 = new DateTime(appointment._Time.Start.Year, appointment._Time.Start.Month, appointment._Time.Start.Day);
            dt6 = dt6.AddHours(10);
            dt6 = dt6.AddMinutes(30);
            dates.Add(dt6);
            DateTime dt3 = new DateTime(appointment._Time.Start.Year, appointment._Time.Start.Month, appointment._Time.Start.Day);
            dt3 = dt3.AddHours(11);
            dates.Add(dt3);
            DateTime dt7 = new DateTime(appointment._Time.Start.Year, appointment._Time.Start.Month, appointment._Time.Start.Day);
            dt7 = dt7.AddHours(11);
            dt7 = dt7.AddMinutes(30);
            dates.Add(dt7);
            ObservableCollection<Doctor> doctors = ReadDoctors();
            /*if (appointment._Time.Start.Hour < 8 || appointment._Time.End.Hour > 12)
            {
                free = false;
            }*/
            if (priority.Equals("Date"))
            {
                foreach (Appointment app in FindAll())
                {
                    if (app._Doctor._Id == appointment._Doctor._Id)
                    {
                        if (app._Time.Start.Year == appointment._Time.Start.Year && app._Time.Start.DayOfYear == appointment._Time.Start.DayOfYear)
                        {
                            if ((appointment._Time.Start.Hour * 60 + appointment._Time.Start.Minute < app._Time.Start.Hour * 60 + app._Time.Start.Minute) && (appointment._Time.End.Hour * 60 + appointment._Time.End.Minute < app._Time.End.Hour * 60 + app._Time.End.Minute))
                            {
                                free = false;
                            }
                        }
                    }
                    if (app._Time.Start.Year == appointment._Time.Start.Year && app._Time.Start.DayOfYear == appointment._Time.Start.DayOfYear)
                    {
                        if ((appointment._Time.Start.Hour * 60 + appointment._Time.Start.Minute < app._Time.Start.Hour * 60 + app._Time.Start.Minute) && (appointment._Time.End.Hour * 60 + appointment._Time.End.Minute < app._Time.End.Hour * 60 + app._Time.End.Minute))
                        {
                            doctors.Remove(app._Doctor);
                        }
                    }
                }
            }
            else if (priority.Equals("Doctor"))
            {
                foreach (Appointment app in FindAll())
                {
                    if (app._Doctor._Id == appointment._Doctor._Id)
                    {
                        if (app._Time.Start.Year == appointment._Time.Start.Year && app._Time.Start.DayOfYear == appointment._Time.Start.DayOfYear)
                        {
                            if ((appointment._Time.Start.Hour * 60 + appointment._Time.Start.Minute < app._Time.Start.Hour * 60 + app._Time.Start.Minute) && (appointment._Time.End.Hour * 60 + appointment._Time.End.Minute < app._Time.End.Hour * 60 + app._Time.End.Minute))
                            {
                                free = false;
                            }
                            dates.Remove(app._Time.Start);
                        }

                    }
                    
                }
            }
            if (free)
            {
                appointments.Add(appointment);
            }
            else if (priority.Equals("Date"))
            {
                foreach (Doctor d in doctors) 
                {
                    appointment._Doctor = d;
                    appointments.Add(appointment);
                }
            }
            else
            {
                foreach (DateTime d in dates)
                {
                    TimeInterval ti = new TimeInterval(d, d.AddMinutes(30));
                    Appointment app = new Appointment(null, appointment._Doctor, appointment._Patient, ti, AppointmentType.EXAMINATION);
                    appointments.Add(app);
                }
            }
            return appointments;
        }
        public AppointmentRepository appointmentRepository;
        public DoctorRepository doctorRepository;
    }
}