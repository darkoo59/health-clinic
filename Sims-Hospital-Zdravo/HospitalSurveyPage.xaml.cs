using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for HospitalSurvey.xaml
    /// </summary>
    public partial class HospitalSurveyPage : Page
    {
        private App app;
        private SurveyController surveyController;
        private ObservableCollection<QuestionForSurvey> questions;
        private Frame frame;
        public HospitalSurveyPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            app = Application.Current as App;
            this.surveyController = app._surveyController;
            this.DataContext = this;
            Survey.ItemsSource = getQuestions();

            Survey.AutoGenerateColumns = false;   
        }
        public ObservableCollection<QuestionForSurvey> getQuestions()
        {
            questions = new ObservableCollection<QuestionForSurvey>();
            foreach (string s in surveyController.GetHospitalQuestions())
            {
                questions.Add(new QuestionForSurvey(s));
            }
            return questions;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<QuestionAndRate> questionsAndRates = new List<QuestionAndRate>();
            int i = 1;
            foreach (QuestionForSurvey questionForSurvey in questions)
            {
                questionsAndRates.Add(new QuestionAndRate(i, questionForSurvey.Text, CheckIfComboBoxChecked(questionForSurvey)));
                i++;
            }
            surveyController.CreateHospitalSurvey(new HospitalSurvey(questionsAndRates));
            frame.Content = new HomePatient(frame);
        }
        private int CheckIfComboBoxChecked(QuestionForSurvey questionForSurvey)
        {
            int checkbox = 0;
            if (questionForSurvey.One) checkbox = 1;
            if (questionForSurvey.Two) checkbox = 2;
            if (questionForSurvey.Three) checkbox = 3;
            if (questionForSurvey.Four) checkbox = 4;
            if (questionForSurvey.Five) checkbox = 5;
            return checkbox;
        }
    }
}
