using System.Collections.Generic;
using Model;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IRequestForFreeDaysRepository:IGenericCRUD<FreeDaysRequest>
    {
        List<FreeDaysRequest> FindRequestByDoctorSpecialty(Doctor doctor);
        List<FreeDaysRequest> RequestPendingOrApproved(Doctor doctor);
        int GenerateId();
    }
}