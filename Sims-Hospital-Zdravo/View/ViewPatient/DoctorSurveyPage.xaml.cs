using Controller;
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
        private SurveyController surveyController;
        private AppointmentPatientController appointmentPatientController;
        private App app;
        private ObservableCollection<QuestionForSurvey> questions;
        private Frame frame;
        public DoctorSurveyPage(Appointment appointment, Frame frame)
        {
            app = Application.Current as App;
            InitializeComponent();
            this.frame = frame;
            this.surveyController = new SurveyController();
            appointmentPatientController = new AppointmentPatientController(app._accountRepository);
            this.appointment = appointment;
            this.DataContext = this;
            InitalizeList();
            Survey.ItemsSource = questions;

            Survey.AutoGenerateColumns = false;
        }
        private void InitalizeList()
        {
            questions = new ObservableCollection<QuestionForSurvey>();
            foreach (QuestionForSurvey questionForSurvey in surveyController.GetDoctorQuestions())
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
            surveyController.CreateDoctorSurvey(new DoctorSurvey(appointment, questionsAndRates));
            appointmentPatientController.SetAppointmentRated(appointment);
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
