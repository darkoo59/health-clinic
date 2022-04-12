/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using DataHandler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Repository
{
    public class RoomRepository
    {
        public RoomDataHandler roomDataHandler;
        public ObservableCollection<Room> rooms;
        public RoomRepository(RoomDataHandler rmDataHandler)
        {
            rooms = new ObservableCollection<Room>();
            roomDataHandler = rmDataHandler;
            LoadDataFromFiles();

        }
        public void Create(Room room)
        {
            rooms.Add(room);
            LoadDataToFile();
        }

        public ref ObservableCollection<Room> ReadAll()
        {
            return ref rooms;
        }

        public void Update(Room room)
        {
            int id = room._Id;
            foreach (Room rm in rooms)
            {
                if (id == rm._Id)
                {
                    rm._Id = room._Id;
                    rm._Floor = room._Floor;
                    rm._Type = room._Type;
                    LoadDataToFile();
                    return;
                }
            }
        }

        public void Delete(Room room)
        {
            rooms.Remove(room);
            LoadDataToFile();
        }

        public Room FindById(int id)
        {
            foreach (Room room in rooms)
            {
                if (id == room._Id)
                {
                    return room;
                }
            }
            return null;
        }

        public void DeleteById(int id)
        {
            Room room = FindById(id);
            if (room != null) rooms.Remove(room);
        }

        public Room FindByType(RoomType type)
        {
            foreach (Room room in rooms)
            {
                if (room._Type == type)
                {
                    return room;
                }
            }

            return null;
        }

        public void LoadDataFromFiles()
        {
            rooms = roomDataHandler.ReadAll();
        }

        public void LoadDataToFile() 
        {
            roomDataHandler.Write(rooms);
        }




    }
}