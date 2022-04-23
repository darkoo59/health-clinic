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
    public class MedicalRecordValidator
    {
        private MedicalRecordService recordService;


        public MedicalRecordValidator(MedicalRecordService service)
        {
            this.recordService = service;
        }

        private void medicalRecordIdAlreadyExist(MedicalRecord medicalRecord)
        {
            if (recordService.FindById(medicalRecord._Id) != null)
            {
                throw new Exception("Medical Id Already Exist!");
            }
        }

        private void patientIdAlreadyExist(Patient patient)
        {
            if (recordService.FindPatientById(patient._Id) != null)
            {
                throw new Exception("Patient Id Already Exist!");
            }
        }

        private void jmbgRightFormat(Patient patient)
        {
            String jmbg = patient._Jmbg;
            if(jmbg.Length != 13)
            {
                throw new Exception("JMBG must have 13 numbers!");
            }
            else
            {
                foreach (char c in jmbg)
                {
                    if (c < '0' || c > '9')
                        throw new Exception("JMBG can have numbers only!");
                }
            }
        }


        public void InsertValidation(MedicalRecord medicalRecord,Patient patient)
        {
            medicalRecordIdAlreadyExist(medicalRecord);
            patientIdAlreadyExist(patient);
            jmbgRightFormat(patient);
        }

        public void UpdateValidation(Patient patient)
        {
            jmbgRightFormat(patient);
        }
    }
}
