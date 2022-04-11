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
        public RoomRepository(RoomDataHandler roomDataHandler) 
        {
            this.rooms = new List<Room>();
            this.roomDataHandler = roomDataHandler;
        } 
      public void Create(Room room)
      {
            Console.WriteLine(room);
            this.rooms.Add(room);
      }
      
      public List<Room> Read()
      {
         // TODO: implement
         return this.rooms;
      }
      
      public void Update(Room room)
      {
            int id = room._Id;
            foreach (Room rm in rooms) 
            {
                if (id == rm._Id) {
                    rm._Id = room._Id;
                    rm._Floor = room._Floor;
                    rm._Type = room._Type;
                    break;
                }
            }
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

        public Room FindByType(RoomType type) { 
            foreach(Room room in rooms)
            {
                if (room._Type == type) return room;
            }

            return null;
        } 

      public void loadData() 
      {
            this.rooms = roomDataHandler.ReadAll();  
      }
   
      public DataHandler.RoomDataHandler roomDataHandler;
      public List<Room> rooms;
      
      /// <pdGenerated>default getter</pdGenerated>
   
   }
}