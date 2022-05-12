using System.Collections.Generic;
using Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IFilter<T>
    {
        List<T> Filter(List<T> objects);
    }
}