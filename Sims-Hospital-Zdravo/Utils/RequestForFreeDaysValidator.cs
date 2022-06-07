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
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Utils
{
    public class RequestForFreeDaysValidator
    {
        private IAppointmentRepository appointmentRepository;
        private IRequestForFreeDaysRepository requestForFreeDaysRepository;


        public RequestForFreeDaysValidator(IAppointmentRepository appointmentRepository,IRequestForFreeDaysRepository requestForFreeDaysRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.requestForFreeDaysRepository = requestForFreeDaysRepository;
            
        }

        public  void ValidateIfDoctorIsFree(FreeDaysRequest request, IAppointmentRepository appointmentRepository)
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

        public List<TimeInterval> GetAllAppointmentsBetweenDatesPlannedForVacation(FreeDaysRequest request, IAppointmentRepository appointmentRepository)
        {
            List<TimeInterval> timeIntervals = appointmentRepository.GetTimeIntervalsForDoctor(request.Doctor._Id);
            List<TimeInterval> takenTimeIntervals = timeIntervals.Where(i => i.Start.Date >= request.TimeInterval.Start.Date && i.Start.Date <= request.TimeInterval.End.Date).ToList();
            return takenTimeIntervals;
        }

        public void CheckIfIsTooLateForSchedulingVacation(FreeDaysRequest request)
        {
            DateTime dateTime = DateTime.Now;
            if (( request.TimeInterval.Start.Date - dateTime.Date).Days <= 2)
            {
                throw new Exception("Days off can't be scheduled two days before the start of time period");
            }
        }

        public ObservableCollection<TimeInterval> GetTimeIntervalsForSpecialistRequests(FreeDaysRequest request)
        {
            List<FreeDaysRequest> specialistFreeDaysRequests = requestForFreeDaysRepository.RequestPendingOrApproved(request.Doctor);
            ObservableCollection<TimeInterval> timeIntervals = new ObservableCollection<TimeInterval>();
            foreach(FreeDaysRequest freeDaysRequest in specialistFreeDaysRequests)
            {
              timeIntervals.Add(freeDaysRequest.TimeInterval);
            }
            return timeIntervals;
        }

        public bool CheckIfOverlapingOfDateTime(FreeDaysRequest request)
        {
            bool overlap = false;
            ObservableCollection<TimeInterval> timeIntervals = GetTimeIntervalsForSpecialistRequests(request);
            foreach (TimeInterval interval in timeIntervals)  
            {
                if(interval.Start.Date< request.TimeInterval.End.Date && request.TimeInterval.Start <interval.End.Date  )
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
