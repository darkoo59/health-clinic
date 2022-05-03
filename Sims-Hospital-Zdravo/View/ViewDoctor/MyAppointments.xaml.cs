using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Controller;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for MyAppointments.xaml
    /// </summary>
    public partial class MyAppointments : Window
    {
        public DoctorAppointmentController docController;
        private PatientMedicalRecordController PatientMedicalRecordController;
        public ObservableCollection<Appointment> appointmentsScheduled;
        private App app;
        private DateTime AppointmentDate;
        private AnamnesisController anamnesisController;
        public MyAppointments( )
        {
            app = App.Current as App;
            
            InitializeComponent();
            this.DataContext = this;
            this.PatientMedicalRecordController = app._patientMedRecController;
            this.docController =app._doctorAppointmentController;
            this.appointmentsScheduled = docController.GetByDoctorID(2);
            this.DoctorAppointments.ItemsSource = appointmentsScheduled;
            this.anamnesisController = app._anamnesisController;
            DoctorAppointments.AutoGenerateColumns = false;
            //Button btnmedical = new Button();
           
             DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Start Time";
            data_column.Binding = new Binding("_Time.Start");
            DoctorAppointments.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Start Time";
            data_column.Binding = new Binding("_Time.End");

            DoctorAppointments.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Patient Name";
            data_column.Binding = new Binding("_Patient._Name");
            DoctorAppointments.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Patient Surname";
            data_column.Binding = new Binding("_Patient._Surname");
            DoctorAppointments.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Room";
            data_column.Binding = new Binding("_Room.Id");
            DoctorAppointments.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = "Type";
            data_column.Binding = new Binding("_Type");
            DoctorAppointments.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = " Medical records";
            DoctorAppointments.Columns.Add(data_column);
            

        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = DoctorAppointments.SelectedValue as Appointment;
            if (appointment != null)
            {
                Patient patient = appointment._Patient;
                MedicalRecord record = PatientMedicalRecordController.findMedicalRecordByPatient(patient);
                PatientMedicalRecord medRed = new PatientMedicalRecord(record, docController, anamnesisController, appointment);
                medRed.Show();
            }
            else
            {
                MessageBox.Show("Chose whose medical record you want to see.");
            }
            
            

        }

        private void AppointmentShow_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
             AppointmentDate = AppointmentShow.SelectedDate.Value;
            this.appointmentsScheduled = docController.FilterAppointmentsByDate(AppointmentDate);
            this.DoctorAppointments.ItemsSource = appointmentsScheduled;


        }

        
    }
}
