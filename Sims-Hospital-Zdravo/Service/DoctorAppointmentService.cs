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
using System.Linq;
using Sims_Hospital_Zdravo.Utils;
using System.Windows;

namespace Service
{
    public class DoctorAppointmentService
    {
        public DoctorRepository doctorRepo;
        public AppointmentRepository appointmentRepository;
        public PatientRepository patientRepository;
        public TimeSchedulerService timeSchedulerService;
        private AppointmentDoctorValidator validator;
        private Appointment app;

        public DoctorAppointmentService(AppointmentRepository appointmentRepository, PatientRepository patientRepository, DoctorRepository docRepo, TimeSchedulerService timeSchedulerService, RoomService roomService)
        {
            this.timeSchedulerService = timeSchedulerService;
            this.appointmentRepository = appointmentRepository;
            this.patientRepository = patientRepository;
            this.doctorRepo = docRepo;
            this.validator = new AppointmentDoctorValidator(appointmentRepository, timeSchedulerService, roomService);
        }

        public Doctor GetDoctor(int id)
        {
            return doctorRepo.FindDoctorById(id);
        }

        public List<Doctor> ReadAllDoctors()
        {
            return doctorRepo.ReadAll().ToList();
        }

        public void Create(Appointment appointment)
        {
            try
            {
                validator.ValidateAppointment(appointment);

                appointmentRepository.Create(appointment);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
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
            validator.ValidateAppointment(appointment);
            appointmentRepository.Update(appointment);
        }

        public Appointment GetByID(Appointment appointment)
        {
            // TODO: implement
            //return appointmentRepository.GetByID(appointment);
            return null;
            //return null;
        }


        /// <summary>
        /// Appointment repository patient zamijeniti sa patient handlerom ili sta vec bude trebalo
        /// </summary>
        /// <param name="appointmentRepo"></param>
        /// <param name="appRepoPatient"></param>
        public int GenerateId()
        {
            ObservableCollection<Appointment> appointments = appointmentRepository.FindAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Appointment app in appointments)
            {
                ids.Add(app._Id);
            }

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }

        public ObservableCollection<Appointment> FilterAppointmentsByDate(DateTime date)
        {
            var AppointmentsDate = appointmentRepository.FindByDoctorId(2);
            var appDate = AppointmentsDate.Where(i => i._Time.Start.Date == date).ToList();
            AppointmentsDate = new ObservableCollection<Appointment>(appDate);

            return AppointmentsDate;
        }

        public void DeleteAfterExaminationIsDone(DateTime tl, int id, Patient pat)
        {
            app = timeSchedulerService.findAppointmentByDate(tl, id, pat);
            appointmentRepository.Delete(app);
        }

        public Appointment FindAppointmentByDateAndPatient(DateTime date, Patient pat, int id)
        {
            ObservableCollection<Appointment> appointments = appointmentRepository.FindByDoctorId(id);

            foreach (Appointment appointment in appointments)
            {
                if (appointment._Time.Start.Date.Equals(date.Date) && appointment._Patient._Jmbg.Equals(pat._Jmbg))
                {
                    app = appointment;
                }
            }

            return app;
        }
        
        public ObservableCollection<Doctor> FindDoctorsBySpecalty(SpecialtyType specaltyType)
        {
           return  doctorRepo.FindDoctorsBySpecalty(specaltyType);
        }


        public void IfUrgentRescheduleAllAppointment(int doctorID)
        {
            //ObservableCollection<>
        }

        public void UrgentSurgery(Appointment appointment,double duration)
        {
            
            TimeInterval tl = timeSchedulerService.FindIntervalForOperation(appointment,duration);
            appointment._Time = tl;
            Create(appointment);

        }

    }
}