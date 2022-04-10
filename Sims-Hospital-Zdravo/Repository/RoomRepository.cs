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
            this.roomDataHandler = roomDataHandler;
        } 
      public void Create(Room room)
      {
         // TODO: implement
      }
      
      public List<Room> Read()
      {
         // TODO: implement
         return roomDataHandler.ReadAll();
      }
      
      public void Update(Room room)
      {
         // TODO: implement
      }
      
      public void Delete(Room room)
      {
         // TODO: implement
      }
      
      public Room FindById(int id)
      {
         // TODO: implement
         return null;
      }
      
      public void DeleteById(int id)
      {
         // TODO: implement
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