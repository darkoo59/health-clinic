using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        public DoctorQuestionDataHandler doctorQuestionDataHandler;
        public HospitalQuestionDataHandler hospitalQuestionDataHandler;
        public List<QuestionForSurvey> hospitalQuestions;
        public List<QuestionForSurvey> doctorQuestions;
        public QuestionRepository()
        {
            this.doctorQuestionDataHandler = new DoctorQuestionDataHandler();
            this.hospitalQuestionDataHandler = new HospitalQuestionDataHandler();
            LoadDataFromFiles();
        }
        public List<QuestionForSurvey> GetDoctorQuestions() 
        {
            return doctorQuestions;
        }
        public List<QuestionForSurvey> GetHospitalQuestions()
        {
            return hospitalQuestions;
        }
        public void LoadDataFromFiles()
        {
            doctorQuestions = doctorQuestionDataHandler.ReadAll();
            hospitalQuestions = hospitalQuestionDataHandler.ReadAll();
        }
    }
}
