using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Collections.ObjectModel;
namespace Service
{
    public class PatientMedicalRecordService
    {
        private MedicalRecordsRepository _medicalRecordsRepository;
        private PatientRepository _patientRepository;
        private ObservableCollection<Patient> patients;
        private List<MedicalRecord> _records;


        public PatientMedicalRecordService()
        {
            this._medicalRecordsRepository = new MedicalRecordsRepository();
            this._patientRepository = new PatientRepository();
            
        }
    

        public MedicalRecord FindMedicalRecordByPatient(Patient patient)

        {
            
            _records = _medicalRecordsRepository.FindAll();
            foreach(MedicalRecord record in _records)
            {
                if (record.Patient.Id.Equals(patient.Id))
                {
                    return record;
                    
                }

            }
            return null;
        }
        public Patient FindPatientById(int id) 
        {
            return _patientRepository.FindPatientById(id);
        }

    }
}
