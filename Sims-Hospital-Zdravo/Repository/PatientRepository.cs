using DataHandler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PatientRepository
    {
        public PatientDataHandler patientHandler;
        public ObservableCollection<Patient> patients;
        public MedicalRecordsRepository MedicalRecordsRepository;

        public PatientRepository(PatientDataHandler patientHandler)
        {
            this.patientHandler = patientHandler;
            this.patients = new ObservableCollection<Patient>();
            LoadDataFromFile();
        }

        public void Create(Patient patient)
        {
            patients.Add(patient);
            LoadDataToFile();
        }

        public void Delete(Patient patient)
        {
            patients.Remove(patient);
            LoadDataToFile();
        }

        public void Update(Patient newPatient)
        {
            foreach (Patient patient in patients)
            {
                if (patient._Id == newPatient._Id)
                {
                    patient._Name = newPatient._Name;
                    patient._Email = newPatient._Email;
                    patient._BirthDate = newPatient._BirthDate;
                    patient._Address = newPatient._Address;
                    patient._Jmbg = newPatient._Jmbg;
                    patient._Password = newPatient._Password;
                    patient._Surname = newPatient._Surname;
                    patient._Username = newPatient._Username;
                    LoadDataToFile();
                    return;
                }
            }

        }


        public Patient FindPatientById(int id)
        {
            foreach (Patient patient in patients)
            {
                if (patient._Id == id) return patient;
            }
            return null;
        }

        public ref ObservableCollection<Patient> ReadAll()
        {
            return ref this.patients;

        }

        //public MedicalRecord findMedicalRecordByPatient(Patient patient)
        //{
        //    foreach(MedicalRecord record in medicalRecords)

        //}
        public void LoadDataFromFile()
        {
            this.patients = patientHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            patientHandler.Write(patients);
        }


    }
}
