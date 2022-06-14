using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public  class MedicalEquipmentViewModel:BindableBase
    {

        private List<MedicalEquipment> _medicalEquipment;

        public MedicalEquipmentViewModel()
        {
            LoadMedicalEquiment();
        }
        public List<MedicalEquipment> MedicalEquipment
        {
            get
            {
                return _medicalEquipment;
            }
            set
            {
                _medicalEquipment = value;
            }
        }
        public void LoadMedicalEquiment()
        {
            List<MedicalEquipment> medEq = new List<MedicalEquipment>();
            medEq.Add(new MedicalEquipment("X-ray"));
            medEq.Add(new MedicalEquipment("Defirbillator"));
            medEq.Add(new MedicalEquipment("EKG-machine"));
            medEq.Add(new MedicalEquipment("Patient monitors"));
            _medicalEquipment = medEq;

        }



    }
}
