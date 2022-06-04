using Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Controller
{
    public class MeetingController
    {
        private MeetingService _meetingService;

        public MeetingController(MeetingService meetingService)
        {
            this._meetingService = meetingService;
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
