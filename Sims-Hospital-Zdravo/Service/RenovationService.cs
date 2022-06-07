using Service;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo.Model
{
    public class RenovationService
    {
        private IRenovationRepository _renovationRepository;
        private TimeSchedulerService _timeSchedulerService;
        private RenovationValidator _renovationValidator;
        private IRoomRepository _roomRepository;

        public RenovationService(TimeSchedulerService timeSchedulerService)
        {
            _renovationRepository = new RenovationRepository();
            _roomRepository = new RoomRepository();
            _timeSchedulerService = timeSchedulerService;
            _renovationValidator = new RenovationValidator(timeSchedulerService);
        }

        public void MakeRenovationAppointment(RenovationAppointment renovationAppointment)
        {
            _renovationValidator.ValidateRenovation(renovationAppointment.Room, renovationAppointment.Time);
            Create(renovationAppointment);
        }

        public void MakeAdvancedRenovationAppointment(AdvancedRenovationAppointment renovationAppointment)
        {
            _renovationValidator.ValidateAdvancedRenovation(renovationAppointment);
            Create(renovationAppointment);
        }


        public void FinishRenovationAppointment(int renovationId)
        {
            RenovationAppointment renovationAppointment = FindById(renovationId);
            if (renovationAppointment.IsAdvancedRenovation())
            {
                FinishAdvancedRenovationAppointment((AdvancedRenovationAppointment)renovationAppointment);
            }

            _renovationRepository.DeleteById(renovationId);
        }

        private void FinishAdvancedRenovationAppointment(AdvancedRenovationAppointment advancedRenovationAppointment)
        {
            if (advancedRenovationAppointment.RoomRenovationType == RoomRenovationType.JOIN)
            {
                JoinRoomsAfterRenovation(advancedRenovationAppointment);
                return;
            }

            SplitRoomsAfterRenovation(advancedRenovationAppointment);
        }

        private void JoinRoomsAfterRenovation(AdvancedRenovationAppointment advancedRenovationAppointment)
        {
            Room resultRoom = advancedRenovationAppointment.Room;
            List<Room> roomsForJoining = advancedRenovationAppointment.Rooms;
            resultRoom.Id = GenerateId();
            resultRoom.Quadrature = CalculateQuadratureForRooms(roomsForJoining);
            _roomRepository.RemoveMultiple(roomsForJoining);
            _roomRepository.Create(resultRoom);
        }

        private void SplitRoomsAfterRenovation(AdvancedRenovationAppointment advancedRenovationAppointment)
        {
            Room forSpliting = advancedRenovationAppointment.Room;
            List<Room> resultRooms = advancedRenovationAppointment.Rooms;
            foreach (Room room in resultRooms)
            {
                room.Id = GenerateId();
                Console.WriteLine("Generating id for room " + room.RoomNumber);
                _roomRepository.Create(room);
            }

            _roomRepository.DeleteById(forSpliting.Id);
        }


        private int CalculateQuadratureForRooms(List<Room> rooms)
        {
            return rooms.Sum(room => room.Quadrature);
        }

        public List<TimeInterval> GetTakenDateIntervals(List<Room> rooms)
        {
            return _timeSchedulerService.FindReservedDatesForRooms(rooms);
        }

        public List<TimeInterval> GetTakenDateIntervals(Room room)
        {
            return _timeSchedulerService.FindReservedDatesForRoom(room);
        }

        public void Create(RenovationAppointment renovation)
        {
            _renovationRepository.Create(renovation);
        }

        public void Update(RenovationAppointment renovation)
        {
            _renovationRepository.Update(renovation);
        }

        public void Delete(RenovationAppointment renovation)
        {
            _renovationRepository.Delete(renovation);
        }

        public List<RenovationAppointment> FindAll()
        {
            return _renovationRepository.FindAll();
        }

        public List<RenovationAppointment> FindByType(RenovationType type)
        {
            return _renovationRepository.FindByType(type);
        }

        public RenovationAppointment FindById(int renovationId)
        {
            return _renovationRepository.FindById(renovationId);
        }

        public int GenerateId()
        {
            List<RenovationAppointment> appointments = _renovationRepository.FindAll();
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