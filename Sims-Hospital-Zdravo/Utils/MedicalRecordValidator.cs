using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims_Hospital_Zdravo.Utils
{
    class MedicalRecordValidator
    {
        private MedicalRecordService recordService;
        private MedicalRecord medicalRecord;
        private Patient patient;

        public MedicalRecordValidator(MedicalRecordService service,MedicalRecord record,Patient patient)
        {
            this.recordService = service;
            this.medicalRecord = record;
            this.patient = patient;
        }

        public void medicalRecordIdAlreadyExist()
        {
            if (recordService.FindById(medicalRecord._Id) != null)
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("There is medical record with same id", "Medical record same id!", MessageBoxButton.OK);
                throw new Exception("Medical Id Already Exist!");
            }
        }

        public void patientIdAlreadyExist()
        {
            if (recordService.findPatientById(patient._Id) != null)
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("There is patient with same id", "Patient same id!", MessageBoxButton.OK);
                throw new Exception("Patient Id Already Exist!");
            }
        }

        public void InsertValidation()
        {
            medicalRecordIdAlreadyExist();
            patientIdAlreadyExist();
        }
    }
}
