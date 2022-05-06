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

        public RenovationService(RenovationRepository renovationRepository, TimeSchedulerService timeSchedulerService,
            RoomRepository roomRepository)
        {
            this._renovationRepository = renovationRepository;
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
            // _renovationValidator.ValidateRenovation(room, time);
            AdvancedRenovationAppointment renovationAppointment = new AdvancedRenovationAppointment(time, room, description, rooms, roomRenovationType, GenerateId());
            Create(renovationAppointment);
        }

        public List<TimeInterval> GetTakenDateIntervals(Room room)
        {
            return timeSchedulerService.FindReservedDatesForRoom(room);
        }


        public void FinishRenovationAppointment(int renovationId)
        {
            _renovationRepository.DeleteById(renovationId);
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