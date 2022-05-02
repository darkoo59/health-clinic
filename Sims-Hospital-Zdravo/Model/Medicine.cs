using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public  class Medicine
    {
        private string name;
        private int id;
        private int strength;

        public Medicine(int id, string name,  int strength)
        {
            this.id = id;
            this.name = name;
            this.strength = strength;
        }

        public string _Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
       
        public int _Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
            }
        }

    }
}
