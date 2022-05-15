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

        private TimeInterval timeInterval;
        private Doctor doctor;
        private string reasonForFreeDays;
        private RequestStatus status;

        public FreeDaysRequest(TimeInterval timeInterval, Doctor doctor, string reasonForFreeDays, RequestStatus status)
        {
            this.timeInterval = timeInterval;
            this.doctor = doctor;
            this.reasonForFreeDays = reasonForFreeDays;
            this.status = status;
        }


        public TimeInterval _TimeInterval
        {
            get
            {
                return timeInterval;
            }
            set
            {
                if(value!= null)
                timeInterval = value;
            }
        }

        public Doctor _Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (value != null)
                    doctor = value;
            }
        }
        public string _ReasonForfreeDays
        {
            get
            {
                return reasonForFreeDays;
            }
            set
            {
                if (value != null)
                    reasonForFreeDays = value;
            }
        }
        public RequestStatus _Status
        {
            get
            {
                return status;
            }
            set
            {
                    status = value;
            }
        }
    }
}
