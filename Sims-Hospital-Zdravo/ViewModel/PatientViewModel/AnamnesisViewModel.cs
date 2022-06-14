using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.ViewModel.PatientViewModel
{
    public class AnamnesisViewModel
    {
        public string Anamnesis { get; set; }
        public AnamnesisViewModel(Anamnesis anamnesis)
        {
            if (anamnesis != null)this.Anamnesis = anamnesis.Anamensis;
        }
    }
}
