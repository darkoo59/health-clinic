using Controller;
using Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for HomePatientPage.xaml
    /// </summary>
    public partial class HomePatient : Page
    {
        public Frame patient;
        public AppointmentPatientController appointmentPatientController;
        App app;
        public HomePatient(Frame patient)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.patient = patient;
            this.appointmentPatientController = app._appointmentPatientController;
            this.DataContext = this;

            Apps.AutoGenerateColumns = false;
            MultiBinding date = new MultiBinding();
            date.StringFormat = "{0}/{1}/{2}";
            date.Bindings.Add(new Binding("_Time.Start.Day"));
            date.Bindings.Add(new Binding("_Time.Start.Month"));
            date.Bindings.Add(new Binding("_Time.Start.Year"));
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Date";
            data_column.Binding = date;
            Apps.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Time";
            MultiBinding time = new MultiBinding();
            time.StringFormat = "{}{0:00}:{1:00}";
            time.Bindings.Add(new Binding("_Time.Start.TimeOfDay.Hours"));
            time.Bindings.Add(new Binding("_Time.Start.TimeOfDay.Minutes"));
            data_column.Binding = time;
            Apps.Columns.Add(data_column);
            MultiBinding doctor = new MultiBinding();
            doctor.StringFormat = "{0} {1}";
            doctor.Bindings.Add(new Binding("_Doctor._Name"));
            doctor.Bindings.Add(new Binding("_Doctor._Surname"));
            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor";
            data_column.Binding = doctor;
            Apps.Columns.Add(data_column);
            DataGridCheckBoxColumn data_check_box = new DataGridCheckBoxColumn();
            data_check_box.Header = "Rated";
            data_check_box.Binding = new Binding("Rated");
            Apps.Columns.Add(data_check_box);
            Apps.ItemsSource = appointmentPatientController.FindByPatientID(1);
            Apps.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            patient.Content = new HospitalSurveyPage(patient);
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment) Apps.SelectedItem;
            patient.Content = new DoctorSurveyPage(appointment, patient);
        }
    }
}
