using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Repository;

namespace Sims_Hospital_Zdravo.Service
{
    public class SecretaryAppointmentService
    {
        private AppointmentRepository appointmentRepository;
        public SecretaryAppointmentService(AppointmentRepository appRepo)
        {
            this.appointmentRepository = appRepo;
        }
        
        public ObservableCollection<Appointment>  ReadAllAppointmentsForDate(DateTime date)
        {
            return appointmentRepository.ReadAllAppointmentsForDate(date);
        }
        
        public void Delete(Appointment appointmentToDelete)
        {
            appointmentRepository.Delete(appointmentToDelete);
        }
        
        public void Update(Appointment appointmentToUpdate)
        {
            appointmentRepository.Update(appointmentToUpdate);
        }
    }
}