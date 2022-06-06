using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class MeetingRepository
    {
        private MeetingDataHandler _meetingDataHandler;
        private ObservableCollection<Meeting> _meetings;

        public MeetingRepository(MeetingDataHandler meetingDataHandler)
        {
            _meetings = new ObservableCollection<Meeting>();
            _meetingDataHandler = meetingDataHandler;
            LoadDataFromFiles();
        }

        public void Create(Meeting meeting)
        {
            meeting.Id = GenerateId();
            _meetings.Add(meeting);
            LoadDataToFile();
        }

        public ref ObservableCollection<Meeting> ReadAll()
        {
            return ref _meetings;
        }

        public void Update(Meeting meeting)
        {
            int id = meeting.Id;
            foreach (Meeting meet in _meetings)
            {
                if (id == meet.Id)
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
        }

        public void Delete(Meeting meeting)
        {
            _meetings.Remove(meeting);
            LoadDataToFile();
        }

        public Meeting FindById(int id)
        {

            foreach (Meeting meeting in _meetings)
            {

                if (id == meeting.Id)
                {

                    return meeting;
                }
            }

            return null;
        }

        public void DeleteById(int id)
        {
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

        private int GenerateId()
        {
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Meeting meeting in _meetings)
            {
                ids.Add(meeting.Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }
    }
}
