using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class RenovationDataHandler
    {
        public List<RenovationAppointment> ReadAll()
        {
            string renovationSerialized = System.IO.File.ReadAllText(_path);
            List<RenovationAppointment> renovations =
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<RenovationAppointment>>(renovationSerialized);
            return renovations;
        }

        public void Write(List<RenovationAppointment> renovations)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(renovations);
            System.IO.File.WriteAllText(_path, serialized);
        }

        private string _path = @"..\..\Resources\renovation_appointments.txt";
    }
}