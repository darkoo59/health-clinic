using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    internal class Specialist: Doctor
    {

        private String Specalty;
         
        public String _Specalty
        {
            get { 
                return Specalty;
            }    
            set { 
                Specalty = value; 
            }   
        }

    }
}
