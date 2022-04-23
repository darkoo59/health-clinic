/***********************************************************************
 * Module:  PatientDataHandler.cs
 * Author:  Darko
 * Purpose: Definition of the Class DataHandler.PatientDataHandler
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataHandler
{
   public class PatientDataHandler
   {
      public ObservableCollection<Patient> ReadAll()
      {
            // TODO: implement
            string patientsSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Patient> patients = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Patient>>(patientsSerialized);
            return patients;
        }
      
      public void Write(ObservableCollection<Patient> patients)
      {
            // TODO: implement
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(patients);
            System.IO.File.WriteAllText(Path, serialized);
        }
   
      private String Path = @"..\..\Resources\patient.txt";

    }
}