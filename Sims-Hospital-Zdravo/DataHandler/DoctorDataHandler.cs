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
    public class DoctorDataHandler
    {
        public List<Doctor> ReadAll()
        {
            string doctorSerialized = System.IO.File.ReadAllText(Path);
            List<Doctor> doctors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Doctor>>(doctorSerialized);
            return doctors;
        }

        public void Write(List<Doctor> doctors)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(doctors);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\doctor.txt";
    }
}