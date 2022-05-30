using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Controller
{
    public class AnamnesisController
    {

        private AnamnesisService anamnesisService;

        public AnamnesisController( AnamnesisService anamnesisService)
        {
            this.anamnesisService = anamnesisService;
        }

        public void Create (Anamnesis anamnesis)
        {
            anamnesisService.Create (anamnesis);
        }

        public void Update (Anamnesis anamnesis)
        {
            anamnesisService.Update(anamnesis);
        }

        public ref ObservableCollection<Anamnesis> ReadAll()
        {
            return  ref anamnesisService.ReadAll();

        }
         public ObservableCollection<Anamnesis> findAnamnesisByDoctor(int id)
        {
            return anamnesisService.findAnamnesisByDoctor (id);

        }



    }
}
