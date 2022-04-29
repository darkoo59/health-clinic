using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Sims_Hospital_Zdravo.Controller
{
    class RenovationController
    {
        private RenovationService renovationService;

        public RenovationController(RenovationService renovationService)
        {
            this.renovationService = renovationService;
        }

        public void MakeRenovationAppointment(TimeInterval time, Room room, RenovationType type, string description)
        {
            renovationService.MakeRenovationAppointment(time, room, type, description);
        }

        public void FinishRenovationAppointment(int renovationId)
        {
            renovationService.FinishRenovationAppointment(renovationId);
        }

        public void Create(RenovationAppointment renovation)
        {
            renovationService.Create(renovation);
        }

        public void Update(RenovationAppointment renovation)
        {
            renovationService.Update(renovation);
        }

        public void Delete(RenovationAppointment renovation)
        {
            renovationService.Delete(renovation);
        }

        public List<RenovationAppointment> ReadAll()
        {
            return renovationService.ReadAll();
        }

        public List<RenovationAppointment> FindByType(RenovationType type)
        {
            return renovationService.FindByType(type);
        }
    }
}