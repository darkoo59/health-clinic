using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        public SurveyController surveyController { get; set; }
        public ObservableCollection<QuestionForSurvey> questions { get; set; }
        public Frame frame { get; set; }
        public Timer timer { get; set; }
        public HospitalSurveyPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            this.surveyController = new SurveyController();
            this.DataContext = this;
            InitalizeList();
            Survey.ItemsSource = questions;
            Survey.AutoGenerateColumns = false;
        }
        private void InitalizeList()
        {
            questions = new ObservableCollection<QuestionForSurvey>();
            foreach (QuestionForSurvey questionForSurvey in surveyController.GetHospitalQuestions())
            {
                SetRadioButtons(questionForSurvey);
                questions.Add(questionForSurvey);
            }
        }
        private void SetRadioButtons(QuestionForSurvey questionForSurvey)
        {
            questionForSurvey.One = false;
            questionForSurvey.Two = false;
            questionForSurvey.Three = false;
            questionForSurvey.Four = false;
            questionForSurvey.Five = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<QuestionAndRate> questionsAndRates = new List<QuestionAndRate>();
            foreach (QuestionForSurvey questionForSurvey in questions)
            {
                questionsAndRates.Add(new QuestionAndRate(questionForSurvey.Id, questionForSurvey.Text, CheckIfComboBoxChecked(questionForSurvey)));
            }
            surveyController.CreateHospitalSurvey(new HospitalSurvey(questionsAndRates));
            frame.Content = new HomePatient(frame);
        }
        private int CheckIfComboBoxChecked(QuestionForSurvey questionForSurvey)
        {
            if (questionForSurvey.One) return 1;
            if (questionForSurvey.Two) return 2;
            if (questionForSurvey.Three) return 3;
            if (questionForSurvey.Four) return 4;
            if (questionForSurvey.Five) return 5;
            return 0;
        }
    }
}