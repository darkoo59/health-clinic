using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Repository;
using Sims_Hospital_Zdravo.Model;
using Repository;
using Model;
namespace Sims_Hospital_Zdravo.Service
{
    internal class RequestForFreeDaysService
    {
        private RequestForFreeDaysRepository _requestForFreeDaysRepository;
        private AppointmentRepository _appointmentRepository;

        public RequestForFreeDaysService(RequestForFreeDaysRepository requestForFreeDaysRepository,AppointmentRepository appointmentRepository)
        {
            this._requestForFreeDaysRepository = requestForFreeDaysRepository;
            this._appointmentRepository = appointmentRepository;
        }


        public void Create(FreeDaysRequest request)
        {
            _requestForFreeDaysRepository.Create(request);
        }

        public void Delete (FreeDaysRequest request)
        {
            _requestForFreeDaysRepository.Delete(request);
        }

        public  ref ObservableCollection<FreeDaysRequest> ReadAll()
        {
            return  ref _requestForFreeDaysRepository.ReadAll();
        }



        public ObservableCollection<TimeInterval> GetAllAppointmentsBetweenDatesPlannedForVacation(FreeDaysRequest request ,AppointmentRepository appointmentRepository)
        {
            var timeIntervals = appointmentRepository.GetTimeIntervalsForDoctor(request._Doctor._Id);
            var takenTimeIntervals =(ObservableCollection<TimeInterval>) timeIntervals.Where(i => i.Start.Date >= request._TimeInterval.Start.Date && i.Start.Date <= request._TimeInterval.End.Date);
            return  takenTimeIntervals;
        }

    }
}
