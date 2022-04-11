/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using DataHandler;
using Model;
using System;
using System.Collections.Generic;
namespace Repository
{
    public class RoomRepository
    {
        public RoomDataHandler roomDataHandler;
        public List<Room> rooms;
        public RoomRepository(RoomDataHandler rmDataHandler)
        {
            rooms = new List<Room>();
            roomDataHandler = rmDataHandler;
            loadDataFromFiles();

        }
        public void Create(Room room)
        {
            this.rooms.Add(room);
            roomDataHandler.Write(rooms);
        }

        public List<Room> ReadAll()
        {
            return this.rooms;
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
                    return;
                }
            }

            throw new Exception("Room doesn't exist!");
        }

        public void Delete(Room room)
        {
            rooms.Remove(room);
        }

        public Room FindById(int id)
        {
            foreach (Room room in rooms)
            {
                if (id == room._Id) return room;
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
                if (room._Type == type) return room;
            }

            return null;
        }

        public void loadDataFromFiles()
        {
            rooms = roomDataHandler.ReadAll();
        }

        public void loadDataToFiles() 
        {
            roomDataHandler.Write(rooms);
        }




    }
}