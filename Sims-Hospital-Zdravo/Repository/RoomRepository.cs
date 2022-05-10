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
            StackTrace stackTrace = new StackTrace();
            Console.WriteLine(stackTrace.GetFrame(1).GetMethod().Name);
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
            LoadDataToFile();
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

        public Room FindByRoomNumber(string roomNumber)
        {
            foreach (Room room in _rooms)
            {
                if (room.RoomNumber.Equals(roomNumber))
                {
                    return room;
                }
            }

            return null;
        }

        public void RemoveMultiple(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                DeleteById(room.Id);
            }
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