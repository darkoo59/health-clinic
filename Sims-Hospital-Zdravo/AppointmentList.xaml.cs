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
    public partial class AppointmentList : Window
    {
        public AppointmentPatientController appointmentPatientController;
        public Appointment appointment;
        public ObservableCollection<Appointment> appointments;
        public ObservableCollection<Doctor> doctors;

        public string priority;
        public AppointmentList(AppointmentPatientController appointmentPatientController, Appointment appointment, string priority)
        {
            InitializeComponent();
            this.appointmentPatientController = appointmentPatientController;
            this.appointment = appointment;
            this.priority = priority;
            this.DataContext = this;
            appointments = appointmentPatientController.InitializeList(appointment,priority);
            Apps.ItemsSource = appointments;
            Apps.AutoGenerateColumns = false;

            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Date and Time";
            data_column.Binding = new Binding("_Time.Start");
            Apps.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor Name";
            data_column.Binding = new Binding("_Doctor._Name");
            Apps.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor Surname";
            data_column.Binding = new Binding("_Doctor._Surname");
            Apps.Columns.Add(data_column);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            appointment = (Appointment) Apps.SelectedItem;
            appointmentPatientController.Create(appointment);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
