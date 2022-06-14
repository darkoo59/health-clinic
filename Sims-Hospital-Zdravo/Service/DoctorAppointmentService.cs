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
        public DoctorRepository _doctorRepo;
        public AppointmentRepository _appointmentRepository;
        public PatientRepository _patientRepository;
        public TimeSchedulerService _timeSchedulerService;
        public RoomService _roomService;
        private AppointmentDoctorValidator _validator;
        private Appointment _app;

        public DoctorAppointmentService()
        {
            this._timeSchedulerService = new TimeSchedulerService();
            this._appointmentRepository = new AppointmentRepository();
            this._roomService = new RoomService();
            this._patientRepository = new PatientRepository();
            this._doctorRepo = new DoctorRepository();
            this._validator = new AppointmentDoctorValidator(_appointmentRepository,_timeSchedulerService, _roomService);
        }

        public Doctor GetDoctor(int id)
        {
            return _doctorRepo.FindDoctorById(id);
        }

        public List<Doctor> ReadAllDoctors()
        {
            return _doctorRepo.FindAll();
        }

        public void Create(Appointment appointment)
        {
            _validator.ValidateAppointment(appointment);
            _appointmentRepository.Create(appointment);
        }

        public void DeleteByID(Appointment appointment)
        {
            _appointmentRepository.Delete(appointment);
        }

        public List<Appointment> ReadAll(int id)
        {
            return _appointmentRepository.FindByDoctorId(id);
        }


        public ref ObservableCollection<Patient> GetPatients()
        {
            return ref _patientRepository.ReadAll();
            //Zamijeniti pravim funkcijama
        }

        public void Update(Appointment appointment)
        {
            // TODO: implement
            _validator.ValidateAppointment(appointment);
            _appointmentRepository.Update(appointment);
        }

        public Appointment GetByID(Appointment appointment)
        {
            // TODO: implement
            //return appointmentRepository.GetByID(appointment);
            return null;
        }


        public int GenerateId()
        {
            List<Appointment> appointments = _appointmentRepository.FindAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Appointment app in appointments)
            {
                ids.Add(app.Id);
            }

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }

        public List<Appointment> FilterAppointmentsByDate(DateTime date)
        {
            var appointmentsDate = _appointmentRepository.FindByDoctorId(2);
            var appDate = appointmentsDate.Where(i => i.Time.Start.Date == date).ToList();
            appointmentsDate = new List<Appointment>(appDate);

            return appointmentsDate;
        }

        public void DeleteAfterExaminationIsDone(DateTime tl, int id, Patient pat)
        {
            _app = _timeSchedulerService.FindAppointmentByDate(tl, id, pat);
            _appointmentRepository.Delete(_app);
        }

        


        public List<Doctor> FindDoctorsBySpecalty(SpecialtyType specaltyType)
        {
            return _doctorRepo.FindDoctorsBySpecalty(specaltyType);
        }


        

        public void UrgentSurgery(Appointment appointment, double duration)
        {
            TimeInterval tl = _timeSchedulerService.FindIntervalForOperation(appointment, duration);
            appointment.Time = tl;
            Create(appointment);
        }
        public List<Appointment> FindOperations()
        {
            return _appointmentRepository.FindOperations();
        }
    }
}