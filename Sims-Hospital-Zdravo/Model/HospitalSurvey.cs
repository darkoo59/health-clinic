using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Model
{
    public class HospitalSurvey : ISurveyStatistic
    {
        private List<QuestionAndRate> _questionsAndRates;

        public HospitalSurvey(List<QuestionAndRate> questionsAndRates)
        {
            QuestionsAndRates = questionsAndRates;
        }

        public List<QuestionAndRate> QuestionsAndRates
        {
            get { return _questionsAndRates; }
            set { this._questionsAndRates = value; }
        }

        public int GetMarkSum()
        {
            return _questionsAndRates.Sum(question => question.Rate);
        }

        public QuestionAndRate GetQuestionById(int id)
        {
            return _questionsAndRates.FirstOrDefault(question => question.Id == id);
        }
    }
}