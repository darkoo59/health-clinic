using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface ISurveyStatistic
    {
        int GetMarkSum();
        QuestionAndRate GetQuestionById(int id);
    }
}