using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IMedicineRepository : IGenericCRUD<Medicine>
    {

        List<Medicine> FindByStatus(MedicineStatus medicineStatus);
        Medicine FindById(int id);


    }
}
