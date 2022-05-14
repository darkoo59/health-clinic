using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Repository;
using Service;
using Sims_Hospital_Zdravo.Model;
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

        public ObservableCollection<Appointment> ReadAll()
        {
            return appointmentRepository.FindAll();
        }

        public void Create(Appointment appointment)
        {
            validator.ValidateCreate(appointment);
            appointment._Id = GenerateId();
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
                if (timeSchedulerService.IsRoomFreeInInterval(room.Id, interval))
                    availableRooms.Add(room);
            }
            return availableRooms;
        }

        public Room FindAvailableRoomForEmergencyAppointment(TimeInterval interval)
        {
            List<Room> availableRooms = new List<Room>();
            foreach (Room room in roomRepository.ReadAll())
            {
                if ((room.Type == RoomType.OPERATION || room.Type == RoomType.EXAMINATION) && timeSchedulerService.IsRoomFreeInInterval(room.Id, interval))
                    return room;
            }
            return null;
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

        public Appointment FindAndScheduleEmergencyAppointmentIfCan(Patient patient, SpecialtyType type) //TODO
        {
            TimeInterval interval = new TimeInterval(DateTime.Now.AddMinutes(5), DateTime.Now.AddHours(1));
            int counter = 0;
            DateTime minStart = new DateTime();
            Appointment appointment = null;
            foreach (Doctor doctor in doctorRepo.FindDoctorsBySpeciality(type))
            {
                for (int i = 0; i < 6; i++)
                {
                    if (timeSchedulerService.IsDoctorFreeInInterval(doctor._Id, interval))
                    {
                        if(counter == 0)
                        {
                            minStart = interval.Start;
                            appointment = new Appointment(FindAvailableRoomForEmergencyAppointment(interval), doctor, patient, interval, AppointmentType.URGENCY);
                            appointment._Id = GenerateId();
                            counter++;
                        }
                        else if(interval.Start.CompareTo(minStart)<0)
                        {
                            minStart = interval.Start;
                            appointment = new Appointment(FindAvailableRoomForEmergencyAppointment(interval), doctor, patient, interval, AppointmentType.URGENCY);
                            appointment._Id = GenerateId();
                            appointment._Patient._Id = GeneratePatientId();
                            Create(appointment);
                        }
                    }
                    interval.Start.AddMinutes(5);
                }
                interval.Start = DateTime.Now.AddMinutes(5);
            }
            return appointment;
        }
        public List<Doctor> FindAvailableDoctorsForSpeciality(SpecialtyType type)
        {
            return doctorRepo.FindDoctorsBySpeciality(type);
        }

        public bool IsDoctorFreeInIntervalWithoutSelectedAppointment(int doctorId, Appointment app)
        {
            return timeSchedulerService.IsDoctorFreeInIntervalWithoutSelectedAppointment(doctorId, app);
        }

        public bool IsPatientFreeInIntervalWithoutSelectedAppointment(int patientId, Appointment app)
        {
            return timeSchedulerService.IsPatientFreeInIntervalWithoutSelectedAppointment(patientId, app);
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

        public TimeInterval FindFirstDateToReschedule(Appointment appointment)
        {
            DateTime startDate = appointment._Time.Start;
            DateTime endDate = appointment._Time.End;
            TimeSpan appointmentLength = appointment._Time.End - appointment._Time.Start;
            startDate = appointment._Time.Start.AddHours(1);
            endDate = startDate;
            endDate = endDate.Add(appointmentLength);
            TimeInterval interval = new TimeInterval(startDate, endDate);
            while (true)
            {
                if (timeSchedulerService.IsDoctorFreeInInterval(appointment._Doctor._Id,
                        interval) && timeSchedulerService.IsPatientFreeInInterval(appointment._Patient._Id,interval))
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
            List<Appointment> appointmentsToReschedule = appointmentRepository.FindByDoctorSpecialityBeforeDate(type, DateTime.Now.AddDays(1));
            List<EmergencyReschedule> appointmentsAndRescheduleDate = new List<EmergencyReschedule>();
            foreach(Appointment app in appointmentsToReschedule)
            {
                if (app._Type != AppointmentType.URGENCY)
                {
                    TimeInterval rescheduleTo = FindFirstDateToReschedule(app);
                    EmergencyReschedule rescheduledAppointment = new EmergencyReschedule(app, rescheduleTo);
                    appointmentsAndRescheduleDate.Add(rescheduledAppointment);
                }
            }

            appointmentsAndRescheduleDate.Sort((x,y) => x.RescheduledDate.Start.CompareTo(y.RescheduledDate.Start));
            return appointmentsAndRescheduleDate;
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

        public int GeneratePatientId()
        {
            ObservableCollection<Patient> patients = patientRepository.ReadAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Patient patient in patients)
            {
                ids.Add(patient._Id);
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