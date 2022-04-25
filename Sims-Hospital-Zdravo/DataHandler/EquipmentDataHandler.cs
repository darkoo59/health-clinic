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
   public class EquipmentDataHandler
   {
        public ObservableCollection<Equipment> ReadAll()
        {
            string equipmentSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Equipment> equipment = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Equipment>>(equipmentSerialized);
            return equipment;
        }

        public void Write(ObservableCollection<Equipment> equipment)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(equipment);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private string Path = @"..\..\Resources\equipment.txt";

    }
}