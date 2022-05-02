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
            string roomsSerialized = System.IO.File.ReadAllText(_path);
            ObservableCollection<Room> rooms = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Room>>(roomsSerialized);
            return rooms;
        }

        public void Write(ObservableCollection<Room> rooms)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(rooms);
            System.IO.File.WriteAllText(_path, serialized);
        }

        private string _path = @"..\..\Resources\rooms.txt";
    }
}