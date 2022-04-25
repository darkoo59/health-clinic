using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    class EquipmentTransferController
    {


        private EquipmentTransferService equipmentTransferService;

        public EquipmentTransferController(EquipmentTransferService equipmentTransferService)
        {
            this.equipmentTransferService = equipmentTransferService;
        }

        public void TransferFromWarehouse(int roomId, Equipment eq, int quantity, TimeInterval ti)
        {
            equipmentTransferService.TransferFromWarehouse(roomId, eq, quantity, ti);
        }

        public void MakeAppointmentFromWarehouse(int roomId, Equipment eq, int quantity, TimeInterval ti)
        {
            equipmentTransferService.MakeAppointmentFromWarehouse(roomId, eq, quantity, ti);
        }
    }
}
