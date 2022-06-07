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
        private int _id;
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

        public DateTime Start
        {
            get { return _start; }
            set{ this._start = value; }
        }

        public Room Room
        {
            get { return _room; }
            set { this._room = value; }
        }

        public List<User> OptionalAttendees
        {
            get { return _optionalAttendees; }
            set { this._optionalAttendees = value; }
        }

        public List<User> RequiredAttendees
        {
            get { return _requiredAttendees; }
            set { this._requiredAttendees = value; }
        }

        public int Id
        {
            get { return _id; }
            set { this._id = value; }
        }

    }
}
