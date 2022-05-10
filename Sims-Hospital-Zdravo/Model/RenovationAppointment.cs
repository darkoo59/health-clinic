using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum RenovationType
{
    ADVANCED,
    BASIC
}

namespace Sims_Hospital_Zdravo.Model
{
    public class RenovationAppointment
    {
        private int _id;
        private TimeInterval _time;
        private Room _room;
        private String _description;
        private RenovationType _type;


        public RenovationAppointment(TimeInterval time, Room room, String description, RenovationType type,
            int id)
        {
            this._time = time;
            _description = description;
            _room = room;
            _id = id;
            _type = type;
        }


        public int Id
        {
            get { return _id; }

            set { _id = value; }
        }

        public TimeInterval Time
        {
            get { return _time; }
            set { _time = value; }
        }


        public Room Room
        {
            get { return _room; }

            set { _room = value; }
        }

        public String Description
        {
            get { return _description; }

            set { _description = value; }
        }

        public RenovationType Type
        {
            get { return _type; }

            set { _type = value; }
        }

        public bool IsAdvancedRenovation()
        {
            return Type == RenovationType.ADVANCED;
        }
    }
}