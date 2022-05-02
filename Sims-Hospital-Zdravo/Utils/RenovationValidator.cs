using System;
using Controller;
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
        private TimeSchedulerService _timeSchedulerService;

        public RenovationValidator(RoomRepository roomRepository, RenovationRepository renovationRepository,
            TimeSchedulerService timeSchedulerService)
        {
            this.renovationRepository = renovationRepository;
            this.roomRepository = roomRepository;
            this._timeSchedulerService = timeSchedulerService;
        }

        private void ValidateRoomTaken(Room room, TimeInterval ti)
        {
            if (!_timeSchedulerService.IsRoomFreeInDateInterval(room.Id, ti))
            {
                throw new Exception("Room taken in given interval");
            }
        }

        private void ValidateRoomExists(Room room)
        {
            if (room == null) throw new Exception("Room not selected!");
            foreach (Room rm in roomRepository.ReadAll())
            {
                if (rm.Id == room.Id)
                {
                    return;
                }
            }

            throw new Exception("Room with given Id doesn't exist!");
        }

        private void ValidateDateCorrect(TimeInterval ti)
        {
            if (ti.Start > ti.End)
            {
                throw new Exception("End date should be later than start date!");
            }
        }

        public void ValidateRenovation(Room room, TimeInterval ti)
        {
            ValidateRoomExists(room);
            ValidateRoomTaken(room, ti);
        }
    }
}