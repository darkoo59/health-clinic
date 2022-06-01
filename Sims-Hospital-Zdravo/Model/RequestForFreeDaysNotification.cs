using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Model
{
    public class RequestForFreeDaysNotification
    {

        private FreeDaysRequest _request;

        public RequestForFreeDaysNotification(FreeDaysRequest request)
        {
            _request = request;
        }

        public FreeDaysRequest Request
        {
            get
            {
                return _request;
            }
            set
            {
                _request = value;
            }
        }

    }
}
