/***********************************************************************
 * Module:  MedicalRecordsRepository.cs
 * Author:  Darko
 * Purpose: Definition of the Class Repository.MedicalRecordsRepository
 ***********************************************************************/

using DataHandler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Repository
{
    public class MedicalRecordsRepository
    {
        public ObservableCollection<MedicalRecord> medicalRecords;
        public List<Patient> patients;
        public DataHandler.PatientDataHandler patientDataHandler;
        public DataHandler.MedicalRecordDataHandler medicalRecordDataHandler;

        public MedicalRecordsRepository(PatientDataHandler patientHandler, MedicalRecordDataHandler recordDataHandler)
        {
            medicalRecords = new ObservableCollection<MedicalRecord>();
            patientDataHandler = patientHandler;
            medicalRecordDataHandler = recordDataHandler;
            LoadDataFromFiles();
        }

        public void Create(Model.MedicalRecord medicalRecord, Patient patient)
        {
            // TODO: implement
            this.medicalRecords.Add(medicalRecord);
            this.patients.Add(patient);
            LoadDataToFiles();
        }

        public MedicalRecord FindById(int id)
        {
            // TODO: implement
            foreach(MedicalRecord record in medicalRecords)
            {
                if (record._Id == id)
                    return record;
            }
            return null;
        }

        //dodato
        public ref ObservableCollection<MedicalRecord> ReadAll()
        {
            return ref medicalRecords;
        }

        public System.Collections.Generic.List<Patient> ReadAllPatients()
        {
            return patients;
        }

        public void Update(MedicalRecord medicalRecord, Patient patient)
        {
            // TODO: implement
            int medicalId = medicalRecord._Id;
            foreach(MedicalRecord record in medicalRecords)
            {
                if(record._Id == medicalId)
                {
                    record._BloodType = medicalRecord._BloodType;
                    record._Gender = medicalRecord._Gender;
                    record._MaritalStatus = medicalRecord._MaritalStatus;
                    break;
                }
            }
            int patientId = patient._Id;
            foreach(Patient patient2 in patients)
            {
                if(patient2._Id == patientId)
                {
                    patient2._BirthDate = patient._BirthDate;
                    patient2._Email = patient._Email;
                    patient2._Jmbg = patient._Jmbg;
                    patient2._Name = patient._Name;
                    patient2._PhoneNumber = patient._PhoneNumber;
                    patient2._Surname = patient._Surname;
                    LoadDataToFiles();
                    return;
                }
            }
            return;
        }

        public void DeleteById(int id)
        {
            // TODO: implement
            foreach(MedicalRecord record in medicalRecords)
            {
                if(record._Id == id)
                {
                    medicalRecords.Remove(record);
                    LoadDataToFiles();
                    return;
                }
            }
            return;
                
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            // TODO: implement
            medicalRecords.Remove(medicalRecord);
            LoadDataToFiles();
        }


        public MedicalRecord FindRecordByPatientId(int id)
        {
            foreach (MedicalRecord record in medicalRecords)
            {
                if (record._PatientId == id)
                {
                    return record;
                }
            }
            return null;
        }

        public Patient FindPatientById(int id)
        {
            foreach(Patient patient in patients)
            {
                if(patient._Id == id)
                {
                    return patient;
                }
            }
            return null;
        }

        public void LoadDataFromFiles()
        {
            this.medicalRecords = medicalRecordDataHandler.ReadAll();
            this.patients = patientDataHandler.ReadAll();
        }

        public void LoadDataToFiles()
        {
            medicalRecordDataHandler.Write(medicalRecords);
            patientDataHandler.Write(patients);
        }
    }
}