using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IQuestionRepository
    {
        List<QuestionForSurvey> GetDoctorQuestions();
        List<QuestionForSurvey> GetHospitalQuestions();
    }
}
