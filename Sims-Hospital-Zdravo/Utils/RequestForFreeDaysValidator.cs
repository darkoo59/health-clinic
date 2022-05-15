using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using Model;
using Sims_Hospital_Zdravo;
using Sims_Hospital_Zdravo.Service;
using Sims_Hospital_Zdravo.Repository;
using Repository;
using System.Collections.ObjectModel;
namespace Sims_Hospital_Zdravo.Utils
{
    public class RequestForFreeDaysValidator
    {
        private AppointmentRepository appointmentRepository;
        private FreeDaysRequest request;
        private RequestForFreeDaysRepository requestForFreeDaysRepository;


        public RequestForFreeDaysValidator(AppointmentRepository appointmentRepository, FreeDaysRequest request,RequestForFreeDaysRepository requestForFreeDaysRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.requestForFreeDaysRepository = requestForFreeDaysRepository;
        }

        public  void ValidateIfDoctorIsFree(FreeDaysRequest request, AppointmentRepository appointmentRepository)
        {

            


        }
        public ObservableCollection<TimeInterval> GetAllAppointmentsBetweenDatesPlannedForVacation(FreeDaysRequest request, AppointmentRepository appointmentRepository)
        {
            var timeIntervals = appointmentRepository.GetTimeIntervalsForDoctor(request._Doctor._Id);
            var takenTimeIntervals = (ObservableCollection<TimeInterval>)timeIntervals.Where(i => i.Start.Date >= request._TimeInterval.Start.Date && i.Start.Date <= request._TimeInterval.End.Date);
            return takenTimeIntervals;
        }

        public void CheckIfIsTooLateForSchedulingVacation(FreeDaysRequest request)
        {
            DateTime dateTime = DateTime.Now;

            if( ( request._TimeInterval.Start.Date - dateTime.Date).Days <=2)
            {
                throw new Exception("Days off can't be scheduled two days before the start of time period");
            }

        }

        public void AnotherDoctorWithSameSpecialtyOnVacation(FreeDaysRequest request)
        {
            var SpecialistOnVacation = requestForFreeDaysRepository.RequestPendinfOrApproved(request._Doctor);
            if( SpecialistOnVacation != null)
            {
                throw new Exception("Cant schedule days off due to another specialist is taken specified interval ");
            }
        }

    }
}
