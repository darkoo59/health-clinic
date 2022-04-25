using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;
using Repository;

namespace Sims_Hospital_Zdravo.Utils
{
    class EquipmentTransferValidator
    {
        private EquipmentRepository eqRepo;
        private RoomRepository roomRepo;

        public EquipmentTransferValidator(RoomRepository roomRepo, EquipmentRepository eqRepo)
        {
            this.eqRepo = eqRepo;
            this.roomRepo = roomRepo;
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

        public void validateTransferFromStorage(int roomId, int quantity, int equipmentId)
        {
            HasEnoughEquipment(roomId, quantity, equipmentId);
        }


    }
}
