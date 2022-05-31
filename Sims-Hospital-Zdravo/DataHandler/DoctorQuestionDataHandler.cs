using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class DoctorQuestionDataHandler
    {
        public List<string> ReadAll()
        {
            string questionSerialized = System.IO.File.ReadAllText(Path);
            List<string> questions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(questionSerialized);
            return questions;
        }
        private String Path = @"..\..\Resources\questions_doctor.txt";
    }
}
