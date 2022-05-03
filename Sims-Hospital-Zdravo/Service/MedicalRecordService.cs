/***********************************************************************
 * Module:  MedicalRecordService.cs
 * Author:  Darko
 * Purpose: Definition of the Class Service.MedicalRecordService
 ***********************************************************************/

using Model;
using Repository;
using Sims_Hospital_Zdravo.Repository;
using Sims_Hospital_Zdravo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Sims_Hospital_Zdravo.Model;

namespace Service
{
   public class MedicalRecordService
   {

        public Repository.MedicalRecordsRepository medicalRecordRepository;
        public PatientRepository patientRepository;
        public AllergensRepository allergensRepository;
        public MedicalRecordValidator validator;

        public MedicalRecordService(MedicalRecordsRepository medicalRepo, PatientRepository patientRepo, AllergensRepository alergRepo)
        {
            medicalRecordRepository = medicalRepo;
            patientRepository = patientRepo;
            allergensRepository = alergRepo;
            validator = new MedicalRecordValidator(this);
        }
      public void Create(MedicalRecord medicalRecord, Patient patient)
      {
            // TODO: implement
            validator.InsertValidation(patient._Jmbg);
            medicalRecordRepository.Create(medicalRecord);
            patientRepository.Create(patient);
         return;
      }
      
      public MedicalRecord FindById(int id)
      {
            // TODO: implement
            return medicalRecordRepository.FindById(id);
      }


       public ref ObservableCollection<MedicalRecord> ReadAll()
      {
         // TODO: implement
         return ref medicalRecordRepository.ReadAll();
      }
      
      public void Update(MedicalRecord medicalRecord, Patient patient)
      {
            // TODO: implement
            validator.UpdateValidation(patient._Jmbg);
            patientRepository.Update(patient);
            medicalRecordRepository.Update(medicalRecord);
         return;
      }
      
      public void DeleteById(int id)
      {
            // TODO: implement
            medicalRecordRepository.DeleteById(id);
      }
      
      public void Delete(MedicalRecord medicalRecord)
      {
            // TODO: implement
            medicalRecordRepository.Delete(medicalRecord);
      }
      
        public Patient FindPatientById(int id)
        {
            return medicalRecordRepository.FindPatientById(id);
        }

        public List<String> ReadAllAllergens()
        {
            return allergensRepository.ReadAll();
        }

        public int GenerateId()
        {
            ObservableCollection<MedicalRecord> medicalRecords = medicalRecordRepository.ReadAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach(MedicalRecord record in medicalRecords)
            {
                ids.Add(record._Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;

        }

        public int GenreatePatientId()
        {
            ObservableCollection<Patient> patients = patientRepository.ReadAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Patient patient in patients)
            {
                ids.Add(patient._Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }

        public void CreatePrescription(Prescription prescription)
        {

        }

   
   }
}