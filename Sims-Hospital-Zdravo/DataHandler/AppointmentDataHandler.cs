/***********************************************************************
 * Module:  RoomDataHandler.cs
 * Author:  Darko
 * Purpose: Definition of the Class DataHandler.RoomDataHandler
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataHandler
{
   public class AppointmentDataHandler
   {
      public List<Appointment> ReadAll()
      {
            string appointmentSerialized = System.IO.File.ReadAllText(Path);
            List<Appointment> appointments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Appointment>>(appointmentSerialized);
            return appointments;
        }
      
      public void Write(List<Appointment> appointments)
      {
         // TODO: implement
      }
   
      private String Path = @"..\..\Resources\appointment.txt";
   
   }
}