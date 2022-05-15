using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class QuestionForSurvey
    {
        public string Text { get; set; }
        public bool One { get; set; }
        public bool Two { get; set; }
        public bool Three { get; set; }
        public bool Four { get; set; }
        public bool Five { get; set; }

        public QuestionForSurvey(string text)
        {
            Text = text;
            One = false;
            Two = false;
            Three = false;
            Four = false;
            Five = false;
        }
    }
}
