using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class MeetingCreatedNotifications : Notification
    {
        private DateTime _meetingStart;
        private RoleType _roleType;
        private int _userId;

        public MeetingCreatedNotifications(string content, DateTime start,RoleType role,int userId) : base(content,-1)
        {
            this._meetingStart = start;
            this._roleType = role;
            this._userId = userId;
        }

        public DateTime MeetingStart
        {
            get { return _meetingStart; }
            set { _meetingStart = value; }
        }

        public RoleType RoleType
        {
            get { return _roleType; }
            set { _roleType = value; }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

    }
}
