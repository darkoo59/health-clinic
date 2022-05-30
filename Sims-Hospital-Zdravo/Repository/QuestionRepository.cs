using Sims_Hospital_Zdravo.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class QuestionRepository
    {
        public DoctorQuestionDataHandler doctorQuestionDataHandler;
        public HospitalQuestionDataHandler hospitalQuestionDataHandler;
        public List<string> hospitalQuestions;
        public List<string> doctorQuestions;
        public QuestionRepository(DoctorQuestionDataHandler doctorQuestionDataHandler, HospitalQuestionDataHandler hospitalQuestionDataHandler)
        {
            this.doctorQuestionDataHandler = doctorQuestionDataHandler;
            this.hospitalQuestionDataHandler = hospitalQuestionDataHandler;
            LoadDataFromFiles();
        }
        public List<String> GetDoctorQuestions() 
        {
            return doctorQuestions;
        }
        public List<String> GetHospitalQuestions()
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
