using System.Collections.Generic;

namespace Sims_Hospital_Zdravo.Model
{
    public class QuestionStatistic
    {
        private string _questionContent;
        private List<MarkStatistic> _marks;

        public QuestionStatistic(string questionContent, List<MarkStatistic> marks)
        {
            _questionContent = questionContent;
            _marks = marks;
        }

        public string QuestionContent
        {
            get => _questionContent;
            set => _questionContent = value;
        }

        public List<MarkStatistic> Marks
        {
            get => _marks;
            set => _marks = value;
        }
    }
}