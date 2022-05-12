using System.Collections.Generic;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public abstract class IFilterPipeline<T>
    {
        protected List<IFilter<T>> filters;

        public abstract List<T> FilterAll(List<T> objects);

        public abstract IFilterPipeline<T> AddFilter(IFilter<T> filter);
    }
}