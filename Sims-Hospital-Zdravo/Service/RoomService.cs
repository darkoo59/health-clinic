/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Repository;

namespace Service
{
    public class RoomService
    {
        public RoomService(RoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }
        public void Create(Room room)
        {
            roomRepository.Create(room);
        }
        public ref ObservableCollection<Room> ReadAll()
        {
            return ref roomRepository.ReadAll();
        }

        public void Update(Room room)
        {
            roomRepository.Update(room);
        }

        public void Delete(Room room)
        {
            roomRepository.Delete(room);
        }

        public Room FindById(int id)
        {
            return roomRepository.FindById(id);
        }

        public Room FindByType(RoomType type)
        {
            return roomRepository.FindByType(type);
        }

        public void DeleteById(int id)
        {
            roomRepository.DeleteById(id);
        }

        public RoomRepository roomRepository;

    }
}