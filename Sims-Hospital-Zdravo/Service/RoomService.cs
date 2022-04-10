/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using System;
using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
   public class RoomService
   {
        public RoomService(RoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }
      public void Create(Room room)
      {
         // TODO: implement
      }
      
      public List<Room> Read()
      {
         // TODO: implement
         return roomRepository.Read();
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
   
      public RoomRepository roomRepository;
   
   }
}