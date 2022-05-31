using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecord.xaml
    /// </summary>
    public partial class PatientMedicalRecord : Window
    {

        private DoctorAppointmentController _doctorAppointmentController;
        private MedicalRecordController _medicalRecordController;
        private MedicalRecord medRecord;
        private Appointment app;
        private AnamnesisController anamnesisController;
        private int doctorId;
        private Sims_Hospital_Zdravo.Model.Allergens allergens;

        public MedicalRecord _MedicalRecord
        {
            get
            {
                return this.medRecord;
            }
            set
            {
                this.medRecord = value;
            }
        }
        public PatientMedicalRecord(MedicalRecordController medicalRecordController,DoctorAppointmentController doctorAppointmentController, AnamnesisController anamnesisController,Appointment app,MedicalRecord med,int id )
        {
            
            this.medRecord = med;
            this.app = app;
            this.doctorId = id;
            InitializeComponent();
            this._medicalRecordController = medicalRecordController;
            this.DataContext = med;
            this._doctorAppointmentController = doctorAppointmentController;
            this.anamnesisController = anamnesisController;
            Binding binding = new Binding("Patient._Name");
            binding.Source = med;
            Patientname.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient._Surname");
            Patientsurname.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient._BirthDate");
            birthdate.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Gender");
            genderPat.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient._PhoneNumber");
            number.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient._Address");
            adress.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("MaritalStatus");
            marital.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient._Jmbg");
            uid.SetBinding(TextBlock.TextProperty, binding);
            
            

            
            
            AllergensBox.DataContext = med;
            //AllergensBox.ItemsSource = med._PatientAllergens._Allergens;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnamnesisWindow anamnesisWindow = new AnamnesisWindow(anamnesisController,_doctorAppointmentController, medRecord,app);
            anamnesisWindow.Show();
        }

        

        private void Prescribe_medicine(object sender, RoutedEventArgs e)
        {
            //PrescriptionWindow prescriptionWindow = new PrescriptionWindow(_medicalRecordController,medRecord,doctorId);
            //prescriptionWindow.Show();

        }
    }
}
