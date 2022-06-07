using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Service;

namespace Sims_Hospital_Zdravo.Controller
{
    public class SurveyController
    {
        public SurveyService surveyService;

        public SurveyController()
        {
            this.surveyService = new SurveyService();
        }

        public void CreateDoctorSurvey(DoctorSurvey doctorSurvey)
        {
            surveyService.CreateDoctorSurvey(doctorSurvey);
        }

        public void CreateHospitalSurvey(HospitalSurvey hospitalSurvey)
        {
            surveyService.CreateHospitalSurvey(hospitalSurvey);
        }

        public List<QuestionForSurvey> GetHospitalQuestions()
        {
            return surveyService.GetHospitalQuestions();
        }

        public List<QuestionForSurvey> GetDoctorQuestions()
        {
            return surveyService.GetDoctorQuestions();
        }

        public SurveyStatistics GetDoctorSurveyStatistics(int doctorId)
        {
            return surveyService.GetDoctorSurveyStatistics(doctorId);
        }

        public SurveyStatistics GetHospitalSurveyStatistics()
        {
            return surveyService.GetHospitalSurveyStatistics();
        }
    }
}