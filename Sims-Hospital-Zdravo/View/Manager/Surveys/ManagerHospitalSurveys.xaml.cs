using System.Windows;
using System.Windows.Controls;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.View.Manager.Surveys
{
    public partial class ManagerHospitalSurveys : Page
    {
        private App app;
        private SurveyController surveyController;
        private SurveyStatistics surveyStatistics;

        public ManagerHospitalSurveys()
        {
            InitializeComponent();
            app = Application.Current as App;
            surveyController = app._surveyController;
            surveyStatistics = surveyController.GetHospitalSurveyStatistics();
            SurveyStats.ItemsSource = surveyStatistics.QuestionStatistics;
        }
    }
}