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


        private void warehouseAlreadyExists(Room room)
        {
            if (room.Type == RoomType.WAREHOUSE && _roomService.FindByType(RoomType.WAREHOUSE) != null)
                throw new Exception("Warehouse already exists!");
        }

        private void IdAlreadyExists(Room room)
        {
            if (_roomService.FindById(room.Id) != null)
                throw new Exception("Id already exists!");
        }

        private void IdNotFound(int id)
        {
            if (_roomService.FindById(id) != null)
                throw new Exception("Id already exists!");
        }

        private void IdNotFound(Room room)
        {
            if (_roomService.FindById(room.Id) == null)
                throw new Exception("Id Not Found!");
        }

        private void RoomDoesntExist(Room room)
        {
            if (room == null)
                throw new Exception("Room doesn't exist!");
        }

        public void ValidateCreate(Room room)
        {
            warehouseAlreadyExists(room);
            IdAlreadyExists(room);
        }

        public void ValidateUpdate(Room room)
        {
            RoomDoesntExist(room);
            warehouseAlreadyExists(room);
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