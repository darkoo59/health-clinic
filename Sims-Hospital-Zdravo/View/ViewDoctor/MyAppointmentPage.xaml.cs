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
using Sims_Hospital_Zdravo.Controller;
using System.Collections.ObjectModel;
using Model;
using Controller;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for MyAppointmentPage.xaml
    /// </summary>
    public partial class MyAppointmentPage : Page
    {

        public DoctorAppointmentController docController;
        public MedicalRecordController medicalRecordController;
        private PatientMedicalRecordController PatientMedicalRecordController;
        public ObservableCollection<Appointment> appointmentsScheduled;
        private App app;
        private int doctorId;
        private DateTime AppointmentDate;
        private AnamnesisController anamnesisController;
        public MyAppointmentPage(DoctorAppointmentController doctorAppointmentController, AnamnesisController anamnesisController, MedicalRecordController medicalRecordController, int id)
        {
            InitializeComponent();

            app = App.Current as App;

            
            // this.DataContext = this;
            this.doctorId = id;

            this.medicalRecordController = medicalRecordController;
            this.PatientMedicalRecordController = app._patientMedRecController;
            this.docController = doctorAppointmentController;

            this.appointmentsScheduled = docController.GetByDoctorID(id);
            this.DoctorAppointments.ItemsSource = appointmentsScheduled;
            this.anamnesisController = anamnesisController;
            DoctorAppointments.AutoGenerateColumns = false;
            //Button btnmedical = new Button();

            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Start Time";
            data_column.Binding = new Binding("_Time.Start");
            DoctorAppointments.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "End Time";
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

            


        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = DoctorAppointments.SelectedValue as Appointment;
            if (appointment != null)
            {
                Patient patient = appointment._Patient;
                MedicalRecord record = PatientMedicalRecordController.findMedicalRecordByPatient(patient);
                PatientMedicalRecord medRed = new PatientMedicalRecord(medicalRecordController, docController, anamnesisController, appointment, record, doctorId);
                //medRed.Show();
            }
            else
            {
                MessageBox.Show("Chose whose medical record you want to see.");
            }



        }

        private void AppointmentShow_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AppointmentDate = AppointmentShow.SelectedDate.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chose date");
            }


            this.appointmentsScheduled = docController.FilterAppointmentsByDate(AppointmentDate);
            this.DoctorAppointments.ItemsSource = appointmentsScheduled;





        }

    }
 }

