using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;
using Repository;

namespace Utils
{
    class EquipmentTransferValidator
    {
        private RoomRepository roomRepo;
        private TimeSchedulerService timeSchedulerService;


        public EquipmentTransferValidator(RoomRepository roomRepo, TimeSchedulerService timeSchedulerService)
        {
            this.roomRepo = roomRepo;
            this.timeSchedulerService = timeSchedulerService;
        }


        private void HasEnoughEquipment(int roomId, int quantity, int equipmentId)
        {
            Room room = roomRepo.FindById(roomId);
            foreach (RoomEquipment re in room._RoomEquipment)
            {
                if (re._Equip._Id != equipmentId) continue;
                if (re._Quantity < quantity)
                {
                    throw new Exception("Not enough equipment for transfer!");
                }
            }
        }

        private void RoomExists(int roomId)
        {
            Room room = roomRepo.FindById(roomId);
            if (room == null)
                throw new Exception("Room number " + roomId + " does not exist!");
        }


        public void ValidateTransferFromRoom(int fromRoomId, int toRoomId, int equipmentId, int quantity,
            TimeInterval ti)
        {
            RoomExists(fromRoomId);
            RoomExists(toRoomId);
            HasEnoughEquipment(fromRoomId, quantity, equipmentId);
            ValidateRoomTaken(fromRoomId, ti);
            ValidateRoomTaken(toRoomId, ti);
        }

        private void ValidateRoomTaken(int roomId, TimeInterval ti)
        {
            if (!timeSchedulerService.IsRoomFreeInInterval(roomId, ti))
            {
                throw new Exception("Room " + roomId + " is not free in given interval!");
            }
        }
    }
}