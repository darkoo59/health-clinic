using System.Collections.Generic;
using Model;

public enum RoomRenovationType
{
    SPLIT,
    JOIN
}

namespace Sims_Hospital_Zdravo.Model
{
    public class AdvancedRenovationAppointment : RenovationAppointment
    {
        public List<Room> rooms { get; }
        public RoomRenovationType roomRenovationType { get; set; }

        public AdvancedRenovationAppointment(TimeInterval time, Room room, string description, List<Room> rooms, RoomRenovationType roomRenovationType, int id) : base(time, room, description, RenovationType.ADVANCED, id)
        {
            this.rooms = rooms;
            this.roomRenovationType = roomRenovationType;
        }
    }
}