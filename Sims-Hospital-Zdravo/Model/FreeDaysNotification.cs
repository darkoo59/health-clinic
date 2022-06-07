using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class FreeDaysNotification : Notification
    { 
        private FreeDaysRequest _freeDaysRequest;
        private String _explanation;

        public FreeDaysNotification(string content, int id, FreeDaysRequest request) : base(content, id)
        {
            this._freeDaysRequest = request;
            this._explanation = content;
        }

        public FreeDaysRequest FreeDaysRequest
        {
            get { return _freeDaysRequest; }
            set { _freeDaysRequest = value; }
        }

        public String Explanation
        {
            get { return _explanation; }
            set { _explanation = value; }
        }
    }
}
