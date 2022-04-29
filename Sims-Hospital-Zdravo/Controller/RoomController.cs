/***********************************************************************
 * Module:  RoomController.cs
 * Author:  stjep
 * Purpose: Definition of the Class Controller.RoomController
 ***********************************************************************/

using Model;
using Service;
using Sims_Hospital_Zdravo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Controller
{
    public class RoomController
    {
        public RoomService roomService;


        public RoomController(RoomService roomService)
        {
            this.roomService = roomService;
        }
        public void Create(Room room)
        {
            roomService.Create(room);
        }

        public ref ObservableCollection<Room> ReadAll()
        {
            return ref roomService.ReadAll();
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




    }
}