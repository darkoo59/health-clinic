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
            private int strength;
            private string dosage;
            private int numberOfDays;

            public Medicine(string name, int strength, string dosage, int numberOfDays)
            {
                this.name = name;

                this.strength = strength;
                this.dosage = dosage;
                this.numberOfDays = numberOfDays;
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
            public string _Dosage
            {
                get
                {
                    return dosage;
                }
                set
                {
                    dosage = value;
                }
            }
            public int _NumberOfDays
            {
                get
                {
                    return numberOfDays;
                }
                set
                {
                    numberOfDays = value;
                }
            }



        }
    }







