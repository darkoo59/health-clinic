using System.Collections.Generic;

namespace Sims_Hospital_Zdravo.Model
{
    public class SurveyStatistics
    {
        private List<QuestionStatistic> _questionStatistics;
        private double _averageMark;

        public SurveyStatistics(List<QuestionStatistic> questionStatistics, double averageMark)
        {
            _questionStatistics = questionStatistics;
            _averageMark = averageMark;
        }

        public List<QuestionStatistic> QuestionStatistics
        {
            get => _questionStatistics;
            set => _questionStatistics = value;
        }

        public double AverageMark
        {
            get => _averageMark;
            set => _averageMark = value;
        }
    }
}