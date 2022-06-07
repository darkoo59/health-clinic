using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Controller
{
    public class RequestForFreeDaysController
    {
        private RequestForFreeDaysService _requestForFreeDaysService;

        public RequestForFreeDaysController()
        {
            this._requestForFreeDaysService = new RequestForFreeDaysService();
        }

        public void Create(FreeDaysRequest request)
        {
            _requestForFreeDaysService.Create(request);
        }

        public void CreateUrgent(FreeDaysRequest request)
        {
            _requestForFreeDaysService.CreateUrgent(request);
        }

        public void UpdateRequestAndNotify(FreeDaysRequest request, Notification notification)
        {
            _requestForFreeDaysService.UpdateRequestAndNotify(request, notification);
        }


        public void Delete(int requestID)
        {
            _requestForFreeDaysService.Delete(requestID);
        }

        public List<FreeDaysRequest> FindAll()
        {
            return  _requestForFreeDaysService.ReadAll();
        }

        public List<FreeDaysRequest> ReadAllByDoctor(int doctorId)
        {
            return _requestForFreeDaysService.ReadAllByDoctor(doctorId);
        }

        public void SendRequestForFreeDaysWithNotifyingSecretary(FreeDaysRequest freeDaysRequest, Notification notification)
        {
            _requestForFreeDaysService.SendRequestForFreeDaysWithNotifyingSecretary(freeDaysRequest, notification);
        }


    }
}
