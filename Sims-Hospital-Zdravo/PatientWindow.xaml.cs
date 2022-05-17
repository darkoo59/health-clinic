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
        Frame frame;

        public PatientWindow(Frame frame)
        {
            app = Application.Current as App;
            this.frame = frame;
            InitializeComponent();
            app._taskScheduleTimer.AddObserver(this);
            this.appointmentPatientController = app._appointmentPatientController;
            this.DataContext = this;

            McDataGrid.AutoGenerateColumns = false;
            Binding date = new Binding("_Time.Start");
            date.StringFormat = "{0:dd/MM/yyyy}";
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Date";
            data_column.Binding = date;
            McDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Time";
            Binding time = new Binding("_Time.Start");
            time.StringFormat = "{0:HH:mm}";
            data_column.Binding = time;
            McDataGrid.Columns.Add(data_column);
            MultiBinding doctor = new MultiBinding();
            doctor.StringFormat = "{0} {1}";
            doctor.Bindings.Add(new Binding("_Doctor._Name"));
            doctor.Bindings.Add(new Binding("_Doctor._Surname"));
            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor";
            data_column.Binding = doctor;
            data_column.Width = 190;
            McDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Room";
            data_column.Binding = new Binding("_Room._Id");
            McDataGrid.Columns.Add(data_column);

            McDataGrid.ItemsSource = appointmentPatientController.FindByPatientIdNew(1);
            McDataGrid.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PatientCreate(frame);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)McDataGrid.SelectedValue;
            frame.Content = new PatientUpdate(frame, appointment) { DataContext = McDataGrid.SelectedItem };
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                appointmentPatientController.Delete((Appointment)McDataGrid.SelectedItem);
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        public void Notify(Notification notification)
        {
            if (typeof(MedicineCreatedNotification).IsInstanceOfType(notification)) return;
            MessageBox.Show(notification.Content);
        }
    }
}