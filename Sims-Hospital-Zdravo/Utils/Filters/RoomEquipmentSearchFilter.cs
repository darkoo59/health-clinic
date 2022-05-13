using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Utils.Filters
{
    public class RoomEquipmentSearchFilter : IFilter<RoomEquipment>
    {
        private string parameter;

        public RoomEquipmentSearchFilter(string parameter)
        {
            this.parameter = parameter;
        }

        public List<RoomEquipment> Filter(List<RoomEquipment> objects)
        {
            return objects.Where(roomEquipment => roomEquipment.Equipment.Name.IndexOf(parameter, StringComparison.OrdinalIgnoreCase) != -1).ToList();
        }
    }
}