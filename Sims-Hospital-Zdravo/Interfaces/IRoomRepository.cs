using System.Collections.Generic;
using Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IRoomRepository : IGenericCRUD<Room>
    {
        Room FindById(int id);
        Room FindByRoomNumber(string roomNumber);
        Room FindByType(RoomType type);
        List<RoomEquipment> FindAllEquipment();

        void RemoveMultiple(List<Room> rooms);
    }
}