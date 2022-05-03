using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Repository;
using Service;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo.Service
{
    public class SecretaryAppointmentService
    {
        private AppointmentRepository appointmentRepository;
        private PatientRepository patientRepository;
        private RoomRepository roomRepository;
        private DoctorRepository doctorRepo;
        private TimeSchedulerService timeSchedulerService;
        private AppointmentSecretaryValidator validator;
        public SecretaryAppointmentService(AppointmentRepository appRepo, PatientRepository patientRepo, TimeSchedulerService timeService, RoomRepository roomRepository, DoctorRepository doctorRepository)
        {
            this.appointmentRepository = appRepo;
            this.patientRepository = patientRepo;
            this.timeSchedulerService = timeService;
            this.roomRepository = roomRepository;
            this.doctorRepo = doctorRepository;
            this.validator = new AppointmentSecretaryValidator(this);
        }

        public ObservableCollection<Appointment> ReadAllAppointmentsForDate(DateTime date)
        {
            return appointmentRepository.ReadAllAppointmentsForDate(date);
        }

        public void Create(Appointment appointment)
        {
            validator.ValidateCreate(appointment);
            appointmentRepository.Create(appointment);
        }

        public void Delete(Appointment appointmentToDelete)
        {
            appointmentRepository.Delete(appointmentToDelete);
        }

        public void Update(Appointment appointmentToUpdate)
        {
            validator.ValidateRescheduling(appointmentToUpdate);
            appointmentRepository.Update(appointmentToUpdate);
        }

        public ObservableCollection<Patient> ReadAllPatients()
        {
            return patientRepository.ReadAll();
        }

        public List<Room> FindAvailableRoomsForInterval(TimeInterval interval)
        {
            List<Room> availableRooms = new List<Room>();
            foreach (Room room in roomRepository.ReadAll())
            {
                if (timeSchedulerService.IsRoomFreeInInterval(room._Id, interval))
                    availableRooms.Add(room);
            }
            return availableRooms;
        }

        public List<Doctor> FindAvailableDoctorsForInterval(TimeInterval interval)
        {
            List<Doctor> availableDoctors = new List<Doctor>();
            foreach (Doctor doctor in doctorRepo.ReadAll())
            {
                if (timeSchedulerService.IsDoctorFreeInInterval(doctor._Id, interval))
                {
                    availableDoctors.Add(doctor);
                }
            }
            return availableDoctors;
        }

        public ObservableCollection<Patient> FindAvailablePatientsForInterval(TimeInterval startEndTime)
        {
            ObservableCollection<Patient> availablePatients = new ObservableCollection<Patient>();
            foreach (Patient patient in patientRepository.ReadAll())
            {
                if (timeSchedulerService.IsPatientFreeInInterval(patient._Id, startEndTime))
                {
                    availablePatients.Add(patient);
                }
            }
            return availablePatients;
        }

        public int GenerateId()
        {
            ObservableCollection<Appointment> appointments = appointmentRepository.FindAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Appointment appointment in appointments)
            {
                ids.Add(appointment._Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;

        }

        public void ValidateAppointmentInterval(TimeInterval interval)
        {
            validator.SchedulingAppointmentInWrongTime(interval);
        }
    }
}