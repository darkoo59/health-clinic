using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Sims_Hospital_Zdravo.Repository
{
    public class RenovationRepository
    {
        private RenovationDataHandler _renovationDataHandler;
        private List<RenovationAppointment> _renovations;

        public RenovationRepository(RenovationDataHandler renovationDataHandler)
        {
            this._renovationDataHandler = renovationDataHandler;
            _renovations = new List<RenovationAppointment>();
            LoadDataFromFile();
        }

        public void Create(RenovationAppointment renovation)
        {
            _renovations.Add(renovation);
            LoadDataToFile();
        }

        public void Update(RenovationAppointment renovation)
        {
            foreach (RenovationAppointment reno in _renovations)
            {
                if (reno.Id == renovation.Id)
                {
                    reno.Room = renovation.Room;
                    reno.Type = renovation.Type;
                    reno.Time = renovation.Time;
                    reno.Description = renovation.Description;
                    return;
                }
            }
        }

        public void Delete(RenovationAppointment renovation)
        {
            _renovations.Remove(renovation);
            LoadDataToFile();
        }

        public List<RenovationAppointment> ReadAll()
        {
            return _renovations;
        }

        public List<RenovationAppointment> FindByType(RenovationType type)
        {
            List<RenovationAppointment> apps = new List<RenovationAppointment>();

            foreach (RenovationAppointment renovation in _renovations)
            {
                if (renovation.Type == type)
                {
                    apps.Add(renovation);
                }
            }

            return apps;
        }


        public RenovationAppointment FindById(int renovationId)
        {
            foreach (RenovationAppointment renovationAppointment in _renovations)
            {
                if (renovationAppointment.Id == renovationId)
                    return renovationAppointment;
            }

            return null;
        }

        public void DeleteById(int renovationId)
        {
            RenovationAppointment renovationAppointment = FindById(renovationId);
            Delete(renovationAppointment);
        }

        public List<TimeInterval> FindTakenIntervalsForRoom(int roomId)
        {
            List<TimeInterval> intervals = new List<TimeInterval>();
            foreach (RenovationAppointment renovationAppointment in _renovations)
            {
                if (renovationAppointment.Room.Id == roomId)
                {
                    intervals.Add(renovationAppointment.Time);
                }
            }

            return intervals;
        }


        private void LoadDataToFile()
        {
            _renovationDataHandler.Write(_renovations);
        }

        private void LoadDataFromFile()
        {
            _renovations = _renovationDataHandler.ReadAll();
        }
    }
}