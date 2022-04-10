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
   public class RoomDataHandler
   {
      public List<Room> ReadAll()
      {
            string roomsSerialized = System.IO.File.ReadAllText(Path);
            List<Room> rooms = Newtonsoft.Json.JsonConvert.DeserializeObject <List<Room>>(roomsSerialized);
            return rooms;
      }
      
      public void Write(List<Room> rooms)
      {
         // TODO: implement
      }
   
      private String Path = @"..\..\..\Resources\rooms.txt";
   
   }
}