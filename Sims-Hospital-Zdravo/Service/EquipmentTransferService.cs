using Model;
using Repository;
using System.Collections.ObjectModel;
using Utils;
using System.Linq;
using System;
using System.Collections.Generic;
using Sims_Hospital_Zdravo.Interfaces;

namespace Service
{
    public class EquipmentTransferService
    {
        private IRelocationRepository _relocationAppointmentRepository;
        private IRoomRepository _roomRepository;
        private TimeSchedulerService _timeSchedulerService;
        private EquipmentTransferValidator _validator;

        public EquipmentTransferService(TimeSchedulerService timeSchedulerService)
        {
            _roomRepository = new RoomRepository();
            _relocationAppointmentRepository = new RelocationAppointmentRepository();
            _timeSchedulerService = timeSchedulerService;
            _validator = new EquipmentTransferValidator(timeSchedulerService);
        }

        public void FinishRelocationAppointment(int appointmentId)
        {
            RelocationAppointment appointment = _relocationAppointmentRepository.FindById(appointmentId);
            Room toRoom = appointment.ToRoom;
            RoomEquipment re = appointment.RoomEquipment;
            toRoom.AddEquipment(re);
            _roomRepository.Update(toRoom);
            _relocationAppointmentRepository.DeleteById(appointmentId);
        }

        public void MakeRelocationAppointment(RelocationAppointment relocationAppointment)
        {
            _validator.ValidateTransferFromRoom(relocationAppointment);
            MakeAppointmentFromRoom(relocationAppointment);
        }

        private void MakeAppointmentFromRoom(RelocationAppointment relocationAppointment)
        {
            Room fromRoom = _roomRepository.FindById(relocationAppointment.FromRoom.Id);
            fromRoom.RemoveEquipment(relocationAppointment.RoomEquipment);
            _roomRepository.Update(fromRoom);
            _relocationAppointmentRepository.Create(relocationAppointment);
        }

        public List<TimeInterval> FindReservedTimeForRooms(Room fromRoom, Room toRoom)
        {
            return _timeSchedulerService.FindReservedTimeForRooms(fromRoom, toRoom);
        }

        public List<RelocationAppointment> FindAll()
        {
            return _relocationAppointmentRepository.FindAll();
        }


        public int GenerateId()
        {
            List<RelocationAppointment> appointments = _relocationAppointmentRepository.FindAll();
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