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

namespace Repository
{
    public class RoomRepository : IUpdateFilesObserver
    {
        private RoomDataHandler _roomDataHandler;
        private ObservableCollection<Room> _rooms;

        public RoomRepository(RoomDataHandler rmDataHandler)
        {
            _rooms = new ObservableCollection<Room>();
            _roomDataHandler = rmDataHandler;
            LoadDataFromFiles();
        }

        public void Create(Room room)
        {
            _rooms.Add(room);
            room.AddObserver(this);
            LoadDataToFile();
        }

        public ref ObservableCollection<Room> ReadAll()
        {
            return ref _rooms;
        }

        public void Update(Room room)
        {
            int id = room.Id;
            foreach (Room rm in _rooms)
            {
                if (id == rm.Id)
                {
                    rm.Id = room.Id;
                    rm.Floor = room.Floor;
                    rm.Type = room.Type;
                    LoadDataToFile();
                    return;
                }
            }
        }

        public void Delete(Room room)
        {
            _rooms.Remove(room);
            LoadDataToFile();
        }

        public Room FindById(int id)
        {
            foreach (Room room in _rooms)
            {
                if (id == room.Id)
                {
                    return room;
                }
            }

            return null;
        }

        public void DeleteById(int id)
        {
            Room room = FindById(id);
            if (room != null) _rooms.Remove(room);
        }

        public Room FindByType(RoomType type)
        {
            foreach (Room room in _rooms)
            {
                if (room.Type == type)
                {
                    return room;
                }
            }

            return null;
        }

        public void LoadDataFromFiles()
        {
            _rooms = _roomDataHandler.ReadAll();
            foreach (Room room in _rooms)
            {
                room.AddObserver(this);
            }
        }

        public void LoadDataToFile()
        {
            _roomDataHandler.Write(_rooms);
        }

        public void NotifyUpdated()
        {
            LoadDataToFile();
        }
    }
}