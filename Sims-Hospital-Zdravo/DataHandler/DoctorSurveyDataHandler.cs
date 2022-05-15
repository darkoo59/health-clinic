using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class DoctorSurveyDataHandler
    {
        public List<DoctorSurvey> ReadAll()
        {
            string surveySerialized = System.IO.File.ReadAllText(Path);
            List<DoctorSurvey> surveys = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DoctorSurvey>>(surveySerialized);
            return surveys;
        }

        public void Write(List<DoctorSurvey> surveys)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(surveys);
            System.IO.File.WriteAllText(Path, serialized);
        }
        private String Path = @"..\..\Resources\doctor_surveys.txt";
    }
}
