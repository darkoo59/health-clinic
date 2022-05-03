using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class Medicine
    {
        private string name;
        private int id;
        private string allergens;
        private string description;
        private string strength;
        
        public Medicine(string name, string strength, string allergens, string description)
        {
            this.name = name;
            this.allergens = allergens;
            this.description = description;
            this.strength = strength;
            
        }
        


        public string _Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

       

        public string _Allergens
        {
            get
            {
                return allergens;
            }
            set
            {
                allergens = value;
            }
        }
        public int _Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
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
        
        

        public string _Strength
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







