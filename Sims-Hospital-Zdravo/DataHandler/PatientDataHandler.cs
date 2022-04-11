/***********************************************************************
 * Module:  PatientDataHandler.cs
 * Author:  Darko
 * Purpose: Definition of the Class DataHandler.PatientDataHandler
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;

namespace DataHandler
{
   public class PatientDataHandler
   {
      public List<Patient> ReadAll()
      {
            // TODO: implement
            string patientsSerialized = System.IO.File.ReadAllText(Path);
            List<Patient> patients = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Patient>>(patientsSerialized);
            return patients;
        }
      
      public void Write(List<Patient> patients)
      {
         // TODO: implement
      }
   
      private String Path = @"..\..\Resources\patient.txt";

    }
}