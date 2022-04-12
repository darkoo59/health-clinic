using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Utils
{
    class RoomValidator
    {
        private RoomService roomService;
        private Room room;

        public RoomValidator(RoomService roomService, Room room)
        {
            this.roomService = roomService;
            this.room = room;
        }


        private void warehouseAlreadyExists() {
            if (room._Type == RoomType.WAREHOUSE && roomService.FindByType(RoomType.WAREHOUSE) != null)
                throw new Exception("Warehouse already exists!");
        }

        private void IdAlreadyExists()
        {
            if (roomService.FindById(room._Id) != null)
                throw new Exception("Id already exists!");
        }

        private void IdNotFound(int id)
        {
            if (roomService.FindById(id) != null)
                throw new Exception("Id already exists!");

        }

        private void IdNotFound()
        {
            if (roomService.FindById(room._Id) == null)
                throw new Exception("Id Not Found!");
        }

        private void RoomDoesntExist()
        {
            if (room == null)
                throw new Exception("Room doesn't exist!");
        }

        public void ValidateCreate()
        {
            warehouseAlreadyExists();
            IdAlreadyExists();
        }

        public void ValidateUpdate()
        {
            RoomDoesntExist();
            warehouseAlreadyExists();
        }

        public void ValidateDelete()
        {
            RoomDoesntExist();
            IdNotFound();
        }

        public void ValidateDelete(int id)
        {
            IdNotFound(id);
        }
    }
}
