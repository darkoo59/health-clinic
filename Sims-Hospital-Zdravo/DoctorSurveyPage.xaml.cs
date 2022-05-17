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
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Rate how much do you agree with this statements";
            data_column.Binding = new Binding("Text");
            data_column.Width = 484;
            Survey.Columns.Add(data_column);
            Survey.Columns.Add(getCheckBox("1", "One"));
            Survey.Columns.Add(getCheckBox("2", "Two"));
            Survey.Columns.Add(getCheckBox("3", "Three"));
            Survey.Columns.Add(getCheckBox("4", "Four"));
            Survey.Columns.Add(getCheckBox("5", "Five"));
        }
        private DataGridCheckBoxColumn getCheckBox(string header, string binding)
        {
            DataGridCheckBoxColumn checkBox = new DataGridCheckBoxColumn();
            checkBox.Header = header;
            checkBox.Binding = new Binding(binding);
            checkBox.Width = 40;
            return checkBox;
        }
        public List<QuestionForSurvey> getQuestions()
        {
            questions = new List<QuestionForSurvey>();
            questions.Add(new QuestionForSurvey("Doctor was polite."));
            questions.Add(new QuestionForSurvey("Room was clean."));
            questions.Add(new QuestionForSurvey("Appointment held on schedule."));
            questions.Add(new QuestionForSurvey("Doctor explained everything in understandable way."));
            questions.Add(new QuestionForSurvey("Doctor is good overall."));
            return questions;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] rates = {0,0,0,0,0};
            int i = 0;
            foreach (QuestionForSurvey questionForSurvey in questions)
            {
                rates[i] = CheckIfComboBoxChecked(questionForSurvey);
                i++;
            }
            surveyController.CreateDoctorSurvey(new DoctorSurvey(appointment, rates[0], rates[1], rates[2], rates[3], rates[4]));
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
