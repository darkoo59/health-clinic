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
   public class RoomDataHandler
   {
      public ObservableCollection<Room> ReadAll()
      {
            string roomsSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Room> rooms = Newtonsoft.Json.JsonConvert.DeserializeObject <ObservableCollection<Room>>(roomsSerialized);
            return rooms;
      }
      
      public void Write(ObservableCollection<Room> rooms)
      {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(rooms);
            System.IO.File.WriteAllText(Path, serialized);      
      }
   
      private String Path = @"..\..\Resources\rooms.txt";
   
   }
}