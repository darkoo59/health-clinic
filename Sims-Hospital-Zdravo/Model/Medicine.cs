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
            //private string dosage;
            //private int numberOfDays;

            public Medicine(string name, string strength, string allergens,string description)
            {
                this.name = name;
                this.allergens = allergens;
                this.description = description;
                this.strength = strength;
                //this.dosage = dosage;
                //this.numberOfDays = numberOfDays;
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
        //public string _Dosage
        //{
        //    get
        //    {
        //        return dosage;
        //    }
        //    set
        //    {
        //        dosage = value;
        //    }
        //}
        //public int _NumberOfDays
        //{
        //    get
        //    {
        //        return numberOfDays;
        //    }
        //    set
        //    {
        //        numberOfDays = value;
        //    }
        //}



    }
    }







