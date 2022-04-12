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

        public Service.MedicalRecordService medicalRecordService;
        public MedicalRecordValidator validator;
        public MedicalRecordController(MedicalRecordService recordService)
        {
            medicalRecordService = recordService;
            validator = new MedicalRecordValidator(medicalRecordService);
        }

        public void Create(MedicalRecord medicalRecord, Patient patient)
      {
            // TODO: implement
            validator.InsertValidation(medicalRecord,patient);
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
            validator.UpdateValidation(patient);
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
      
   
   }
}