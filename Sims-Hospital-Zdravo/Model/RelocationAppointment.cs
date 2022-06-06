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
        private TimeInterval _scheduled;
        private RoomEquipment _roomEquipment;
        private int _id;
        private Room _fromRoom;
        private Room _toRoom;

        public RelocationAppointment(Room fromRoom, Room toRoom, TimeInterval ti, RoomEquipment re, int id)
        {
            this._fromRoom = fromRoom;
            this._toRoom = toRoom;
            this._scheduled = ti;
            this._roomEquipment = re;
            this._id = id;
        }

        public Room FromRoom
        {
            get { return _fromRoom; }
            set { this._fromRoom = value; }
        }

        public Room ToRoom
        {
            get { return _toRoom; }
            set { this._toRoom = value; }
        }

        public TimeInterval Scheduled
        {
            get { return _scheduled; }
            set { this._scheduled = value; }
        }

        public RoomEquipment RoomEquipment
        {
            get { return _roomEquipment; }

            set { this._roomEquipment = value; }
        }

        public int Id
        {
            get { return _id; }

            set { _id = value; }
        }

        public void Update(RelocationAppointment appointment)
        {
            RoomEquipment = appointment.RoomEquipment;
            Scheduled = appointment.Scheduled;
            FromRoom = appointment.FromRoom;
            ToRoom = appointment.ToRoom;
        }
    }
}