/***********************************************************************
 * Module:  RoomController.cs
 * Author:  stjep
 * Purpose: Definition of the Class Controller.RoomController
 ***********************************************************************/

using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class RoomController
   {

        public RoomController(RoomService roomService) {
            this.roomService = roomService;
        }
      public void Create(Room room)
      {
         // TODO: implement
      }
      
      public List<Room> Read()
      {
         // TODO: implement
         return roomService.Read();
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
   
      private void Validate(Room room)
      {
         // TODO: implement
      }
   
      public RoomService roomService;
   
   }
}