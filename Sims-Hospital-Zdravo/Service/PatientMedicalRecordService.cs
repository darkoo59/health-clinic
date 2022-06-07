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
        private ObservableCollection<MedicalRecord> _records;


        public PatientMedicalRecordService(MedicalRecordsRepository medRepo, PatientRepository patRepo)
        {
            this._medicalRecordsRepository = medRepo;
            this._patientRepository = patRepo;
            
        }
    

        public MedicalRecord FindMedicalRecordByPatient(Patient patient)

        {
            Console.WriteLine(patient.Id);
            _records = _medicalRecordsRepository.ReadAll();
            foreach(MedicalRecord record in _records)
            {
                Console.WriteLine(record.Patient.Id);
                if (record.Patient.Id.Equals(patient.Id))
                {
                    return record;
                    
                }

            }
            return null;
        }

    }
}
