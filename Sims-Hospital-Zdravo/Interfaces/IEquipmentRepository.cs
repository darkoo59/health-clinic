using Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IEquipmentRepository : IGenericCRUD<Equipment>
    {
        Equipment FindById(int id);
        Equipment FindByName(string name);
    }
}