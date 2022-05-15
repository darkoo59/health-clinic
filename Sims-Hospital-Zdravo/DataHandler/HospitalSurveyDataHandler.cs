using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class HospitalSurveyDataHandler
    {
        public List<HospitalSurvey> ReadAll()
        {
            string surveySerialized = System.IO.File.ReadAllText(Path);
            List<HospitalSurvey> surveys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HospitalSurvey>>(surveySerialized);
            return surveys;
        }

        public void Write(List<HospitalSurvey> surveys)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(surveys);
            System.IO.File.WriteAllText(Path, serialized);
        }
        private String Path = @"..\..\Resources\hospital_surveys.txt";
    }
}
