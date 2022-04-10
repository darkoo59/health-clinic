/***********************************************************************
 * Module:  MedicalRecordService.cs
 * Author:  stjep
 * Purpose: Definition of the Class Service.MedicalRecordService
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;

namespace Service
{
   public class MedicalRecordService
   {
      public int Create(MedicalRecord medicalRecord)
      {
         // TODO: implement
         return 0;
      }
      
      public MedicalRecord FindById(int id)
      {
         // TODO: implement
         return null;
      }
      
      public List<MedicalRecord> FindAll()
      {
         // TODO: implement
         return null;
      }
      
      public MedicalRecord Update(MedicalRecord medicalRecord)
      {
         // TODO: implement
         return null;
      }
      
      public void DeleteById(int id)
      {
         // TODO: implement
      }
      
      public void Delete(MedicalRecord medicalRecord)
      {
         // TODO: implement
      }
      
      public List<MedicalRecord> FindByPatient(Patient patient)
      {
         // TODO: implement
         return null;
      }
   
      public Repository.MedicalRecordsRepository medicalRecordRepository;
   
   }
}