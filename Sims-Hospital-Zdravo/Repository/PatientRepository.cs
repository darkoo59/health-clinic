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

        public PatientRepository()
        {
            this.patientHandler = new PatientDataHandler();
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
                if (patient.Id == newPatient.Id)
                {
                    patient.Name = newPatient.Name;
                    patient.Email = newPatient.Email;
                    patient.BirthDate = newPatient.BirthDate;
                    patient.Address = newPatient.Address;
                    patient.Jmbg = newPatient.Jmbg;
                    patient._Password = newPatient._Password;
                    patient.Surname = newPatient.Surname;
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
                if (patient.Id == id) return patient;
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
