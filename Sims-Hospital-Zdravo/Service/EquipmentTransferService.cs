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
            this._roomRepository = roomRepository;
            this._relocationAppointmentRepository = relocationAppointmentRepository;
            this._timeSchedulerService = timeSchedulerService;
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

        public void MakeRelocationAppointment(int fromRoomId, int toRoomId, Equipment eq, int quantity, TimeInterval ti)
        {
            _validator.ValidateTransferFromRoom(fromRoomId, toRoomId, eq.Id, quantity, ti);
            MakeAppointmentFromRoom(fromRoomId, toRoomId, eq, quantity, ti);
        }


        private void MakeAppointmentFromRoom(int fromRoomId, int toRoomId, Equipment eq, int quantity, TimeInterval ti)
        {
            Room fromRoom = _roomRepository.FindById(fromRoomId);
            Room toRoom = _roomRepository.FindById(toRoomId);
            RoomEquipment eqForTransfer = new RoomEquipment(eq, quantity);
            RelocationAppointment relocationApp = new RelocationAppointment(fromRoom, toRoom, ti, eqForTransfer, GenerateId());

            fromRoom.RemoveEquipment(eqForTransfer);
            _relocationAppointmentRepository.Create(relocationApp);
        }

        public List<TimeInterval> FindReservedTimeForRooms(Room fromRoom, Room toRoom)
        {
            return _timeSchedulerService.FindReservedTimeForRooms(fromRoom, toRoom);
        }

        public List<RelocationAppointment> ReadAll()
        {
            return _relocationAppointmentRepository.ReadAll();
        }


        private int GenerateId()
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