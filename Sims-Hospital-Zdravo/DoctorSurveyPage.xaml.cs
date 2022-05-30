using Model;
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
    /// Interaction logic for DoctorSurveyPage.xaml
    /// </summary>
    public partial class DoctorSurveyPage : Page
    {
        private Appointment appointment;
        App app;
        private SurveyController surveyController;
        List<QuestionForSurvey> questions;
        Frame frame;
        public DoctorSurveyPage(Appointment appointment, Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            app = Application.Current as App;
            this.surveyController = app._surveyController;
            this.appointment = appointment;
            this.appointment.Rated = false;
            this.DataContext = this;
            Survey.ItemsSource = getQuestions();

            Survey.AutoGenerateColumns = false;
        }
        public List<QuestionForSurvey> getQuestions()
        {
            questions = new List<QuestionForSurvey>();
            foreach (string s in surveyController.GetDoctorQuestions())
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
            surveyController.CreateDoctorSurvey(new DoctorSurvey(appointment, questionsAndRates));
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
