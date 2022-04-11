/***********************************************************************
 * Module:  MedicalRecordController.cs
 * Author:  stjep
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
      
      public MedicalRecord FindByPatient(Patient patient)
      {
         // TODO: implement
         return null;
      }
   
      public Service.MedicalRecordService medicalRecordService;
   
   }
}