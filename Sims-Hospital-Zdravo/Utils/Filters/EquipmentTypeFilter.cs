using System.Collections.Generic;
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
            List<RoomEquipment> filteredEquipment = new List<RoomEquipment>();
            foreach (RoomEquipment roomEquipment in objects)
            {
                if (roomEquipment.Equipment.Type == equipmentType)
                {
                    filteredEquipment.Add(roomEquipment);
                }
            }

            return filteredEquipment;
        }
    }
}