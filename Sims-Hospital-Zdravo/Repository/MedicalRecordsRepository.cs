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

        public MedicalRecordsRepository(MedicalRecordDataHandler recordDataHandler)
        {
            medicalRecords = new ObservableCollection<MedicalRecord>();
            medicalRecordDataHandler = recordDataHandler;
            LoadDataFromFiles();
        }

        public void Create(Model.MedicalRecord medicalRecord)
        {
            // TODO: implement
            this.medicalRecords.Add(medicalRecord);
         //   this.patients.Add(patient);
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

        public Patient FindPatientById(int id)
        {
            foreach(MedicalRecord medicalRecord in medicalRecords)
            {
                if (medicalRecord._Patient._Id == id)
                    return medicalRecord._Patient;
            }
            return null;
        }

        //dodato
        public ref ObservableCollection<MedicalRecord> ReadAll()
        {
            return ref medicalRecords;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            // TODO: implement
            int medicalId = medicalRecord._Id;
            foreach (MedicalRecord record in medicalRecords)
            {
                if (record._Id == medicalId)
                {
                    record._BloodType = medicalRecord._BloodType;
                    record._Gender = medicalRecord._Gender;
                    record._MaritalStatus = medicalRecord._MaritalStatus;
                    record._Patient = medicalRecord._Patient;
                    LoadDataToFiles();
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

        public void LoadDataFromFiles()
        {
            medicalRecords = medicalRecordDataHandler.ReadAll();
        }

        public void LoadDataToFiles()
        {
            medicalRecordDataHandler.Write(medicalRecords);
        }
    }
}