using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IUpdateFilesObservable
    {
        void NotifyUpdated();
        void AddObserver(IUpdateFilesObserver observer);
        void RemoveObserver(IUpdateFilesObserver observer);
    }
}
