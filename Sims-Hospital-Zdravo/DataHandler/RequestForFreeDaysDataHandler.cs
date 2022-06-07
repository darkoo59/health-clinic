using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using System.Collections.ObjectModel;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class RequestForFreeDaysDataHandler
    {
        public List<FreeDaysRequest> ReadAll()
        {
            string requestSerialized = System.IO.File.ReadAllText(Path);
            List<FreeDaysRequest> request = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FreeDaysRequest>>(requestSerialized);
            return request;
        }

        public void Write(List<FreeDaysRequest> request)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\requests.txt";
    }
}
