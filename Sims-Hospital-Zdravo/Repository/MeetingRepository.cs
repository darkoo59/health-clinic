using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Repository
{
    public class MeetingRepository:IMeetingRepository
    {
        private MeetingDataHandler _meetingDataHandler;
        private List<Meeting> _meetings;

        public MeetingRepository(MeetingDataHandler meetingDataHandler)
        {
            _meetings = new List<Meeting>();
            _meetingDataHandler = meetingDataHandler;
            LoadDataFromFiles();
        }

        public void Create(Meeting meeting)
        {
            LoadDataFromFiles();
            meeting.Id = GenerateId();
            _meetings.Add(meeting);
            LoadDataToFile();
        }

        public List<Meeting> FindAll()
        {
            LoadDataFromFiles();
            return _meetings;
        }

        public void Update(Meeting meeting)
        {
            LoadDataFromFiles();
            int id = meeting.Id;
            foreach (var meet in _meetings.Where(meet => id == meet.Id))
            {
                meet.Id = meeting.Id;
                meet.Start = meeting.Start;
                meet.Room = meeting.Room;
                meet.OptionalAttendees = meeting.OptionalAttendees;
                meet.RequiredAttendees = meeting.RequiredAttendees;
                LoadDataToFile();
                return;
            }
        }

        public void Delete(Meeting meeting)
        {
            LoadDataFromFiles();
            _meetings.Remove(meeting);
            LoadDataToFile();
        }

        public Meeting FindById(int id)
        {
            LoadDataFromFiles();
            return _meetings.FirstOrDefault(meeting => id == meeting.Id);
        }

        public void DeleteById(int id)
        {
            LoadDataFromFiles();
            Meeting meeting = FindById(id);
            if (meeting != null) _meetings.Remove(meeting);
            LoadDataToFile();
        }

        private void LoadDataFromFiles()
        {
            _meetings = _meetingDataHandler.ReadAll();
        }

        private void LoadDataToFile()
        {
            _meetingDataHandler.Write(_meetings);
        }

        public int GenerateId()
        {
            LoadDataFromFiles();
            int id = 0;
            List<int> ids = _meetings.Select(meeting => meeting.Id).ToList();
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }
    }
}
