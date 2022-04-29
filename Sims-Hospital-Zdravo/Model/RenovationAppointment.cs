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
        private int Id;
        private TimeInterval Time;
        private Room Room;
        private String Description;
        private RenovationType Type;


        public RenovationAppointment(TimeInterval time, Room room, String description, RenovationType type,
            int id)
        {
            this.Time = time;
            Description = description;
            Room = room;
            Id = id;
            Type = type;
        }


        public int _Id
        {
            get { return Id; }

            set { Id = value; }
        }

        public TimeInterval _Time
        {
            get { return Time; }
            set { Time = value; }
        }


        public Room _Room
        {
            get { return Room; }

            set { Room = value; }
        }

        public String _Description
        {
            get { return Description; }

            set { Description = value; }
        }

        public RenovationType _Type
        {
            get { return Type; }

            set { Type = value; }
        }
    }
}