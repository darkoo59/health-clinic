/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using DataHandler;
using Model;
using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;


namespace Repository
{
    public class RoomRepository : IUpdateFilesObserver, IRoomRepository
    {
        private RoomDataHandler _roomDataHandler;
        private List<Room> _rooms;

        public RoomRepository()
        {
            _rooms = new List<Room>();
            _roomDataHandler = new RoomDataHandler();
        }

        public List<Room> FindAll()
        {
            return _roomDataHandler.ReadAll();
        }

        public void Create(Room room)
        {
            LoadDataFromFiles();
            _rooms.Add(room);
            room.AddObserver(this);
            LoadDataToFile();
        }

        public void Update(Room room)
        {
            LoadDataFromFiles();
            foreach (var rm in _rooms.Where(rm => room.Id == rm.Id))
            {
                rm.Update(room);
                LoadDataToFile();
                return;
            }
        }

        public void Delete(Room room)
        {
            LoadDataFromFiles();
            _rooms.Remove(room);
            LoadDataToFile();
        }

        public Room FindById(int id)
        {
            LoadDataFromFiles();
            return _rooms.FirstOrDefault(room => room.Id == id);
        }

        public void DeleteById(int id)
        {
            Room room = FindById(id);
            if (room != null) _rooms.Remove(room);
            LoadDataToFile();
        }

        public Room FindByType(RoomType type)
        {
            LoadDataFromFiles();
            return _rooms.FirstOrDefault(room => room.Type == type);
        }

        public Room FindByRoomNumber(string roomNumber)
        {
            LoadDataFromFiles();
            return _rooms.FirstOrDefault(room => room.RoomNumber.Equals(roomNumber));
        }

        public List<RoomEquipment> FindAllEquipment()
        {
            Room room = FindByType(RoomType.WAREHOUSE);
            return room.FindEquipmentByType(EquipmentType.Consumable);
        }

        public void RemoveMultiple(List<Room> rooms)
        {
            LoadDataFromFiles();
            foreach (Room room in rooms)
            {
                DeleteById(room.Id);
            }

            LoadDataToFile();
        }

        private void LoadDataFromFiles()
        {
            _rooms = _roomDataHandler.ReadAll();
            foreach (Room room in _rooms)
            {
                room.AddObserver(this);
            }
        }

        private void LoadDataToFile()
        {
            _roomDataHandler.Write(_rooms);
        }

        public void NotifyUpdated()
        {
            LoadDataToFile();
        }
    }
}