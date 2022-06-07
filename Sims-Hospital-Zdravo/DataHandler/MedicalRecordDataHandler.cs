/***********************************************************************
 * Module:  RoomDataHandler.cs
 * Author:  Darko
 * Purpose: Definition of the Class DataHandler.RoomDataHandler
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataHandler
{
   public class MedicalRecordDataHandler
   {
      public List<MedicalRecord> ReadAll()
      {
            // TODO: implement
            string recordsSerialized = System.IO.File.ReadAllText(_path);
            List<MedicalRecord> records = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MedicalRecord>>(recordsSerialized);
            return records;
        }
      
      public void Write(List<MedicalRecord> medicalRecords)
      {
            // TODO: implement
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(medicalRecords);
            System.IO.File.WriteAllText(_path, serialized);
        }
   
      private String _path = @"..\..\Resources\medicalRecord.txt";

    }
}