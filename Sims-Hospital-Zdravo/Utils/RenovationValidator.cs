using System;
using Model;
using Repository;
using Service;
using Sims_Hospital_Zdravo.Repository;
using Sims_Hospital_Zdravo.Service;

namespace Sims_Hospital_Zdravo.Utils
{
    public class RenovationValidator
    {
        private RenovationRepository renovationRepository;
        private RoomRepository roomRepository;
        private TimeSchedulerService timeSchedulerService;

        public RenovationValidator(RoomRepository roomRepository, RenovationRepository renovationRepository,
            TimeSchedulerService timeSchedulerService)
        {
            this.renovationRepository = renovationRepository;
            this.roomRepository = roomRepository;
            this.timeSchedulerService = timeSchedulerService;
        }

        private void ValidateRoomTaken(Room room, TimeInterval ti)
        {
            if (timeSchedulerService.isRoomTakenInDateInterval(room._Id, ti))
            {
                throw new Exception("Room taken in give interval");
            }
        }

        public void ValidateRenovation(Room room, TimeInterval ti)
        {
            ValidateRoomTaken(room, ti);
        }
    }
}