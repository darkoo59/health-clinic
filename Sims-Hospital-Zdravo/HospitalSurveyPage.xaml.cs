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
        private Frame patient;
        public HospitalSurveyPage(Frame frame)
        {
            InitializeComponent();
            this.patient = frame;
            app = Application.Current as App;
            this.surveyController = app._surveyController;
            InitializeComponent();
            this.DataContext = this;
            Survey.ItemsSource = getQuestions();
            Survey.AutoGenerateColumns = false;
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Rate how much do you agree with this statements";
            data_column.Binding = new Binding("Text");
            data_column.Width = 484;
            Survey.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            DataGridCheckBoxColumn checkBox = new DataGridCheckBoxColumn();
            checkBox.Header = "1";
            checkBox.Binding = new Binding("One");
            checkBox.Width = 40;
            Survey.Columns.Add(checkBox);
            checkBox = new DataGridCheckBoxColumn();
            checkBox.Header = "2";
            checkBox.Binding = new Binding("Two");
            checkBox.Width = 40;
            Survey.Columns.Add(checkBox);
            checkBox = new DataGridCheckBoxColumn();
            checkBox.Header = "3";
            checkBox.Binding = new Binding("Three");
            checkBox.Width = 40;
            Survey.Columns.Add(checkBox);
            checkBox = new DataGridCheckBoxColumn();
            checkBox.Header = "4";
            checkBox.Binding = new Binding("Four");
            Survey.Columns.Add(checkBox);
            checkBox.Width = 40;
            checkBox = new DataGridCheckBoxColumn();
            checkBox.Header = "5";
            checkBox.Binding = new Binding("Five");
            checkBox.Width = 40;
            Survey.Columns.Add(checkBox);
        }
        public ObservableCollection<QuestionForSurvey> getQuestions()
        {
            questions = new ObservableCollection<QuestionForSurvey>();
            questions.Add(new QuestionForSurvey("Personnel are polite."));
            questions.Add(new QuestionForSurvey("Personnel are professional."));
            questions.Add(new QuestionForSurvey("Hospital is clean."));
            questions.Add(new QuestionForSurvey("Application is good and easy to use."));
            questions.Add(new QuestionForSurvey("Waiting room is comfortable and peaceful."));
            return questions;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] rates = { 0, 0, 0, 0, 0 };
            int i = 0;
            foreach (QuestionForSurvey questionForSurvey in questions)
            {
                rates[i] = CheckIfComboBoxChecked(questionForSurvey);
                i++;
            }
            surveyController.CreateHospitalSurvey(new HospitalSurvey(rates[0], rates[1], rates[2], rates[3], rates[4]));
            patient.Content = new HomePatient(patient);
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
