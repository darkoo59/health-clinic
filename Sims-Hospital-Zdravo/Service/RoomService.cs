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
using Sims_Hospital_Zdravo.Utils;

namespace Service
{
    public class RoomService
    {
        private RoomValidator _validator;

        public RoomService(RoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
            _validator = new RoomValidator(this);
        }

        public void Create(Room room)
        {
            _validator.ValidateCreate(room);
            roomRepository.Create(room);
        }

        public ref ObservableCollection<Room> ReadAll()
        {
            return ref roomRepository.ReadAll();
        }

        public void Update(Room room)
        {
            _validator.ValidateUpdate(room);
            roomRepository.Update(room);
        }

        public void Delete(Room room)
        {
            _validator.ValidateDelete(room);
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
            _validator.ValidateDelete(id);
            roomRepository.DeleteById(id);
        }

        public RoomRepository roomRepository;
    }
}