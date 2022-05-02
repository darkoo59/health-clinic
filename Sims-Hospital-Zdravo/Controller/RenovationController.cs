using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace Sims_Hospital_Zdravo.Controller
{
    public class RenovationController
    {
        private RenovationService _renovationService;

        public RenovationController(RenovationService renovationService)
        {
            this._renovationService = renovationService;
        }

        public void MakeRenovationAppointment(TimeInterval time, Room room, RenovationType type, string description)
        {
            _renovationService.MakeRenovationAppointment(time, room, type, description);
        }

        public List<TimeInterval> GetTakenDateIntervals(Room room)
        {
            return _renovationService.GetTakenDateIntervals(room);
        }

        public void FinishRenovationAppointment(int renovationId)
        {
            _renovationService.FinishRenovationAppointment(renovationId);
        }

        public void Create(RenovationAppointment renovation)
        {
            _renovationService.Create(renovation);
        }

        public void Update(RenovationAppointment renovation)
        {
            _renovationService.Update(renovation);
        }

        public void Delete(RenovationAppointment renovation)
        {
            _renovationService.Delete(renovation);
        }

        public List<RenovationAppointment> ReadAll()
        {
            return _renovationService.ReadAll();
        }

        public List<RenovationAppointment> FindByType(RenovationType type)
        {
            return _renovationService.FindByType(type);
        }
    }
}