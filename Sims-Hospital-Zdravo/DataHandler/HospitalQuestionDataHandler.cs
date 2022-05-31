using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class HospitalQuestionDataHandler
    {
        public List<QuestionForSurvey> ReadAll()
        {
            string questionSerialized = System.IO.File.ReadAllText(Path);
            List<QuestionForSurvey> questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<QuestionForSurvey>>(questionSerialized);
            return questions;
        }
        private string Path = @"..\..\Resources\questions_hospital.txt";
    }
}
