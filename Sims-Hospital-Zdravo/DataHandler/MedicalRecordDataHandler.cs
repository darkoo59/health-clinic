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
      public ObservableCollection<MedicalRecord> ReadAll()
      {
            // TODO: implement
            string recordsSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<MedicalRecord> records = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<MedicalRecord>>(recordsSerialized);
            return records;
        }
      
      public void Write(ObservableCollection<MedicalRecord> medicalRecords)
      {
            // TODO: implement
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(medicalRecords);
            System.IO.File.WriteAllText(Path, serialized);
        }
   
      private String Path = @"..\..\Resources\medicalRecord.txt";

    }
}