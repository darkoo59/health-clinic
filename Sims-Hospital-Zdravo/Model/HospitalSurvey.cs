using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class HospitalSurvey
    {
        private List<QuestionAndRate> _questionsAndRates;
        public HospitalSurvey(List<QuestionAndRate> questionsAndRates)
        {
            QuestionsAndRates = questionsAndRates;
        }
        public List<QuestionAndRate> QuestionsAndRates { get { return _questionsAndRates; } set { this._questionsAndRates = value; } }
    }
}
