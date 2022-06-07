using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Repository;
using Service;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo.Model
{
    public class SecretaryAppointmentService
    {
        private AppointmentRepository _appointmentRepository;
        private PatientRepository _patientRepository;
        private IRoomRepository _roomRepository;
        private DoctorRepository _doctorRepository;
        private TimeSchedulerService _timeSchedulerService;
        private AppointmentSecretaryValidator _validator;

        public SecretaryAppointmentService(AppointmentRepository appRepo, PatientRepository patientRepo, TimeSchedulerService timeService, DoctorRepository doctorRepository)
        {
            this._appointmentRepository = appRepo;
            this._patientRepository = patientRepo;
            this._timeSchedulerService = timeService;
            this._roomRepository = new RoomRepository();
            this._doctorRepository = doctorRepository;
            this._validator = new AppointmentSecretaryValidator(this);
        }

        public ObservableCollection<Appointment> ReadAllAppointmentsForDate(DateTime date)
        {
            return _appointmentRepository.ReadAllAppointmentsForDate(date);
        }

        public ObservableCollection<Appointment> ReadAll()
        {
            return _appointmentRepository.FindAll();
        }

        public void Create(Appointment appointment)
        {
            _validator.ValidateCreate(appointment);
            appointment.Id = GenerateId();
            _appointmentRepository.Create(appointment);
        }

        public void Delete(Appointment appointmentToDelete)
        {
            _appointmentRepository.Delete(appointmentToDelete);
        }

        public void Update(Appointment appointmentToUpdate)
        {
            _validator.ValidateRescheduling(appointmentToUpdate);
            _appointmentRepository.Update(appointmentToUpdate);
        }

        public ObservableCollection<Patient> ReadAllPatients()
        {
            return _patientRepository.ReadAll();
        }

        public List<Room> FindAvailableRoomsForInterval(TimeInterval interval)
        {
            List<Room> availableRooms = new List<Room>();
            foreach (Room room in _roomRepository.FindAll())
            {
                if (_timeSchedulerService.IsRoomFreeInInterval(room.Id, interval))
                    availableRooms.Add(room);
            }

            return availableRooms;
        }

        public Room FindAvailableRoomForEmergencyAppointment(TimeInterval interval)
        {
            List<Room> availableRooms = new List<Room>();
            foreach (Room room in _roomRepository.FindAll())
            {
                if ((room.Type == RoomType.OPERATION || room.Type == RoomType.EXAMINATION) && _timeSchedulerService.IsRoomFreeInInterval(room.Id, interval))
                    return room;
            }

            return null;
        }

        public List<Doctor> FindAvailableDoctorsForInterval(TimeInterval interval)
        {
            List<Doctor> availableDoctors = new List<Doctor>();
            foreach (Doctor doctor in _doctorRepository.ReadAll())
            {
                if (_timeSchedulerService.IsDoctorFreeInInterval(doctor.Id, interval))
                    availableDoctors.Add(doctor);
            }

            return availableDoctors;
        }

        public Appointment FindAndScheduleEmergencyAppointmentIfCan(Patient patient, SpecialtyType type) //TODO
        {
            TimeInterval interval = new TimeInterval(DateTime.Now.AddMinutes(5), DateTime.Now.AddHours(1));
            int counter = 0;
            DateTime minStart = new DateTime();
            Appointment appointment = null;
            foreach (Doctor doctor in _doctorRepository.FindDoctorsBySpeciality(type))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (_timeSchedulerService.IsDoctorFreeInInterval(doctor.Id, interval))
                    {
                        if (counter == 0)
                        {
                            minStart = interval.Start;
                            appointment = new Appointment(FindAvailableRoomForEmergencyAppointment(interval), doctor, patient, interval, AppointmentType.URGENCY);
                            appointment.Id = GenerateId();
                            counter++;
                        }
                        else if (interval.Start.CompareTo(minStart) < 0)
                        {
                            minStart = interval.Start;
                            appointment = new Appointment(FindAvailableRoomForEmergencyAppointment(interval), doctor, patient, interval, AppointmentType.URGENCY);
                            appointment.Id = GenerateId();
                            appointment.Patient.Id = GeneratePatientId();
                        }
                    }

                    interval.Start.AddMinutes(10);
                }

                interval.Start = DateTime.Now.AddMinutes(5);
            }

            return appointment;
        }

        public List<Doctor> FindAvailableDoctorsForSpeciality(SpecialtyType type)
        {
            return _doctorRepository.FindDoctorsBySpeciality(type);
        }

        public bool IsDoctorFreeInIntervalWithoutSelectedAppointment(int doctorId, Appointment app)
        {
            return _timeSchedulerService.IsDoctorFreeInIntervalWithoutSelectedAppointment(doctorId, app);
        }

        public bool IsPatientFreeInIntervalWithoutSelectedAppointment(int patientId, Appointment app)
        {
            return _timeSchedulerService.IsPatientFreeInIntervalWithoutSelectedAppointment(patientId, app);
        }


        public ObservableCollection<Patient> FindAvailablePatientsForInterval(TimeInterval startEndTime)
        {
            ObservableCollection<Patient> availablePatients = new ObservableCollection<Patient>();
            foreach (Patient patient in _patientRepository.ReadAll())
            {
                if (_timeSchedulerService.IsPatientFreeInInterval(patient.Id, startEndTime))
                {
                    availablePatients.Add(patient);
                }
            }

            return availablePatients;
        }

        public TimeInterval FindFirstDateToReschedule(Appointment appointment)
        {
            DateTime startDate = appointment.Time.Start;
            DateTime endDate = appointment.Time.End;
            TimeSpan appointmentLength = appointment.Time.End - appointment.Time.Start;
            startDate = appointment.Time.Start.AddHours(1);
            endDate = startDate;
            endDate = endDate.Add(appointmentLength);
            TimeInterval interval = new TimeInterval(startDate, endDate);
            while (true)
            {
                if (_timeSchedulerService.IsDoctorFreeInInterval(appointment.Doctor._Id,
                        interval) && _timeSchedulerService.IsPatientFreeInInterval(appointment.Patient._Id, interval))
                {
                    return interval;
                }

                interval.Start = interval.Start.AddMinutes(30);
                interval.End = interval.End.AddMinutes(30);
            }
        }

        public List<EmergencyReschedule> FindAllAppointmentsToRescheduleForEmergency(SpecialtyType type)
        {
            //Uzimamo samo appointmente u narednih 24h
            List<Appointment> appointmentsToReschedule = _appointmentRepository.FindByDoctorSpecialityBeforeDate(type, DateTime.Now.AddDays(1));
            List<EmergencyReschedule> appointmentsAndRescheduleDate = new List<EmergencyReschedule>();
            foreach (Appointment app in appointmentsToReschedule)
            {
                if (app.Type != AppointmentType.URGENCY)
                {
                    TimeInterval rescheduleTo = FindFirstDateToReschedule(app);
                    EmergencyReschedule rescheduledAppointment = new EmergencyReschedule(app, rescheduleTo);
                    appointmentsAndRescheduleDate.Add(rescheduledAppointment);
                }
            }

            appointmentsAndRescheduleDate.Sort((x, y) => x.RescheduledDate.Start.CompareTo(y.RescheduledDate.Start));
            return appointmentsAndRescheduleDate;
        }


        public int GenerateId()
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Appointment appointment in appointments)
            {
                ids.Add(appointment.Id);
            }

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }

        public int GeneratePatientId()
        {
            ObservableCollection<Patient> patients = _patientRepository.ReadAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Patient patient in patients)
            {
                ids.Add(patient.Id);
            }

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }

        public void ValidateAppointmentInterval(TimeInterval interval)
        {
            _validator.SchedulingAppointmentInWrongTime(interval);
        }
    }
}