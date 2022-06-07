using System.Collections.Generic;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IRenovationRepository : IExtendedCRUD<RenovationAppointment>
    {
        List<RenovationAppointment> FindByType(RenovationType type);
    }
}