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
    public class EquipmentDataHandler
    {
        public List<Equipment> ReadAll()
        {
            string equipmentSerialized = System.IO.File.ReadAllText(_path);
            List<Equipment> equipment = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Equipment>>(equipmentSerialized);
            return equipment;
        }

        public void Write(List<Equipment> equipment)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(equipment);
            System.IO.File.WriteAllText(_path, serialized);
        }

        private string _path = @"..\..\Resources\equipment.txt";
    }
}