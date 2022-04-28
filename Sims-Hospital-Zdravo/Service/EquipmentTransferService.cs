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
        private RelocationAppointmentRepository relocationAppointmentRepository;
        private RoomRepository roomRepository;
        private TimeSchedulerService timeSchedulerService;
        private EquipmentTransferValidator validator;

        public EquipmentTransferService(RoomRepository roomRepository,
            RelocationAppointmentRepository relocationAppointmentRepository, TimeSchedulerService timeSchedulerService)
        {
            this.roomRepository = roomRepository;
            this.relocationAppointmentRepository = relocationAppointmentRepository;
            this.timeSchedulerService = timeSchedulerService;
            validator = new EquipmentTransferValidator(roomRepository, timeSchedulerService);
        }

        public void FinishRelocationAppointment(int appointmentId)
        {
            RelocationAppointment appointment = relocationAppointmentRepository.FindById(appointmentId);
            Console.WriteLine("Transfering " + appointment._RoomEquipment._Quantity + " Equipment");
            Room toRoom = appointment._ToRoom;
            Room originalRoom = roomRepository.FindById(toRoom._Id);

            RoomEquipment re = appointment._RoomEquipment;

            relocationAppointmentRepository.Delete(appointment);
            originalRoom.AddEquipment(re);
        }

        public void MakeRelocationAppointment(int fromRoomId, int toRoomId, Equipment eq, int quantity, TimeInterval ti)
        {
            validator.ValidateTransferFromRoom(fromRoomId, toRoomId, eq._Id, quantity, ti);
            MakeAppointmentFromRoom(fromRoomId, toRoomId, eq, quantity, ti);
        }


        private void MakeAppointmentFromRoom(int fromRoomId, int toRoomId, Equipment eq, int quantity, TimeInterval ti)
        {
            Room fromRoom = roomRepository.FindById(fromRoomId);
            Room toRoom = roomRepository.FindById(toRoomId);
            RoomEquipment eqForTransfer = new RoomEquipment(eq, quantity);
            RelocationAppointment relocationApp =
                new RelocationAppointment(fromRoom, toRoom, ti, eqForTransfer, GenerateId());

            fromRoom.RemoveEquipment(eqForTransfer);
            relocationAppointmentRepository.Create(relocationApp);
        }

        public List<TimeInterval> FindAvailableTimeForInterval(int minutes, Room fromRoom, Room toRoom)
        {
            return timeSchedulerService.FindAvailableTimeForInterval(minutes, fromRoom, toRoom);
        }

        public List<RelocationAppointment> ReadAll()
        {
            return relocationAppointmentRepository.ReadAll();
        }


        private int GenerateId()
        {
            List<RelocationAppointment> appointments = relocationAppointmentRepository.ReadAll();
            List<int> ids = (List<int>)appointments.Select(x => x._Id);

            int id = 0;

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }
    }
}