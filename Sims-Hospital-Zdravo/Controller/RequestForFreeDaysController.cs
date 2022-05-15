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
    internal class RequestForFreeDaysController
    {
        private RequestForFreeDaysService requestForFreeDaysService;

        public RequestForFreeDaysController(RequestForFreeDaysService requestForFreeDaysService)
        {
            this.requestForFreeDaysService = requestForFreeDaysService;
        }

        public void Create(FreeDaysRequest request)
        {
            requestForFreeDaysService.Create(request);
        }

        public void Delete(FreeDaysRequest request)
        {
            requestForFreeDaysService.Delete(request);
        }

        public ref  ObservableCollection<FreeDaysRequest> ReadAll()
        {
            return  ref requestForFreeDaysService.ReadAll();
        }



    }
}
