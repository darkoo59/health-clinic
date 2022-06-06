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
using Sims_Hospital_Zdravo.DTO;

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

        public List<Room> ReadAll()
        {
            return _roomService.ReadAll();
        }

        public void Update(Room room)
        {
            _roomService.Update(room);
        }

        public void Delete(Room room)
        {
            _roomService.Delete(room);
        }

        public List<RoomEquipment> FilterRoomEquipment(Room room, RoomEquipmentFilterDTO roomEquipmentFilterDto)
        {
            return _roomService.FilterRoomEquipment(room, roomEquipmentFilterDto);
        }

        public Room FindById(int id)
        {
            return _roomService.FindById(id);
        }

        public void DeleteById(int id)
        {
            _roomService.DeleteById(id);
        }

        public int GenerateId()
        {
            return _roomService.GenerateId();
        }
    }
}