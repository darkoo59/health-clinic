/***********************************************************************
 * Module:  RoomDataHandler.cs
 * Author:  Darko
 * Purpose: Definition of the Class DataHandler.RoomDataHandler
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;

namespace DataHandler
{
   public class MedicalRecordDataHandler
   {
      public List<MedicalRecord> ReadAll()
      {
            // TODO: implement
            string recordsSerialized = System.IO.File.ReadAllText(Path);
            List<MedicalRecord> records = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MedicalRecord>>(recordsSerialized);
            return records;
        }
      
      public void Write(List<MedicalRecord> MedicalRecords)
      {
         // TODO: implement
      }
   
      private String Path = @"..\..\Resources\medicalRecord.txt";

    }
}