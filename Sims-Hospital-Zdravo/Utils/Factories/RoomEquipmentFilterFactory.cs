using System;
using Model;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils.FilterPipelines;
using Sims_Hospital_Zdravo.Utils.Filters;

namespace Sims_Hospital_Zdravo.Utils.Factories
{
    public class RoomEquipmentFilterFactory
    {
        public IFilterPipeline<RoomEquipment> CreateEquipmentFilterPipeline(bool showStatic, bool showConsumable)
        {
            if (showStatic && showConsumable)
            {
                return new RoomEquipmentFilterPipeline();
            }

            if (showStatic)
            {
                return (new RoomEquipmentFilterPipeline())
                    .AddFilter(new RoomEquipmentTypeFilter(EquipmentType.Static));
            }

            if (showConsumable)
            {
                return (new RoomEquipmentFilterPipeline())
                    .AddFilter(new RoomEquipmentTypeFilter(EquipmentType.Consumable));
            }

            return (new RoomEquipmentFilterPipeline())
                .AddFilter(new RoomEquipmentTypeFilter(EquipmentType.Consumable))
                .AddFilter(new RoomEquipmentTypeFilter(EquipmentType.Static));
        }
    }
}