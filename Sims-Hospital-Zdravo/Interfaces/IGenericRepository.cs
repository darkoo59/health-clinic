using System.Collections.Generic;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IGenericRepository<T>
    {
        List<T> FindAll();
    }
}