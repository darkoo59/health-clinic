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
            try
            {
                Validate(room);
                roomService.Create(room);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
      }
      
      public List<Room> Read()
      {
         return roomService.Read();
      }
      
      public void Update(Room room)
      {
            roomService.Update(room);
      } 
      
      public void Delete(Room room)
      {
            roomService.Delete(room);
      }
      
      public Room FindById(int id)
      {
            return roomService.FindById(id);
      }
      
      public void DeleteById(int id)
      {
            roomService.DeleteById(id);
      }
   
      private void Validate(Room room)
      {

            if (room._Type == RoomType.WAREHOUSE && roomService.FindByType(RoomType.WAREHOUSE) != null) 
                throw new Exception("Warehouse already exists");
            if (roomService.FindById(room._Id) == null)
                throw new Exception("Id already exists");
            
      }
   
      public RoomService roomService;
   
   }
}