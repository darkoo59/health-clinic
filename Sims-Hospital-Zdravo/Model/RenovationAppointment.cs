using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    class RenovationAppointment
    {
        private int Id;
        private DateTime Start;
        private DateTime End;
        private Room Room;
        private String Description;


        public RenovationAppointment(DateTime start, DateTime end, Room room, String description, int id)
        {
            Start = start;
            End = end;
            Description = description;
            Room = room;
            Id = id;
        }


        public int _Id
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }

        public DateTime _Start
        {
            get
            {
                return Start;
            }
            set
            {
                Start = value;
            }
        }

        public DateTime _End
        {
            get
            {
                return End;
            }

             set
            {
                End = value;
            }
        }


        public Room _Room
        {
            get
            {
                return Room;
            }

            set
            {
                Room = value;
            }
        }

        public String _Description
        {
            get
            {
                return Description;
            }
            
            set
            {
                Description = value;
            }
        }
    }
}
