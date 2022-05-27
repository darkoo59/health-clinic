using System;
using System.Collections.Generic;
using Controller;
using Model;
using Repository;
using Service;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Utils
{
    public class RenovationValidator
    {
        private RenovationRepository renovationRepository;
        private RoomRepository roomRepository;
        private TimeSchedulerService _timeSchedulerService;

        public RenovationValidator(RoomRepository roomRepository, RenovationRepository renovationRepository,
            TimeSchedulerService timeSchedulerService)
        {
            this.renovationRepository = renovationRepository;
            this.roomRepository = roomRepository;
            this._timeSchedulerService = timeSchedulerService;
        }

        private void ValidateRoomTaken(Room room, TimeInterval ti)
        {
            if (!_timeSchedulerService.IsRoomFreeInDateInterval(room.Id, ti))
            {
                throw new Exception("Room taken in given interval");
            }
        }

        private void ValidateRoomExists(Room room)
        {
            if (room == null) throw new Exception("Room not selected!");
            foreach (Room rm in roomRepository.ReadAll())
            {
                if (rm.Id == room.Id)
                {
                    return;
                }
            }

            throw new Exception("Room with given Id doesn't exist!");
        }

        private void ValidateDateCorrect(TimeInterval ti)
        {
            if (ti.Start > ti.End)
            {
                throw new Exception("End date should be later than start date!");
            }
        }

        private void ValidateQuadrature(Room room, List<Room> rooms)
        {
            Console.WriteLine(CalculateQuadratureForRooms(rooms) + " " + room.Quadrature);
            if (room.Quadrature != CalculateQuadratureForRooms(rooms))
                throw new Exception("Quadrature of new rooms should be exactly as real room!");
        }

        private void ValidateFloors(Room room, List<Room> rooms)
        {
            foreach (Room rm in rooms)
            {
                if (rm.Floor != room.Floor)
                    throw new Exception("Floor should be same for all rooms!");
            }
        }

        private void ValidateRoomNumbers(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                RoomNumberExists(room);
            }
        }

        private void RoomNumberExists(Room room)
        {
            if (roomRepository.FindByRoomNumber(room.RoomNumber) != null)
            {
                throw new Exception("Room number " + room.RoomNumber + " already exists!");
            }
        }

        private int CalculateQuadratureForRooms(List<Room> rooms)
        {
            int quadrature = 0;
            foreach (Room room in rooms)
            {
                if (room == null)
                {
                    Console.WriteLine("Soba je null");
                    continue;
                }

                quadrature += room.Quadrature;
            }

            return quadrature;
        }

        private void ValidateRoomsTaken(List<Room> rooms, TimeInterval ti)
        {
            foreach (Room room in rooms)
            {
                ValidateRoomTaken(room, ti);
            }
        }

        private void ValidateSplitAdvancedRenovation(Room room, List<Room> rooms, TimeInterval ti)
        {
            ValidateQuadrature(room, rooms);
            ValidateFloors(room, rooms);
            ValidateRoomTaken(room, ti);
        }

        private void ValidateJoinAdvancedRenovation(Room room, List<Room> rooms, TimeInterval ti)
        {
            // ValidateQuadrature(room, rooms);
            ValidateFloors(room, rooms);
            ValidateRoomsTaken(rooms, ti);
        }

        public void ValidateRenovation(Room room, TimeInterval ti)
        {
            ValidateRoomExists(room);
            ValidateRoomTaken(room, ti);
            ValidateDateCorrect(ti);
        }

        public void ValidateAdvancedRenovation(AdvancedRenovationAppointment advancedRenovationAppointment)
        {
            if (advancedRenovationAppointment.RoomRenovationType == RoomRenovationType.JOIN)
            {
                ValidateJoinAdvancedRenovation(advancedRenovationAppointment.Room, advancedRenovationAppointment.Rooms, advancedRenovationAppointment.Time);
            }
            else
            {
                ValidateSplitAdvancedRenovation(advancedRenovationAppointment.Room, advancedRenovationAppointment.Rooms, advancedRenovationAppointment.Time);
            }
        }
    }
}