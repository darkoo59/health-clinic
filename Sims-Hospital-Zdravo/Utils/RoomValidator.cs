using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Utils
{
    public class RoomValidator
    {
        private RoomService _roomService;


        public RoomValidator(RoomService roomService)
        {
            this._roomService = roomService;
        }


        private void WarehouseAlreadyExists(Room room)
        {
            if (room.Type == RoomType.WAREHOUSE && _roomService.FindByType(RoomType.WAREHOUSE) != null)
                throw new Exception("Warehouse already exists!");
        }

        private void RoomNumberAlreadyExists(Room room)
        {
            Room rm = _roomService.FindByRoomNumber(room.RoomNumber);
            if (rm != null)
            {
                if (room.Id == rm.Id && rm.RoomNumber == room.RoomNumber) return;
                throw new Exception("Room number already exists!");
            }
        }

        private void IdNotFound(int id)
        {
            if (_roomService.FindById(id) == null)
                throw new Exception("Id not found");
        }

        private void IdNotFound(Room room)
        {
            if (_roomService.FindById(room.Id) == null)
                throw new Exception("Id not Found!");
        }

        private void RoomDoesntExist(Room room)
        {
            if (room == null)
                throw new Exception("Given room is null!");
        }

        public void ValidateCreate(Room room)
        {
            WarehouseAlreadyExists(room);
            RoomNumberAlreadyExists(room);
        }

        public void ValidateUpdate(Room room)
        {
            RoomDoesntExist(room);
            WarehouseAlreadyExists(room);
            RoomNumberAlreadyExists(room);
        }

        public void ValidateDelete(Room room)
        {
            RoomDoesntExist(room);
            IdNotFound(room);
        }

        public void ValidateDelete(int id)
        {
            IdNotFound(id);
        }
    }
}