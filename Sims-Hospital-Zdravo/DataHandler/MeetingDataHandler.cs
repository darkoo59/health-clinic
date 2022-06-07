using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class MeetingDataHandler
    {
        public ObservableCollection<Meeting> ReadAll()
        {
            // TODO: implement
            string meetingsSerialized = System.IO.File.ReadAllText(_path);
            ObservableCollection<Meeting> meetings = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Meeting>>(meetingsSerialized);
            return meetings;
        }

        public void Write(ObservableCollection<Meeting> meetings)
        {
            // TODO: implement
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(meetings);
            System.IO.File.WriteAllText(_path, serialized);
        }

        private String _path = @"..\..\Resources\meetings.txt";
    }
}
