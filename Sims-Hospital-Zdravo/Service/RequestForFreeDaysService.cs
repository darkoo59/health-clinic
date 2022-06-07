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
        private IRequestForFreeDaysRepository _requestForFreeDaysRepository;
        private IAppointmentRepository _appointmentRepository;
        private INotificationRepository _notificationRepository;
        private RequestForFreeDaysValidator _freeDaysValidator;

        public RequestForFreeDaysService()
        {
            _requestForFreeDaysRepository = new RequestForFreeDaysRepository();
            _appointmentRepository = new AppointmentRepository();
            _notificationRepository = new NotificationRepository();
            _freeDaysValidator = new RequestForFreeDaysValidator(_appointmentRepository, _requestForFreeDaysRepository);
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

        public void Delete(int requestID)
        {
            _requestForFreeDaysRepository.DeleteById(requestID);
        }

        public List<FreeDaysRequest> ReadAll()
        {
            return _requestForFreeDaysRepository.FindAll();
            
        }

        public List<FreeDaysRequest> ReadAllByDoctor(int doctorId)
        {
            return _requestForFreeDaysRepository.ReadAllByDoctor(doctorId);
        }

        public void SendRequestForFreeDaysWithNotifyingSecretary(FreeDaysRequest freeDaysRequest, Notification notification)
        {
            Create(freeDaysRequest);
            _notificationRepository.Create(notification);
        }
    }
}