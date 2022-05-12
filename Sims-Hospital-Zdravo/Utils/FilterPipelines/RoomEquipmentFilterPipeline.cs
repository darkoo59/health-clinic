using System.Collections.Generic;
using Model;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Utils.FilterPipelines
{
    public class RoomEquipmentFilterPipeline : IFilterPipeline<RoomEquipment>
    {
        public RoomEquipmentFilterPipeline()
        {
            filters = new List<IFilter<RoomEquipment>>();
        }

        public override List<RoomEquipment> FilterAll(List<RoomEquipment> objects)
        {
            List<RoomEquipment> filteredEquipment = new List<RoomEquipment>(objects);
            foreach (var filter in filters)
            {
                filteredEquipment = filter.Filter(filteredEquipment);
            }

            return filteredEquipment;
        }

        public override IFilterPipeline<RoomEquipment> AddFilter(IFilter<RoomEquipment> filter)
        {
            filters.Add(filter);
            return this;
        }
    }
}