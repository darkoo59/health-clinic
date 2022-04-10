/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using System;

namespace Repository
{
   public class RoomRepository
   {
      public void Create(Model.Room room)
      {
         // TODO: implement
      }
      
      public Model.Room Read()
      {
         // TODO: implement
         return null;
      }
      
      public void Update(Model.Room room)
      {
         // TODO: implement
      }
      
      public void Delete(Model.Room room)
      {
         // TODO: implement
      }
      
      public Model.Room FindById(int id)
      {
         // TODO: implement
         return null;
      }
      
      public void DeleteById(int id)
      {
         // TODO: implement
      }
   
      public DataHandler.RoomDataHandler roomDataHandler;
      public System.Collections.ArrayList room;
      
      /// <pdGenerated>default getter</pdGenerated>
      public System.Collections.ArrayList GetRoom()
      {
         if (room == null)
            room = new System.Collections.ArrayList();
         return room;
      }
      
      /// <pdGenerated>default setter</pdGenerated>
      public void SetRoom(System.Collections.ArrayList newRoom)
      {
         RemoveAllRoom();
         foreach (Model.Room oRoom in newRoom)
            AddRoom(oRoom);
      }
      
      /// <pdGenerated>default Add</pdGenerated>
      public void AddRoom(Model.Room newRoom)
      {
         if (newRoom == null)
            return;
         if (this.room == null)
            this.room = new System.Collections.ArrayList();
         if (!this.room.Contains(newRoom))
            this.room.Add(newRoom);
      }
      
      /// <pdGenerated>default Remove</pdGenerated>
      public void RemoveRoom(Model.Room oldRoom)
      {
         if (oldRoom == null)
            return;
         if (this.room != null)
            if (this.room.Contains(oldRoom))
               this.room.Remove(oldRoom);
      }
      
      /// <pdGenerated>default removeAll</pdGenerated>
      public void RemoveAllRoom()
      {
         if (room != null)
            room.Clear();
      }
   
   }
}