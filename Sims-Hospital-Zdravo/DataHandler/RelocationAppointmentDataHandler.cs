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
    public class RelocationAppointmentDataHandler
    {
        public List<RelocationAppointment> ReadAll()
        {
            string appsSerialized = System.IO.File.ReadAllText(_path);
            List<RelocationAppointment> apps =
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<RelocationAppointment>>(appsSerialized);
            return apps;
        }

        public void Write(List<RelocationAppointment> apps)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(apps);
            System.IO.File.WriteAllText(_path, serialized);
        }

        private string _path = @"..\..\Resources\relocation_appointments.txt";
    }
}