using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Service
{
    class EquipmentTransferService
    {


        private EquipmentRepository equipmentRepository;
        private RoomRepository roomRepository;

        public EquipmentTransferService(EquipmentRepository equipmentRepository, RoomRepository roomRepository)
        {
            this.equipmentRepository = equipmentRepository;
            this.roomRepository = roomRepository;
        }
    }
}
