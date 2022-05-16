/***********************************************************************
 * Module:  PatientDataHandler.cs
 * Author:  Darko
 * Purpose: Definition of the Class DataHandler.PatientDataHandler
 ***********************************************************************/

using Model;
using System;
using System.Collections.ObjectModel;

namespace DataHandler
{
    public class DoctorDataHandler
    {
        public ObservableCollection<Doctor> ReadAll()
        {
            string doctorSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Doctor> doctors = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Doctor>>(doctorSerialized);
            return doctors;
        }

        public void Write(ObservableCollection<Doctor> doctors)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(doctors);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\doctor.txt";
    }
}