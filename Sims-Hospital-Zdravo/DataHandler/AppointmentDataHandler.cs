/***********************************************************************
 * Module:  RoomDataHandler.cs
 * Author:  Darko
 * Purpose: Definition of the Class DataHandler.RoomDataHandler
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;



namespace DataHandler
{
   public class AppointmentDataHandler
   {
      public ObservableCollection<Appointment> ReadAll()
      {
            string appointmentSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Appointment> appointments = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(appointmentSerialized);
            return appointments;
        }
      
      public void Write(ObservableCollection<Appointment> appointments)
      {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(appointments);
            System.IO.File.WriteAllText(Path, serialized);
        }
   
      private String Path = @"..\..\Resources\appointment.txt";
   
   }
}