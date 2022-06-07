using System.Collections.Generic;
using Model;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IExtendedCRUD<T> : IGenericCRUD<T>
    {
        List<TimeInterval> FindTakenIntervalsForRoom(int roomId);
        T FindById(int id);
    }
}