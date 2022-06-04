using Model;
using Repository;
using System.Collections.ObjectModel;
using Utils;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Service
{
    public class EquipmentTransferService
    {
        private RelocationAppointmentRepository _relocationAppointmentRepository;
        private RoomRepository _roomRepository;
        private TimeSchedulerService _timeSchedulerService;
        private EquipmentTransferValidator _validator;

        public EquipmentTransferService(RoomRepository roomRepository,
            RelocationAppointmentRepository relocationAppointmentRepository, TimeSchedulerService timeSchedulerService)
        {
            _roomRepository = roomRepository;
            _relocationAppointmentRepository = relocationAppointmentRepository;
            _timeSchedulerService = timeSchedulerService;
            _validator = new EquipmentTransferValidator(roomRepository, timeSchedulerService);
        }

        public void FinishRelocationAppointment(int appointmentId)
        {
            RelocationAppointment appointment = _relocationAppointmentRepository.FindById(appointmentId);
            Room toRoom = appointment.ToRoom;
            Room originalRoom = _roomRepository.FindById(toRoom.Id);
            RoomEquipment re = appointment.RoomEquipment;

            _relocationAppointmentRepository.Delete(appointment);
            originalRoom.AddEquipment(re);
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
            _relocationAppointmentRepository.Create(relocationAppointment);
        }

        public List<TimeInterval> FindReservedTimeForRooms(Room fromRoom, Room toRoom)
        {
            return _timeSchedulerService.FindReservedTimeForRooms(fromRoom, toRoom);
        }

        public List<RelocationAppointment> ReadAll()
        {
            return _relocationAppointmentRepository.ReadAll();
        }


        public int GenerateId()
        {
            List<RelocationAppointment> appointments = _relocationAppointmentRepository.ReadAll();
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