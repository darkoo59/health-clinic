using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Service;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Controller
{
    public class RequestForFreeDaysController
    {
        private RequestForFreeDaysService _requestForFreeDaysService;

        public RequestForFreeDaysController(RequestForFreeDaysService requestForFreeDaysService)
        {
            this._requestForFreeDaysService = requestForFreeDaysService;
        }

        public void Create(FreeDaysRequest request)
        {
            _requestForFreeDaysService.Create(request);
        }

        public void CreateUrgent(FreeDaysRequest request)
        {
            _requestForFreeDaysService.CreateUrgent(request);
        }


        public void Delete(FreeDaysRequest request)
        {
            _requestForFreeDaysService.Delete(request);
        }

        public ref  ObservableCollection<FreeDaysRequest> ReadAll()
        {
            return  ref _requestForFreeDaysService.ReadAll();
        }



    }
}
