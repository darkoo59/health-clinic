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
        private IAppointmentRepository _appointmentRepository;
        private PatientRepository _patientRepository;
        private IRoomRepository _roomRepository;
        private IDoctorRepository _doctorRepository;
        private TimeSchedulerService _timeSchedulerService;
        private AppointmentSecretaryValidator _validator;

        public SecretaryAppointmentService()
        {
            this._appointmentRepository = new AppointmentRepository();
            this._patientRepository = new PatientRepository();
            this._timeSchedulerService = new TimeSchedulerService();
            this._roomRepository = new RoomRepository();
            this._doctorRepository = new DoctorRepository();
            this._validator = new AppointmentSecretaryValidator(this);
        }

        public List<Appointment> ReadAllAppointmentsForDate(DateTime date)
        {
            return _appointmentRepository.ReadAllAppointmentsForDate(date);
        }

        public void Create(Appointment appointment)
        {
            _validator.ValidateCreate(appointment);
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
            return _roomRepository.FindAll().Where(room =>
                _timeSchedulerService.IsRoomFreeInInterval(room.Id, interval)).ToList();
        }

        public Room FindAvailableRoomForEmergencyAppointment(TimeInterval interval)
        {
            return _roomRepository.FindAll().FirstOrDefault(room => (room.Type == RoomType.OPERATION || room.Type
                == RoomType.EXAMINATION) && _timeSchedulerService.IsRoomFreeInInterval(room.Id, interval));
        }

        public List<Doctor> FindAvailableDoctorsForInterval(TimeInterval interval)
        {
            return _doctorRepository.FindAll().Where(doctor => _timeSchedulerService
                .IsDoctorFreeInInterval(doctor.Id, interval)).ToList();
        }

        public Appointment FindAndScheduleEmergencyAppointmentIfCan(Patient patient, SpecialtyType type)
        {
            TimeInterval interval = new TimeInterval(DateTime.Now.AddMinutes(5), DateTime.Now.AddHours(1));
            Appointment appointment = null;
            foreach (Doctor doctor in _doctorRepository.FindDoctorsBySpeciality(type))
            {
                DateTime minStart = interval.Start;
                appointment = new Appointment(FindAvailableRoomForEmergencyAppointment(interval), doctor, patient, interval, AppointmentType.URGENCY);
                for (int i = 0; i < 4; i++)
                {
                    if (_timeSchedulerService.IsDoctorFreeInInterval(doctor.Id, interval) && interval.Start.CompareTo(minStart) < 0)
                    {
                        minStart = interval.Start;
                        appointment = new Appointment(FindAvailableRoomForEmergencyAppointment(interval), doctor, patient, interval, AppointmentType.URGENCY);
                    }

                    interval.Start = interval.Start.AddMinutes(10);
                }
                interval.Start = DateTime.Now.AddMinutes(5);
            }
            return appointment;
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
                    availablePatients.Add(patient);
            }
            return availablePatients;
        }

        public TimeInterval FindFirstDateToReschedule(Appointment appointment)
        {
            DateTime startDate = appointment.Time.Start.AddHours(1);
            TimeSpan appointmentLength = appointment.Time.End - appointment.Time.Start;
            DateTime endDate = startDate.Add(appointmentLength);
            TimeInterval interval = new TimeInterval(startDate, endDate);
            interval = SetDateAndTimeWhenToReschedule(appointment,interval);
            return interval;
        }

        private TimeInterval SetDateAndTimeWhenToReschedule(Appointment appointment, TimeInterval interval)
        {
            while (true)
            {
                if (_timeSchedulerService.IsDoctorFreeInInterval(appointment.Doctor.Id,
                        interval) && _timeSchedulerService.IsPatientFreeInInterval(appointment.Patient.Id, interval))
                {
                    return interval;
                }
                interval.Start = interval.Start.AddMinutes(30);
                interval.End = interval.End.AddMinutes(30);
            }
        }

        public List<EmergencyReschedule> FindAllAppointmentsToRescheduleForEmergency(SpecialtyType type)
        {
            List<Appointment> appointmentsToReschedule = _appointmentRepository.FindByDoctorSpecialityBeforeDate(type, DateTime.Now.AddDays(1));
            List<EmergencyReschedule> appointmentsAndRescheduleDate = (from app in
                appointmentsToReschedule where app.Type != AppointmentType.URGENCY let
                rescheduleTo = FindFirstDateToReschedule(app) select
                new EmergencyReschedule(app, rescheduleTo)).ToList();
            appointmentsAndRescheduleDate.Sort((x, y) =>
                x.RescheduledDate.Start.CompareTo(y.RescheduledDate.Start));
            return appointmentsAndRescheduleDate;
        }

        public void ValidateAppointmentInterval(TimeInterval interval)
        {
            _validator.SchedulingAppointmentInWrongTime(interval);
        }
        
        public List<Appointment> GetAppointmentsForThisWeek()
        {
            List<Appointment> appointmentsToReturn = _appointmentRepository.GetAppointmentsForThisWeek();
            appointmentsToReturn.Sort((app1, app2) => DateTime.Compare(app1.Time.Start, app2.Time.Start));
            return appointmentsToReturn;
        }
    }
}