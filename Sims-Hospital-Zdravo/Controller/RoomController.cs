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
            try
            {
                RoomValidator validator = new RoomValidator(roomService, room);
                validator.ValidateCreate();
                roomService.Create(room);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ref ObservableCollection<Room> ReadAll()
        {
            return ref roomService.ReadAll();
        }

        public void Update(Room room)
        {
            try
            {
                RoomValidator validator = new RoomValidator(roomService, room);
                validator.ValidateUpdate();
                roomService.Update(room);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Delete(Room room)
        {
            try
            {
                RoomValidator validator = new RoomValidator(roomService, room);
                validator.ValidateDelete();
                roomService.Delete(room);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Room FindById(int id)
        {
            return roomService.FindById(id);
        }

        public void DeleteById(int id)
        {
            try
            {
                RoomValidator validator = new RoomValidator(roomService, null);
                validator.ValidateDelete(id);
                roomService.DeleteById(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}