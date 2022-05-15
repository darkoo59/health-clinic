using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
     public class Allergens
    {
        private List<String> allergens;
        private List<String> medicalAllergens;

        public Allergens()
        {
            allergens = new List<String>();
            medicalAllergens = new List<string>();
        }

        public List<String> _Allergens { get; set; }

        public List<String> _MedicalAllergens { get; set; }
    }
}
