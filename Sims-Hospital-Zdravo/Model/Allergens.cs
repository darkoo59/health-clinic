using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    class Allergens
    {
        private List<String> allergens;

        public Allergens()
        {
            allergens = new List<String>();
        }

        private List<String> _Allergens { get; set; }
    }
}
