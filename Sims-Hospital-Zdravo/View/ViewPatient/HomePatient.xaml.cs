using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
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
        private Frame frame;
        private AppointmentPatientController appointmentPatientController;
        private AccountController accountController;
        private App app;
        public HomePatient(Frame frame)
        {
            InitializeComponent();
            this.app = Application.Current as App;
            this.frame = frame;
            this.accountController = app._accountController;
            this.appointmentPatientController = new AppointmentPatientController(app._accountRepository);
            this.DataContext = this;

            Apps.AutoGenerateColumns = false;
            Binding date = new Binding("Time.Start");
            date.StringFormat = "{0:dd/MM/yyyy}";
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Date";
            data_column.Binding = date;
            data_column.Width = 100;
            Apps.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Time";
            Binding time = new Binding("Time.Start");
            time.StringFormat = "{0:HH:mm}";
            data_column.Binding = time;
            data_column.Width = 50;
            Apps.Columns.Add(data_column);
            MultiBinding doctor = new MultiBinding();
            doctor.StringFormat = "{0} {1}";
            doctor.Bindings.Add(new Binding("Doctor.Name"));
            doctor.Bindings.Add(new Binding("Doctor.Surname"));
            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor";
            data_column.Binding = doctor;
            data_column.Width = 195;
            Apps.Columns.Add(data_column);
            DataGridCheckBoxColumn data_check_box = new DataGridCheckBoxColumn();
            data_check_box.Header = "Rated";
            data_check_box.Binding = new Binding("Rated");
            data_check_box.Width = 63;
            Apps.Columns.Add(data_check_box);
            Apps.RowHeaderWidth = 0;
            Apps.ItemsSource = appointmentPatientController.FindByPatientIdOld(accountController.GetLoggedAccount().Id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new HospitalSurveyPage(frame);
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)Apps.SelectedItem;
            frame.Content = new AppointmentDetailsPage(appointment);
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment) Apps.SelectedItem;
            if(appointment.Rated == false) frame.Content = new DoctorSurveyPage(appointment, frame);
        }
    }
}
