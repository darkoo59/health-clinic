/***********************************************************************
 * Module:  MedicalRecordService.cs
 * Author:  Darko
 * Purpose: Definition of the Class Service.MedicalRecordService
 ***********************************************************************/

using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
   public class MedicalRecordService
   {

        public MedicalRecordService(MedicalRecordsRepository medicalRepo)
        {
            medicalRecordRepository = medicalRepo;
        }
      public void Create(MedicalRecord medicalRecord, Patient patient)
      {
            // TODO: implement
            medicalRecordRepository.Create(medicalRecord,patient);
         return;
      }
      
      public MedicalRecord FindById(int id)
      {
            // TODO: implement
            return medicalRecordRepository.FindById(id);
      }
      
      public List<MedicalRecord> ReadAll()
      {
         // TODO: implement
         return medicalRecordRepository.ReadAll();
      }
      
      public void Update(MedicalRecord medicalRecord)
      {
            // TODO: implement
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
      
      public MedicalRecord FindByPatient(Patient patient)
      {
         // TODO: implement
         return medicalRecordRepository.FindByPatient(patient);
      }
   
      public Repository.MedicalRecordsRepository medicalRecordRepository;
   
   }
}