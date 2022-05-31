using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using Model;


 public enum RequestStatus
{
    ACCEPTED,
    PENDING,
    DENIED
}
namespace Sims_Hospital_Zdravo.Model
{
    public class FreeDaysRequest
    {

        private TimeInterval _timeInterval;
        private Doctor _doctor;
        private string _reasonForFreeDays;
        private RequestStatus _status;

        public FreeDaysRequest(TimeInterval timeInterval, Doctor doctor, string reasonForFreeDays,RequestStatus status)
        {
            this._timeInterval = timeInterval;
            this._doctor = doctor;
            this._reasonForFreeDays = reasonForFreeDays;
            this._status = status;
        }


        public TimeInterval TimeInterval
        {
            get
            {
                return _timeInterval;
            }
            set
            {
                if(value!= null)
                _timeInterval = value;
            }
        }

        public Doctor Doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                if (value != null)
                    _doctor = value;
            }
        }
        public string ReasonForfreeDays
        {
            get
            {
                return _reasonForFreeDays;
            }
            set
            {
                if (value != null)
                    _reasonForFreeDays = value;
            }
        }
        public RequestStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                    _status = value;
            }
        }
    }
}
