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
        private RenovationDataHandler renovationDataHandler;
        private List<RenovationAppointment> renovations;

        public RenovationRepository(RenovationDataHandler renovationDataHandler)
        {
            this.renovationDataHandler = renovationDataHandler;
            renovations = new List<RenovationAppointment>();
            LoadDataFromFile();
        }

        public void Create(RenovationAppointment renovation)
        {
            renovations.Add(renovation);
            LoadDataToFile();
        }

        public void Update(RenovationAppointment renovation)
        {
            foreach (RenovationAppointment reno in renovations)
            {
                if (reno._Id == renovation._Id)
                {
                    reno._Room = renovation._Room;
                    reno._Type = renovation._Type;
                    reno._Time = renovation._Time;
                    reno._Description = renovation._Description;
                    return;
                }
            }
        }

        public void Delete(RenovationAppointment renovation)
        {
            renovations.Remove(renovation);
            LoadDataToFile();
        }

        public List<RenovationAppointment> ReadAll()
        {
            return renovations;
        }

        public List<RenovationAppointment> FindByType(RenovationType type)
        {
            List<RenovationAppointment> apps = new List<RenovationAppointment>();

            foreach (RenovationAppointment renovation in renovations)
            {
                if (renovation._Type == type)
                {
                    apps.Add(renovation);
                }
            }

            return apps;
        }


        public RenovationAppointment FindById(int renovationId)
        {
            foreach (RenovationAppointment renovationAppointment in renovations)
            {
                if (renovationAppointment._Id == renovationId)
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
            foreach (RenovationAppointment renovationAppointment in renovations)
            {
                if (renovationAppointment._Room._Id == roomId)
                {
                    intervals.Add(renovationAppointment._Time);
                }
            }

            return intervals;
        }


        private void LoadDataToFile()
        {
            renovationDataHandler.Write(renovations);
        }

        private void LoadDataFromFile()
        {
            renovations = renovationDataHandler.ReadAll();
        }
    }
}