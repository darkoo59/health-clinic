using Service;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo.Service
{
    public class RenovationService
    {
        private RenovationRepository _renovationRepository;
        private TimeSchedulerService timeSchedulerService;
        private RenovationValidator _renovationValidator;
        private RoomRepository _roomRepository;

        public RenovationService(RenovationRepository renovationRepository, TimeSchedulerService timeSchedulerService,
            RoomRepository roomRepository)
        {
            this._renovationRepository = renovationRepository;
            this._roomRepository = roomRepository;
            this.timeSchedulerService = timeSchedulerService;
            _renovationValidator = new RenovationValidator(roomRepository, renovationRepository, timeSchedulerService);
        }

        public void MakeRenovationAppointment(TimeInterval time, Room room, RenovationType type,
            string description)
        {
            _renovationValidator.ValidateRenovation(room, time);
            RenovationAppointment renovationAppointment = new RenovationAppointment(time, room, description, type, GenerateId());
            Create(renovationAppointment);
        }

        public void MakeAdvancedRenovationAppointment(TimeInterval time, Room room, string description, List<Room> rooms, RoomRenovationType roomRenovationType)
        {
            _renovationValidator.ValidateAdvancedRenovation(room, rooms, time, roomRenovationType);
            AdvancedRenovationAppointment renovationAppointment = new AdvancedRenovationAppointment(time, room, description, rooms, roomRenovationType, GenerateId());
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
            resultRoom.Quadrature = CalculateQuadratureForRooms(roomsForJoining);
        }

        private void SplitRoomsAfterRenovation(AdvancedRenovationAppointment advancedRenovationAppointment)
        {
            Room forSpliting = advancedRenovationAppointment.Room;
            List<Room> resultRooms = advancedRenovationAppointment.Rooms;
            foreach (Room room in resultRooms)
            {
                room.Id = GenerateId();
                _roomRepository.Create(room);
            }

            _roomRepository.DeleteById(forSpliting.Id);
        }


        private int CalculateQuadratureForRooms(List<Room> rooms)
        {
            int quadrature = 0;
            foreach (Room room in rooms)
            {
                quadrature += room.Quadrature;
            }

            return quadrature;
        }

        public List<TimeInterval> GetTakenDateIntervals(Room room)
        {
            return timeSchedulerService.FindReservedDatesForRoom(room);
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

        public List<RenovationAppointment> ReadAll()
        {
            return _renovationRepository.ReadAll();
        }

        public List<RenovationAppointment> FindByType(RenovationType type)
        {
            return _renovationRepository.FindByType(type);
        }

        public RenovationAppointment FindById(int renovationId)
        {
            return _renovationRepository.FindById(renovationId);
        }

        private int GenerateId()
        {
            List<RenovationAppointment> appointments = _renovationRepository.ReadAll();
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