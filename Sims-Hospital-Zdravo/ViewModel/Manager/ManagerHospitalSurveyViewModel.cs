using System.Collections.Generic;
using System.Windows;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public class ManagerHospitalSurveyViewModel
    {
        private App app;
        private SurveyController surveyController;
        private SurveyStatistics surveyStatistics;
        public List<QuestionStatistic> QuestionStatistics { get; set; }
        public string AverageMark { get; set; }

        public ManagerHospitalSurveyViewModel()
        {
            app = Application.Current as App;
            surveyController = app._surveyController;
            surveyStatistics = surveyController.GetHospitalSurveyStatistics();
            AverageMark = "Average Mark : " + surveyStatistics.AverageMark;
            QuestionStatistics = surveyStatistics.QuestionStatistics;
        }
    }
}