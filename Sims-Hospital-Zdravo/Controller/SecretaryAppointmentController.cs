using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Service;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Controller
{
    public class SecretaryAppointmentController
    {
        private SecretaryAppointmentService secretaryAppointmenService;
        public SecretaryAppointmentController(SecretaryAppointmentService appService)
        {
            this.secretaryAppointmenService = appService;
        }

        public void Create(Appointment appointment)
        {
            secretaryAppointmenService.Create(appointment);
        }

        public List<Appointment> ReadAllAppointmentsForDate(DateTime date)
        {
            return secretaryAppointmenService.ReadAllAppointmentsForDate(date);
        }

        public void Delete(Appointment appointmentToDelete)
        {
            secretaryAppointmenService.Delete(appointmentToDelete);
        }

        public void Update(Appointment appointmentToUpdate)
        {
            secretaryAppointmenService.Update(appointmentToUpdate);
        }

        public ObservableCollection<Patient> ReadAllPatients()
        {
            return secretaryAppointmenService.ReadAllPatients();
        }

        public List<Room> FindAvailableRoomsForInterval(TimeInterval startEndTime)
        {
            return secretaryAppointmenService.FindAvailableRoomsForInterval(startEndTime);
        }

        public List<Doctor> FindAvailableDoctorsForInterval(TimeInterval startEndTime)
        {
            return secretaryAppointmenService.FindAvailableDoctorsForInterval(startEndTime);
        }

        public ObservableCollection<Patient> FindAvailablePatientsForInterval(TimeInterval startEndTime)
        {
            return secretaryAppointmenService.FindAvailablePatientsForInterval(startEndTime);
        }

        public Appointment FindAndScheduleEmergencyAppointmentIfCan(Patient patient, SpecialtyType type)
        {
            return secretaryAppointmenService.FindAndScheduleEmergencyAppointmentIfCan(patient,type);
        }

        public List<EmergencyReschedule> FindAllAppointmentsToRescheduleForEmergency(SpecialtyType type)
        {
            return secretaryAppointmenService.FindAllAppointmentsToRescheduleForEmergency(type);
        }

        public int GenerateId()
        {
            return secretaryAppointmenService.GenerateId();
        }

        public void ValidateAppointmentInterval(TimeInterval interval)
        {
            secretaryAppointmenService.ValidateAppointmentInterval(interval);
        }

        public int GeneratePatientId()
        {
            return secretaryAppointmenService.GeneratePatientId();
        }
    }
}