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
using System.Collections.ObjectModel;



namespace DataHandler
{
   public class AppointmentDataHandler
   {
      public ObservableCollection<Appointment> ReadAll()
      {
            string appointmentSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Appointment> appointments = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(appointmentSerialized);
            //foreach (Appointment app in appointments)
            //{
            //    Console.WriteLine(app._Id + "id");
            //    Console.WriteLine(app._Doctor._Id + "id dr");
            //    Console.WriteLine(app._Doctor._Name + "ime dr");
            //}
            return appointments;
            //return new ObservableCollection<Appointment>();
        }
      
      public void Write(ObservableCollection<Appointment> appointments)
      {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(appointments);
            System.IO.File.WriteAllText(Path, serialized);
        }
   
      private String Path = @"..\..\Resources\appointment.txt";
   
   }
}