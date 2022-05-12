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
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Page, INotificationObserver
    {
        AppointmentPatientController appointmentPatientController;
        App app;
        Frame patient;
        public PatientWindow(Frame patient)
        {
            app = Application.Current as App;
            this.patient = patient; 
            InitializeComponent();
            app._taskScheduleTimer.AddObserver(this);
            this.appointmentPatientController = app._appointmentPatientController;
            this.DataContext = this;

            McDataGrid.AutoGenerateColumns = false;
            MultiBinding date = new MultiBinding();
            date.StringFormat = "{0}/{1}/{2}";
            date.Bindings.Add(new Binding("_Time.Start.Day"));
            date.Bindings.Add(new Binding("_Time.Start.Month"));
            date.Bindings.Add(new Binding("_Time.Start.Year"));
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Date";
            data_column.Binding = date;
            McDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Time";
            data_column.Binding = new Binding("_Time.Start.TimeOfDay");
            McDataGrid.Columns.Add(data_column);
            MultiBinding doctor = new MultiBinding();
            doctor.StringFormat = "{0} {1}";
            doctor.Bindings.Add(new Binding("_Doctor._Name"));
            doctor.Bindings.Add(new Binding("_Doctor._Surname"));
            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor";
            data_column.Binding = doctor;
            McDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Room";
            data_column.Binding = new Binding("_Room._Id");
            McDataGrid.Columns.Add(data_column);

            McDataGrid.ItemsSource = appointmentPatientController.FindByPatientID(1);
            McDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            patient.Content = new PatientCreate(appointmentPatientController);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)McDataGrid.SelectedValue;
            patient.Content = new PatientUpdate(appointmentPatientController, appointment) { DataContext = McDataGrid.SelectedItem };
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
            //if (dialogResult == MessageBoxResult.Yes)
            //{
            appointmentPatientController.Delete((Appointment)McDataGrid.SelectedItem);
            //}
        }

        public void Notify(Notification notification)
        {
            if (typeof(MedicineApprovalNotification).IsInstanceOfType(notification)) return;
            MessageBox.Show(notification.Content);
        }
    }
}