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
        private RenovationRepository renovationRepository;
        private RoomRepository roomRepository;
        private TimeSchedulerService timeSchedulerService;
        private RenovationValidator renovationValidator;

        public RenovationService(RenovationRepository renovationRepository, TimeSchedulerService timeSchedulerService,
            RoomRepository roomRepository)
        {
            this.renovationRepository = renovationRepository;
            this.timeSchedulerService = timeSchedulerService;
            this.roomRepository = roomRepository;
            renovationValidator = new RenovationValidator(roomRepository, renovationRepository, timeSchedulerService);
        }

        public void MakeRenovationAppointment(TimeInterval time, Room room, RenovationType type,
            string description)
        {
            renovationValidator.ValidateRenovation(room, time);
            RenovationAppointment renovationAppointment =
                new RenovationAppointment(time, room, description, type, GenerateId());
            renovationRepository.Create(renovationAppointment);
        }

        public void FinishRenovationAppointment(int renovationId)
        {
            renovationRepository.DeleteById(renovationId);
        }

        public void Create(RenovationAppointment renovation)
        {
            renovationRepository.Create(renovation);
        }

        public void Update(RenovationAppointment renovation)
        {
            renovationRepository.Update(renovation);
        }

        public void Delete(RenovationAppointment renovation)
        {
            renovationRepository.Delete(renovation);
        }

        public List<RenovationAppointment> ReadAll()
        {
            return renovationRepository.ReadAll();
        }

        public List<RenovationAppointment> FindByType(RenovationType type)
        {
            return renovationRepository.FindByType(type);
        }

        private int GenerateId()
        {
            List<RenovationAppointment> appointments = renovationRepository.ReadAll();
            List<int> ids = new List<int>(appointments.Select(x => x._Id));

            int id = 0;

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }
    }
}