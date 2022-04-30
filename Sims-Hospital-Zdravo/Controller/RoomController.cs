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
        public RoomService _roomService;


        public RoomController(RoomService roomService)
        {
            this._roomService = roomService;
        }

        public void Create(Room room)
        {
            _roomService.Create(room);
        }

        public ref ObservableCollection<Room> ReadAll()
        {
            return ref _roomService.ReadAll();
        }

        public void Update(Room room)
        {
            _roomService.Update(room);
        }

        public void Delete(Room room)
        {
            _roomService.Delete(room);
        }

        public Room FindById(int id)
        {
            return _roomService.FindById(id);
        }

        public void DeleteById(int id)
        {
            _roomService.DeleteById(id);
        }
    }
}