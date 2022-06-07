using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Service
{
    public class SurveyService
    {
        public DoctorSurveyRepository doctorSurveyRepository;
        public HospitalSurveyRepository hospitalSurveyRepository;
        public QuestionRepository questionRepository;

        public SurveyService()
        {
            this.doctorSurveyRepository = new DoctorSurveyRepository();
            this.hospitalSurveyRepository = new HospitalSurveyRepository();
            this.questionRepository = new QuestionRepository();
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

        public int GetHospitalQuestionCount()
        {
            return GetHospitalQuestions().Count;
        }

        public int GetDoctorQuestionCount()
        {
            return GetDoctorQuestions().Count;
        }

        public List<ISurveyStatistic> GetDoctorSurveys(int doctorId)
        {
            return doctorSurveyRepository.FindAllByDoctorId(doctorId).ConvertAll(x => (ISurveyStatistic)x);
        }

        public List<ISurveyStatistic> GetHospitalSurveys()
        {
            return hospitalSurveyRepository.FindAll().ConvertAll(x => (ISurveyStatistic)x);
        }


        public SurveyStatistics GetDoctorSurveyStatistics(int id)
        {
            double averageMark = CalculateAverageSurveyMark(GetDoctorSurveys(id), GetDoctorQuestionCount());
            List<QuestionStatistic> questionStatistics = CalculateAllQuestionsStatistics(GetDoctorSurveys(id), GetDoctorQuestions());
            return new SurveyStatistics(questionStatistics, averageMark);
        }

        public SurveyStatistics GetHospitalSurveyStatistics()
        {
            double averageMark = CalculateAverageSurveyMark(GetHospitalSurveys(), GetHospitalQuestionCount());
            List<QuestionStatistic> questionStatistics = CalculateAllQuestionsStatistics(GetHospitalSurveys(), GetHospitalQuestions());
            return new SurveyStatistics(questionStatistics, averageMark);
        }

        private double CalculateAverageSurveyMark(List<ISurveyStatistic> surveys, int questionCount)
        {
            int sum = surveys.Sum(survey => survey.GetMarkSum());
            if (surveys.Count == 0) throw new Exception("No surveys!");
            return sum * 1.0 / (questionCount * surveys.Count);
        }

        private List<QuestionStatistic> CalculateAllQuestionsStatistics(List<ISurveyStatistic> surveys, List<QuestionForSurvey> questions)
        {
            return questions.Select(question => FindQuestionInSurveys(surveys, question.Id))
                .Select(CalculateQuestionStatistics)
                .ToList();
        }

        private List<QuestionAndRate> FindQuestionInSurveys(List<ISurveyStatistic> surveys, int questionId)
        {
            return surveys.Select(survey => survey.GetQuestionById(questionId)).ToList();
        }

        private QuestionStatistic CalculateQuestionStatistics(List<QuestionAndRate> questions)
        {
            int[] marks = { 0, 0, 0, 0, 0 };
            foreach (QuestionAndRate question in questions)
            {
                marks[question.Rate - 1] += 1;
            }

            return new QuestionStatistic(questions[0].Question, ConvertArrayToMarkList(marks));
        }

        private List<MarkStatistic> ConvertArrayToMarkList(int[] marks)
        {
            return marks.Select((t, i) => new MarkStatistic(i + 1, t)).ToList();
        }
    }
}