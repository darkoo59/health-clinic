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
        private PatientMedicalRecordController patMedRecordController;
        private MedicalRecord medRecord;
        private Appointment app;
        private AnamnesisController anamnesisController;
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
        public PatientMedicalRecord(MedicalRecord med,DoctorAppointmentController doctorAppointmentController, AnamnesisController anamnesisController,Appointment app )
        {
            this.medRecord = med;
            this.app = app;
            InitializeComponent();
            this.DataContext = med;
            this._doctorAppointmentController = doctorAppointmentController;
            this.anamnesisController = anamnesisController;
            Binding binding = new Binding("_Patient._Name");
            binding.Source = med;
            Patientname.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("_Patient._Surname");
            Patientsurname.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("_Patient._BirthDate");
            birthdate.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("_Gender");
            genderPat.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("_Patient._PhoneNumber");
            number.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("_Patient._Address");
            adress.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("_MaritalStatus");
            marital.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("_Patient._Jmbg");
            uid.SetBinding(TextBlock.TextProperty, binding);
            
            

            
            
            AllergensBox.DataContext = med;
            AllergensBox.ItemsSource = med._PatientAllergens._Allergens;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnamnesisWindow anamnesisWindow = new AnamnesisWindow(anamnesisController,_doctorAppointmentController, medRecord,app);
            anamnesisWindow.Show();
        }
    }
}
