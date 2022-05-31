using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using Model;
using Sims_Hospital_Zdravo;
using Sims_Hospital_Zdravo.Repository;
using Repository;
using System.Collections.ObjectModel;
namespace Sims_Hospital_Zdravo.Utils
{
    public class RequestForFreeDaysValidator
    {
        private AppointmentRepository appointmentRepository;
        private RequestForFreeDaysRepository requestForFreeDaysRepository;


        public RequestForFreeDaysValidator(AppointmentRepository appointmentRepository,RequestForFreeDaysRepository requestForFreeDaysRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.requestForFreeDaysRepository = requestForFreeDaysRepository;
            
        }

        public  void ValidateIfDoctorIsFree(FreeDaysRequest request, AppointmentRepository appointmentRepository)
        {
            if (GetAllAppointmentsBetweenDatesPlannedForVacation(request, appointmentRepository) != null)
            {
                throw new Exception("Vacation cant be scheduled due to scheduled appointments");
            }
        }

        public void ValidateSchedulingDaysOff(FreeDaysRequest request)
        {
            ValidateIfDoctorIsFree(request,appointmentRepository);
            CheckIfIsTooLateForSchedulingVacation(request);
            AnotherSpecialistOnVacation(request);
        }

        public List<TimeInterval> GetAllAppointmentsBetweenDatesPlannedForVacation(FreeDaysRequest request, AppointmentRepository appointmentRepository)
        {
            List<TimeInterval> timeIntervals = appointmentRepository.GetTimeIntervalsForDoctor(request._Doctor._Id);
            List<TimeInterval> takenTimeIntervals = timeIntervals.Where(i => i.Start.Date >= request._TimeInterval.Start.Date && i.Start.Date <= request._TimeInterval.End.Date).ToList();
            return takenTimeIntervals;
        }

        public void CheckIfIsTooLateForSchedulingVacation(FreeDaysRequest request)
        {
            DateTime dateTime = DateTime.Now;
            if (( request._TimeInterval.Start.Date - dateTime.Date).Days <= 2)
            {
                throw new Exception("Days off can't be scheduled two days before the start of time period");
            }
        }

        public ObservableCollection<TimeInterval> GetTimeIntervalsForSpecialistRequests(FreeDaysRequest request)
        {
            ObservableCollection<FreeDaysRequest> specialistFreeDaysRequests = requestForFreeDaysRepository.RequestPendingOrApproved(request._Doctor);
            ObservableCollection<TimeInterval> timeIntervals = new ObservableCollection<TimeInterval>();
            foreach(FreeDaysRequest freeDaysRequest in specialistFreeDaysRequests)
            {
              timeIntervals.Add(freeDaysRequest._TimeInterval);
            }
            return timeIntervals;
        }

        public bool CheckIfOverlapingOfDateTime(FreeDaysRequest request)
        {
            bool overlap = false;
            ObservableCollection<TimeInterval> timeIntervals = GetTimeIntervalsForSpecialistRequests(request);
            foreach (TimeInterval interval in timeIntervals)  
            {
                if(interval.Start.Date< request._TimeInterval.End.Date && request._TimeInterval.Start <interval.End.Date  )
                {
                    overlap = true;
                }
            }
            return overlap;
        }
        public void AnotherSpecialistOnVacation(FreeDaysRequest request)
        {
            if (CheckIfOverlapingOfDateTime(request))
            {
                throw new Exception("Overlapping of schedule vacation");
            }
        }
    }
}
