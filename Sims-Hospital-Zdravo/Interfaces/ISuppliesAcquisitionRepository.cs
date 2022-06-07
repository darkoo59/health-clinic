using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface ISuppliesAcquisitionRepository:IGenericCRUD<SuppliesAcquisition>
    {
        SuppliesAcquisition FindById(int id);
    }
}