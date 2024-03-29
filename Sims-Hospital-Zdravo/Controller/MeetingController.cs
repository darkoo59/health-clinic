﻿using Model;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Controller
{
    public class MeetingController
    {
        private MeetingService _meetingService;

        public MeetingController()
        {
            this._meetingService = new MeetingService();
        }

        public void Create(Meeting meeting)
        {
            _meetingService.Create(meeting);
        }

        public void CreateMeetingWithNotifying(Meeting meeting, List<Notification> notifications)
        {
            _meetingService.CreateMeetingWithNotifying(meeting, notifications);
        }

        public List<Room> ReadAllRooms()
        {
            return _meetingService.ReadAllRooms();
        }

        public List<User> ReadAllPotentionalAttendees()
        {
            return _meetingService.ReadAllPotentionalAttendees();
        }
    }
}
