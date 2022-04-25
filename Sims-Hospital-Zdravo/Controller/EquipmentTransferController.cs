using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Controller
{
    class EquipmentTransferController
    {


        private EquipmentTransferService equipmentTransferService;

        public EquipmentTransferController(EquipmentTransferService equipmentTransferService)
        {
            this.equipmentTransferService = equipmentTransferService;
        }
    }
}
