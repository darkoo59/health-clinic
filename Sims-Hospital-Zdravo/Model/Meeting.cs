using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class Meeting
    {
        private DateTime _start;
        private Room _room;
        private List<User> _optionalAttendees;
        private List<User> _requiredAttendees;

        public Meeting(DateTime start, Room room, List<User> optionalAttendees, List<User> requiredAttendees)
        {
            _start = start;
            _room = room;
            _optionalAttendees = optionalAttendees;
            _requiredAttendees = requiredAttendees;
        }

        public DateTime Start { get; set; }

        public Room Room { get; set; }

        public List<User> OptionalAttendees { get; set; }

        public List<User> RequiredAttendees { get; set; }
    }
}
