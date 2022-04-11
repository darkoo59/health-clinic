/***********************************************************************
 * Module:  MedicalRecordController.cs
 * Author:  Darko
 * Purpose: Definition of the Class Controller.MedicalRecordController
 ***********************************************************************/

using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class MedicalRecordController
   {

      public MedicalRecordController(MedicalRecordService recordService)
        {
            medicalRecordService = recordService;
        }
      public void Create(MedicalRecord medicalRecord, Patient patient)
      {
            // TODO: implement
            try
            {
                Validate(medicalRecord);
                medicalRecordService.Create(medicalRecord,patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
      }
      
      public MedicalRecord FindById(int id)
      {
         // TODO: implement
         return medicalRecordService.FindById(id);
      }
      
      public List<MedicalRecord> ReadAll()
      {
         // TODO: implement
         return medicalRecordService.ReadAll();
      }
      
      public void Update(MedicalRecord medicalRecord)
      {
            // TODO: implement
            medicalRecordService.Update(medicalRecord);
      }
      
      public void DeleteById(int id)
      {
            // TODO: implement
            medicalRecordService.DeleteById(id);
      }
      
      public void Delete(MedicalRecord medicalRecord)
      {
            // TODO: implement
            medicalRecordService.Delete(medicalRecord);
      }
      
      public MedicalRecord FindByPatient(Patient patient)
      {
            // TODO: implement
            return medicalRecordService.FindByPatient(patient);
      }

      private void Validate(MedicalRecord record)
      {

            if (medicalRecordService.FindById(record._Id) != null)
                throw new Exception("Id already exists");
            if (medicalRecordService.FindByPatient(record._Patient) != null)
                throw new Exception("Patient already have medical record");

      }

        public Service.MedicalRecordService medicalRecordService;
   
   }
}