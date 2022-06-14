using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public  class MedicalEquipment
    {

       private  string _medicalEquipmentType;
        
        public MedicalEquipment(string medicalequipment)
        {
            this._medicalEquipmentType = medicalequipment;
        }


        public string MedicalEquipmentType
        {
            get { return this._medicalEquipmentType; }
            set
            {
                this._medicalEquipmentType = value;
            }
        }
    }
}
