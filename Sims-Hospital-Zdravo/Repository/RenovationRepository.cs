using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Repository
{
    public class RenovationRepository : IRenovationRepository
    {
        private RenovationDataHandler _renovationDataHandler;
        private List<RenovationAppointment> _renovations;

        public RenovationRepository()
        {
            _renovationDataHandler = new RenovationDataHandler();
            _renovations = new List<RenovationAppointment>();
        }

        public void Create(RenovationAppointment renovation)
        {
            LoadDataFromFiles();
            _renovations.Add(renovation);
            LoadDataToFile();
        }

        public void Update(RenovationAppointment renovation)
        {
            LoadDataFromFiles();
            foreach (var reno in _renovations.Where(reno => reno.Id == renovation.Id))
            {
                reno.Update(renovation);
                LoadDataToFile();
                return;
            }
        }

        public void Delete(RenovationAppointment renovation)
        {
            LoadDataFromFiles();
            _renovations.Remove(renovation);
            LoadDataToFile();
        }

        public List<RenovationAppointment> FindAll()
        {
            return _renovationDataHandler.ReadAll();
        }

        public List<RenovationAppointment> FindByType(RenovationType type)
        {
            LoadDataFromFiles();
            return _renovations.Where(renovation => renovation.Type == type).ToList();
        }

        public RenovationAppointment FindById(int renovationId)
        {
            LoadDataFromFiles();
            return _renovations.FirstOrDefault(renovationAppointment => renovationAppointment.Id == renovationId);
        }

        public void DeleteById(int renovationId)
        {
            RenovationAppointment renovationAppointment = FindById(renovationId);
            _renovations.Remove(renovationAppointment);
            LoadDataToFile();
        }

        public List<TimeInterval> FindTakenIntervalsForRoom(int roomId)
        {
            LoadDataFromFiles();
            return (from renovationAppointment in _renovations where renovationAppointment.Room.Id == roomId select renovationAppointment.Time).ToList();
        }

        private void LoadDataToFile()
        {
            _renovationDataHandler.Write(_renovations.ToList());
        }

        private void LoadDataFromFiles()
        {
            _renovations = _renovationDataHandler.ReadAll();
        }
    }
}