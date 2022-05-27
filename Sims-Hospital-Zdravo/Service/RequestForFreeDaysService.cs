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
using System.Windows;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo.Model
{
    public class RequestForFreeDaysService
    {
        private RequestForFreeDaysRepository _requestForFreeDaysRepository;
        private AppointmentRepository _appointmentRepository;
        private RequestForFreeDaysValidator freeDaysValidator;

        public RequestForFreeDaysService(RequestForFreeDaysRepository requestForFreeDaysRepository, AppointmentRepository appointmentRepository)
        {
            this._requestForFreeDaysRepository = requestForFreeDaysRepository;
            this._appointmentRepository = appointmentRepository;
            this.freeDaysValidator = new RequestForFreeDaysValidator(_appointmentRepository, requestForFreeDaysRepository);
        }

        public void Create(FreeDaysRequest request)
        {
            try
            {
                freeDaysValidator.ValidateSchedulingDaysOff(request);
                _requestForFreeDaysRepository.Create(request);
                MessageBox.Show("Request pending");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CreateUrgent(FreeDaysRequest request)
        {
            _requestForFreeDaysRepository.Create(request);
        }

        public void Delete(FreeDaysRequest request)
        {
            _requestForFreeDaysRepository.Delete(request);
        }

        public ref ObservableCollection<FreeDaysRequest> ReadAll()
        {
            return ref _requestForFreeDaysRepository.ReadAll();
        }
    }
}