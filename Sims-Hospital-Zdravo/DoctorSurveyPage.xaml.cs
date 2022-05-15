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
        Frame Patient;
        public DoctorSurveyPage(Appointment appointment, Frame patient)
        {
            Patient = patient;
            app = Application.Current as App;
            this.surveyController = app._surveyController;
            this.appointment = appointment;
            this.appointment.Rated = false;
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
        public List<QuestionForSurvey> getQuestions()
        {
            questions = new List<QuestionForSurvey>();
            questions.Add(new QuestionForSurvey("Doctor was polite."));
            questions.Add(new QuestionForSurvey("Doctor was professional."));
            questions.Add(new QuestionForSurvey("Room was clean."));
            questions.Add(new QuestionForSurvey("Appointment held on schedule."));
            questions.Add(new QuestionForSurvey("Doctor explained everything in understandable way."));
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
            appointment.Rated = true;
            surveyController.CreateDoctorSurvey(new DoctorSurvey(appointment,rates[0], rates[1], rates[2], rates[3], rates[4]));
            Patient.Content = new HomePatient(Patient);
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
        private void IsCheckable(int checkbox) 
        {
            if (checkbox != 0)
            {
                int i = 0;
                foreach (DataGridColumn dataGridColumn in Survey.Columns)
                {
                    if (i != checkbox) dataGridColumn.IsReadOnly = true;
                    i++;
                }
            }
            else
            {
                foreach (DataGridColumn dataGridColumn in Survey.Columns)
                {
                    dataGridColumn.IsReadOnly = false;
                }
            }
        }
        private void Survey_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //if ((QuestionForSurvey)Survey.CurrentCell.Item != null)
            //{
                //QuestionForSurvey questionForSurvey = (QuestionForSurvey)Survey.CurrentCell.Item;
                //IsCheckable(CheckIfComboBoxChecked(questionForSurvey));
            //}
        }

        private void Survey_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //QuestionForSurvey questionForSurvey = (QuestionForSurvey)Survey.CurrentCell.Item;
            //IsCheckable(CheckIfComboBoxChecked(questionForSurvey));
        }
    }
}
