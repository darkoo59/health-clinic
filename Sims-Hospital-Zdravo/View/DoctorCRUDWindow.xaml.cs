using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for DoctorCRUDWindow.xaml
    /// </summary>
    public partial class DoctorCRUDWindow : Window
    {

        public ObservableCollection<Appointment> DoctorAppointments;
        private DoctorAppointmentController doctorAppController;
        public RoomController roomController;
        private Appointment app;
        public Appointment App
        {
            get
            {
                return this.app;
            }
            set
            {
                this.app = value;
            }
        }

        public DoctorCRUDWindow(DoctorAppointmentController doctorAppController, RoomController rom)
        {
            InitializeComponent();
            this.DataContext = this;
            this.roomController = rom;
            this.doctorAppController = doctorAppController;
            DoctorAppointments = doctorAppController.ReadAll(2);
            
            //this.DataContext = DoctorAppointments;
            dataGridDoctorApps.AutoGenerateColumns = false;
            
                DataGridTextColumn data_column = new DataGridTextColumn();
                data_column.Header = "Start Time";
                data_column.Binding = new Binding("_Time.Start"); 
                dataGridDoctorApps.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Start Time";
            data_column.Binding = new Binding("_Time.End");

            dataGridDoctorApps.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
                data_column.Header = "Patient Name";
                data_column.Binding = new Binding("_Patient._Name");
                dataGridDoctorApps.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Patient Surname";
            data_column.Binding = new Binding("_Patient._Surname");
            dataGridDoctorApps.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Room";
            data_column.Binding = new Binding("_Room._Id");
            dataGridDoctorApps.Columns.Add(data_column);




            dataGridDoctorApps.ItemsSource = DoctorAppointments;
            dataGridDoctorApps.Items.Refresh();
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this appointment?", "Delete", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    doctorAppController.DeleteByID((Appointment)dataGridDoctorApps.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editAppointment_Click(object sender, RoutedEventArgs e)
        {
            app = dataGridDoctorApps.SelectedValue as Appointment;
            if (app != null)
            {
                DoctorUpdateAppointment editAppointment = new DoctorUpdateAppointment(doctorAppController, app, roomController) { DataContext = dataGridDoctorApps.SelectedItem };

                editAppointment.Show();
            }
            else
            {
                MessageBox.Show("Chose appointment you want to edit.");
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            DoctorCreateAppointment addAppointment = new DoctorCreateAppointment(doctorAppController,roomController);
            addAppointment.Show();

        }
    }

    
}
