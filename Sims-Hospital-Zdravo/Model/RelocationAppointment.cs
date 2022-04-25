/***********************************************************************
 * Module:  RelocationAppointment.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.RelocationAppointment
 ***********************************************************************/

using Model;
using System;

namespace Model
{
    public class RelocationAppointment
    {
        private TimeInterval Scheduled;
        private RoomEquipment RoomEquipment;
        private int Id;
        private Room FromRoom;
        private Room ToRoom;
        private bool Allocated;

        public RelocationAppointment(Room fromRoom, Room toRoom, TimeInterval ti, RoomEquipment re, int id)
        {
            this.FromRoom = fromRoom;
            this.ToRoom = toRoom;
            this.Scheduled = ti;
            this.RoomEquipment = re;
            this.Id = id;
            this.Allocated = false;
        }

        public Room _FromRoom
        {
            get
            {
                return FromRoom;
            }
            set
            {
                if (this.FromRoom != value)
                    this.FromRoom = value;
            }
        }

        public Room _ToRoom
        {
            get
            {
                return ToRoom;
            }
            set
            {
                if (this.ToRoom != value)
                    this.ToRoom = value;
            }
        }

        public TimeInterval _Scheduled
        {
            get
            {
                return Scheduled;
            }
            set
            {
                if (this.Scheduled != value)
                    this.Scheduled = value;
            }
        }

        public bool _Allocated
        {
            get
            {
                return Allocated;
            }
            set
            {
                if (this.Allocated != value)
                    this.Allocated = value;
            }
        }

        public RoomEquipment _RoomEquipment
        {
            get
            {
                return RoomEquipment;
            }

            set
            {
                if (this.RoomEquipment != value)
                    this.RoomEquipment = value;
            }
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


    }
}