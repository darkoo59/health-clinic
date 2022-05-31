using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Service
{
    public class SurveyService
    {
        public DoctorSurveyRepository doctorSurveyRepository;
        public HospitalSurveyRepository hospitalSurveyRepository;
        public QuestionRepository questionRepository;

        public SurveyService(DoctorSurveyRepository doctorSurveyRepository, HospitalSurveyRepository hospitalSurveyRepository, QuestionRepository questionRepository)
        {
            this.doctorSurveyRepository = doctorSurveyRepository;
            this.hospitalSurveyRepository = hospitalSurveyRepository;
            this.questionRepository = questionRepository;
        }
        public void CreateDoctorSurvey(DoctorSurvey doctorSurvey) 
        {
            doctorSurveyRepository.Create(doctorSurvey);
        }
        public void CreateHospitalSurvey(HospitalSurvey hospitalSurvey)
        {
            hospitalSurveyRepository.Create(hospitalSurvey);
        }
        public List<QuestionForSurvey> GetHospitalQuestions()
        {
            return questionRepository.GetHospitalQuestions();
        }
        public List<QuestionForSurvey> GetDoctorQuestions()
        {
            return questionRepository.GetDoctorQuestions();
        }
    }
}
