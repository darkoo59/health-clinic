using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Service;
using Sims_Hospital_Zdravo.Service;

namespace Sims_Hospital_Zdravo.Controller
{
    public class SecretaryAppointmentController
    {
        private SecretaryAppointmentService secretaryAppointmenService;
        public SecretaryAppointmentController(SecretaryAppointmentService appService)
        {
            this.secretaryAppointmenService = appService;
        }

        public ObservableCollection<Appointment>  ReadAllAppointmentsForDate(DateTime date)
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
    }
}