using Model;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IAnamnesisRepository : IGenericCRUD<Anamnesis>
    {
        ObservableCollection<Anamnesis> FindAnamesisByPatient(MedicalRecord medicalRecord);
        ObservableCollection<Anamnesis> FindAnamnesisByDoctor(int id);



    }
}
