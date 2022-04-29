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
        private MedicalRecordsRepository MedicalRecordsRepository;
        private PatientRepository PatientRepository;
        private ObservableCollection<Patient> patients;
        private ObservableCollection<MedicalRecord> records;


        public PatientMedicalRecordService(MedicalRecordsRepository medRepo, PatientRepository patRepo)
        {
            this.MedicalRecordsRepository = medRepo;
            this.PatientRepository = patRepo;
            
        }
    

        public MedicalRecord findMedicalRecordByPatient(Patient patient)

        {
            Console.WriteLine(patient._Id);
            records = MedicalRecordsRepository.ReadAll();
            foreach(MedicalRecord record in records)
            {
                Console.WriteLine(record._Patient._Id);
                if (record._Patient._Id.Equals(patient._Id))
                {
                    return record;
                    
                }

            }
            return null;
        }

    }
}
