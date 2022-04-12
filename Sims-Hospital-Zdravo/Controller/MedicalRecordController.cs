/***********************************************************************
 * Module:  MedicalRecordController.cs
 * Author:  Darko
 * Purpose: Definition of the Class Controller.MedicalRecordController
 ***********************************************************************/

using Model;
using Service;
using Sims_Hospital_Zdravo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            MedicalRecordValidator validator = new MedicalRecordValidator(medicalRecordService, medicalRecord, patient);
            validator.InsertValidation();
            medicalRecordService.Create(medicalRecord, patient);
        }
      
      public MedicalRecord FindById(int id)
      {
         // TODO: implement
         return medicalRecordService.FindById(id);
      }

      public Patient findPatientById(int id)
        {
            return medicalRecordService.findPatientById(id);
        }


      public ref ObservableCollection<MedicalRecord> ReadAll()
      {
         // TODO: implement
         return ref medicalRecordService.ReadAll();
      }
      
      public void Update(MedicalRecord medicalRecord, Patient patient)
      {
            // TODO: implement
            medicalRecordService.Update(medicalRecord, patient);
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
      

      private void Validate(MedicalRecord record)
      {

            if (medicalRecordService.FindById(record._Id) != null)
                throw new Exception("Id already exists");
            if (medicalRecordService.FindByPatientId(record._PatientId) != null)
                throw new Exception("Patient already have medical record");

      }

        public Service.MedicalRecordService medicalRecordService;
   
   }
}