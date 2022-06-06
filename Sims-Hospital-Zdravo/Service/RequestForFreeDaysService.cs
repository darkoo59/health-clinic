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
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo.Model
{
    public class RequestForFreeDaysService
    {
        private RequestForFreeDaysRepository _requestForFreeDaysRepository;
        private AppointmentRepository _appointmentRepository;
        private RequestForFreeDaysValidator _freeDaysValidator;
        private INotificationRepository _notificationRepository;

        public RequestForFreeDaysService(RequestForFreeDaysRepository requestForFreeDaysRepository, AppointmentRepository appointmentRepository)
        {
            _requestForFreeDaysRepository = requestForFreeDaysRepository;
            _appointmentRepository = appointmentRepository;
            _notificationRepository = new NotificationRepository();
            _freeDaysValidator = new RequestForFreeDaysValidator(_appointmentRepository, requestForFreeDaysRepository);
        }

        public void Create(FreeDaysRequest request)
        {
            _freeDaysValidator.ValidateSchedulingDaysOff(request);
            _requestForFreeDaysRepository.Create(request);
        }

        public void CreateUrgent(FreeDaysRequest request)
        {
            _requestForFreeDaysRepository.Create(request);
        }

        public void UpdateRequestAndNotify(FreeDaysRequest request, Notification notification)
        {
            _requestForFreeDaysRepository.Update(request);
            _notificationRepository.Create(notification);
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