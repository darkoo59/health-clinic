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
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Page
    {
        public AppointmentPatientController appointmentPatientController;
        App app;
        Frame frame;
        public AccountController accountController;
        public PatientWindow(Frame frame)
        {
            app = Application.Current as App;
            this.frame = frame;
            InitializeComponent();
            this.accountController = app._accountController;
            this.appointmentPatientController = new AppointmentPatientController(app._accountRepository);
            this.DataContext = this;

            McDataGrid.AutoGenerateColumns = false;
            Binding date = new Binding("Time.Start");
            date.StringFormat = "{0:dd/MM/yyyy}";
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Date";
            data_column.Binding = date;
            data_column.Width = 100;
            McDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Time";
            Binding time = new Binding("Time.Start");
            time.StringFormat = "{0:HH:mm}";
            data_column.Binding = time;
            data_column.Width = 50;
            McDataGrid.Columns.Add(data_column);
            MultiBinding doctor = new MultiBinding();
            doctor.StringFormat = "{0} {1}";
            doctor.Bindings.Add(new Binding("Doctor.Name"));
            doctor.Bindings.Add(new Binding("Doctor.Surname"));
            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor";
            data_column.Binding = doctor;
            data_column.Width = 195;
            McDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Room";
            data_column.Binding = new Binding("Room.Id");
            data_column.Width = 63;
            McDataGrid.Columns.Add(data_column);
            McDataGrid.RowHeaderWidth = 0;
            McDataGrid.ItemsSource = appointmentPatientController.FindByPatientIdNew(accountController.GetLoggedAccount().Id);
            McDataGrid.Items.Refresh();
            Update.Visibility = Visibility.Hidden;
            Delete.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PatientCreate(frame);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (McDataGrid.SelectedItem == null)
                {
                    throw new Exception("Select an appointment");
                }
                Appointment appointment = (Appointment)McDataGrid.SelectedValue;
                frame.Content = new PatientUpdate(frame, appointment) { DataContext = McDataGrid.SelectedItem };
            }
            catch (Exception m)
            {
                Update.Visibility = Visibility.Visible;
                Delete.Visibility = Visibility.Hidden;
                Update.Content = m.Message;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                if (McDataGrid.SelectedItem == null)
                {
                    throw new Exception("Select an appointment");
                }
                appointmentPatientController.Delete((Appointment)McDataGrid.SelectedItem);
            }
            catch (Exception m)
            {
                Update.Visibility = Visibility.Hidden;
                Delete.Visibility = Visibility.Visible;
                Delete.Content = m.Message;
            }
        }
    }
}