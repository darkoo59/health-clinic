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
        public DataHandler.MedicalRecordDataHandler medicalRecordDataHandler;
        public ObservableCollection<Patient> patients;

        public MedicalRecordsRepository(MedicalRecordDataHandler recordDataHandler)
        {
            medicalRecords = new ObservableCollection<MedicalRecord>();
            medicalRecordDataHandler = recordDataHandler;
            LoadDataFromFile();
        }

        public void Create(Model.MedicalRecord medicalRecord)
        {
            // TODO: implement
            this.medicalRecords.Add(medicalRecord);
            LoadDataToFile();
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

        public Patient FindPatientById(int id)
        {
            foreach(MedicalRecord medicalRecord in medicalRecords)
            {
                if (medicalRecord._Patient._Id == id)
                    return medicalRecord._Patient;
            }
            return null;
        }

        public ref ObservableCollection<MedicalRecord> ReadAll()
        {
            return ref medicalRecords;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            // TODO: implement
            foreach (MedicalRecord record in medicalRecords)
            {
                if (record._Id == medicalRecord._Id)
                {
                    record._BloodType = medicalRecord._BloodType;
                    record._Gender = medicalRecord._Gender;
                    record._MaritalStatus = medicalRecord._MaritalStatus;
                    record._Allergens = medicalRecord._Allergens;
                    record._Patient = medicalRecord._Patient;
                    LoadDataToFile();
                    break;
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
                    LoadDataToFile();
                    return;
                }
            }
            return;
                
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            // TODO: implement
            medicalRecords.Remove(medicalRecord);
            LoadDataToFile();
        }

        public void LoadDataFromFile()
        {
            medicalRecords = medicalRecordDataHandler.ReadAll();
        }

        

        public void LoadDataToFile()
        {
            medicalRecordDataHandler.Write(medicalRecords);
        }
    }
}