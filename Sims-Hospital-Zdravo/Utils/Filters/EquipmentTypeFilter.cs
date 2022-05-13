using System.Collections.Generic;
using System.Linq;
using Model;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Utils.Filters
{
    public class RoomEquipmentTypeFilter : IFilter<RoomEquipment>
    {
        private EquipmentType equipmentType;

        public RoomEquipmentTypeFilter(EquipmentType equipmentType)
        {
            this.equipmentType = equipmentType;
        }


        public List<RoomEquipment> Filter(List<RoomEquipment> objects)
        {
            return objects.Where(roomEquipment => roomEquipment.Equipment.Type == equipmentType).ToList();
        }
    }
}