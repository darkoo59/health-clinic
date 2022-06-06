using Controller;
using Model;
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
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for AppointmentList.xaml
    /// </summary>
    public partial class AppointmentList : Page
    {
        App app;
        public AppointmentPatientController appointmentPatientController;
        public Appointment appointment;
        public ObservableCollection<Appointment> appointments;
        public ObservableCollection<Doctor> doctors;
        private Frame frame;
        public string priority;
        public AppointmentList(Frame frame, Appointment appointment, string priority)
        {
            this.frame = frame;
            InitializeComponent();
            app = Application.Current as App;
            this.appointmentPatientController = app._appointmentPatientController;
            this.appointment = appointment;
            this.priority = priority;
            this.DataContext = this;
            appointments = appointmentPatientController.InitializeList(appointment,priority);
            Apps.ItemsSource = appointments;
            Apps.AutoGenerateColumns = false;

            Binding date = new Binding("_Time.Start");
            date.StringFormat = "{0:dd/MM/yyyy}";
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Date";
            data_column.Binding = date;
            Apps.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = "Time";
            Binding time = new Binding("_Time.Start");
            time.StringFormat = "{0:HH:mm}";
            data_column.Binding = time;
            Apps.Columns.Add(data_column);

            MultiBinding doctor = new MultiBinding();
            doctor.StringFormat = "{0} {1}";
            doctor.Bindings.Add(new Binding("_Doctor._Name"));
            doctor.Bindings.Add(new Binding("_Doctor._Surname"));
            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor";
            data_column.Binding = doctor;
            data_column.Width = 338;
            Apps.Columns.Add(data_column);

            if (appointments.Contains(appointment))
            {
                Apps.SelectedIndex = 0;
                Text.Text = "Your appointment is available";
            }
            else if (appointments.Count > 0)
            {
                Text.Text = "There is no available appointment with that parameters, here's some available suggestions";
            }
            else 
            {
                Text.Text = "There is no available appointment with that parameters";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            appointment = (Appointment) Apps.SelectedItem;
            appointmentPatientController.Create(appointment);
            frame.Content = new PatientWindow(frame);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            frame.Content = new PatientCreate(frame);
        }
    }
}
