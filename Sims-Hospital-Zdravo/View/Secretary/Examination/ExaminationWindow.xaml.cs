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
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for ExaminationWindow.xaml
    /// </summary>
    public partial class ExaminationWindow : Window,IUpdateFilesObserver
    {
        private SecretaryAppointmentController secretaryAppointmentController;
        public ObservableCollection<Appointment> appointments;
        public ExaminationWindow(SecretaryAppointmentController controller)
        {
            InitializeComponent();
            this.secretaryAppointmentController = controller;
            appointmentsDatePicker.SelectedDate = DateTime.Today;
            GridAppointments.AutoGenerateColumns = false;
            
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Doctor name";
            dataColumn.Binding = new Binding("_Doctor._Name"); 
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Doctor surname";
            dataColumn.Binding = new Binding("_Doctor._Surname"); 
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Patient name";
            dataColumn.Binding = new Binding("_Patient._Name"); 
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Patient surname";
            dataColumn.Binding = new Binding("_Patient._Surname"); 
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Room type";
            dataColumn.Binding = new Binding("_Room._Type"); 
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Start at";
            dataColumn.Binding = new Binding("_Time.Start"); 
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "End at";
            dataColumn.Binding = new Binding("_Time.End"); 
            GridAppointments.Columns.Add(dataColumn);
            
            appointments = secretaryAppointmentController.ReadAllAppointmentsForDate(DateTime.Today);
            GridAppointments.ItemsSource = appointments;
            GridAppointments.Items.Refresh();
        }

        private void appDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GridAppointments.ItemsSource = null;
            //string dateString = appointmentsDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            //GridAppointments.ItemsSource = secretaryAppointmentController.ReadAllAppointmentsForDate(DateTime.Parse(dateString));
            NotifyUpdated();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to cancel this appointment?", "Cancel appointment", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                secretaryAppointmentController.Delete((Appointment)GridAppointments.SelectedItem);
                NotifyUpdated();
                //GridAppointments.ItemsSource = null;
                //string dateString = appointmentsDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                //GridAppointments.ItemsSource = secretaryAppointmentController.ReadAllAppointmentsForDate(DateTime.Parse(dateString));
            }
        }

        private void Reschedule_Click(object sender, RoutedEventArgs e)
        {
            if (GridAppointments.SelectedItem != null)
            {
                RescheduleExaminationWindow rescheduleWindow = new RescheduleExaminationWindow((Appointment)GridAppointments.SelectedItem,secretaryAppointmentController);
                rescheduleWindow.Show();
            }else
                MessageBox.Show("Appointment isn't selected", "Please select appointment", MessageBoxButton.OK);
        }
        
        public void NotifyUpdated()
        {
            appointments = secretaryAppointmentController.ReadAllAppointmentsForDate(DateTime.Parse(appointmentsDatePicker.SelectedDate.ToString()));
            GridAppointments.ItemsSource = appointments;

        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            UpdateChoosePatientWindow choosePatient = new UpdateChoosePatientWindow(secretaryAppointmentController);
            choosePatient.Show();
        }
    }
}
