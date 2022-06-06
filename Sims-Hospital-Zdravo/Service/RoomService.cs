/***********************************************************************
 * Module:  RoomRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RoomRepository
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Repository;
using Sims_Hospital_Zdravo.DTO;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils;
using Sims_Hospital_Zdravo.Utils.FilterPipelines;

namespace Service
{
    public class RoomService
    {
        private RoomValidator _validator;
        private IRoomRepository _roomRepository;


        public RoomService(IRoomRepository roomRepository)
        {
            this._roomRepository = roomRepository;
            _validator = new RoomValidator(this);
        }

        public void Create(Room room)
        {
            _validator.ValidateCreate(room);
            _roomRepository.Create(room);
        }

        public List<Room> ReadAll()
        {
            return _roomRepository.FindAll();
        }

        public void Update(Room room)
        {
            _validator.ValidateUpdate(room);
            _roomRepository.Update(room);
        }

        public void Delete(Room room)
        {
            _validator.ValidateDelete(room);
            _roomRepository.Delete(room);
        }

        public List<RoomEquipment> FilterRoomEquipment(Room room, RoomEquipmentFilterDTO roomEquipmentFilterDto)
        {
            IFilterPipeline<RoomEquipment> roomEquipmentFilterPipeline = RoomEquipmentFilterPipeline.CreateEquipmentFilterPipeline(roomEquipmentFilterDto);
            return roomEquipmentFilterPipeline.FilterAll(room.RoomEquipment);
        }

        public Room FindById(int id)
        {
            return _roomRepository.FindById(id);
        }

        public Room FindByType(RoomType type)
        {
            return _roomRepository.FindByType(type);
        }

        public void DeleteById(int id)
        {
            _validator.ValidateDelete(id);
            _roomRepository.DeleteById(id);
        }

        public Room FindByRoomNumber(string roomNumber)
        {
            return _roomRepository.FindByRoomNumber(roomNumber);
        }

        public int GenerateId()
        {
            List<Room> appointments = _roomRepository.FindAll();
            List<int> ids = new List<int>(appointments.Select(x => x.Id));

            int id = 0;

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }
    }
}