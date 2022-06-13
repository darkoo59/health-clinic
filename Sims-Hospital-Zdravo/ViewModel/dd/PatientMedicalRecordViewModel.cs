using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Model;
using Controller;

namespace Sims_Hospital_Zdravo.ViewModel.dd
{
    public  class PatientMedicalRecordViewModel
    {
        private MedicalRecord _medicalRecord;
        private MedicalRecordController _medicalRecordController;

        public PatientMedicalRecordViewModel(int id)
        {
            this._medicalRecordController = new MedicalRecordController();
            this._medicalRecord = _medicalRecordController.FindById(id);
        }

        public MedicalRecord MedicalRecord
        {
            get
            {
                return _medicalRecord;
            }
        }
    }
}
