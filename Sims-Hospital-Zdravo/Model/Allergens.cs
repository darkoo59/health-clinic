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
        private List<String> _commonAllergens;
        private List<String> _medicalAllergens;

        public Allergens()
        {
            _commonAllergens = new List<String>();
            _medicalAllergens = new List<string>();
        }

        public List<String> CommonAllergens { get; set; }

        public List<String> MedicalAllergens { get; set; }
    }
}
