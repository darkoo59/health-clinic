namespace Sims_Hospital_Zdravo.DTO
{
    public class RoomEquipmentFilterDTO
    {
        public bool ShowStatic { get; set; }
        public bool ShowConsumable { get; set; }
        public string SearchParam { get; set; } = "";

        public RoomEquipmentFilterDTO(bool showStatic, bool showConsumable, string searchParam)
        {
            ShowStatic = showStatic;
            ShowConsumable = showConsumable;
            SearchParam = searchParam;
        }
    }
}