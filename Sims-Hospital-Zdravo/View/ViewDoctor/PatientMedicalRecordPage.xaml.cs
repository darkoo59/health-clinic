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
using Controller;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecordPage.xaml
    /// </summary>
    public partial class PatientMedicalRecordPage : UserControl
    {
        private DoctorAppointmentController _doctorAppointmentController;
        private MedicalRecordController _medicalRecordController;
        private MedicalRecord medRecord;
        private Appointment app;
        private AnamnesisController anamnesisController;
        private int doctorId;
        private Frame frame;
        public PatientMedicalRecordPage(MedicalRecord med, Frame frame)
        {
            InitializeComponent();
            this.medRecord = med;
            
            this.frame = frame;
            this.DataContext = med;
            Binding binding = new Binding("Patient.Name");
            binding.Source = med;
            PatienNameTxt.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient.Surname");
            PatientSurnameTxt.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient.BirthDate");
            BirthDateTxt.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Gender");
            Gendertxt.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient.PhoneNumber");
            numberTxt.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient.Address");
            //AdressTxt.SetBinding(TextBox.TextProperty, binding);

            binding = new Binding("MaritalStatus");
            maritalStatusTxt.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("Patient.Jmbg");
           UIDTxt.SetBinding(TextBlock.TextProperty, binding);
        }

        private void PrescribeButton_Click(object sender, RoutedEventArgs e)
        {
            ListOfMedecinesinSystem listOfMedecinesinSystem = new ListOfMedecinesinSystem(doctorId,medRecord,frame);
            frame.Content = listOfMedecinesinSystem;
        }

       

        private void MedicalreprotClick(object sender, RoutedEventArgs e)
        {
            MedicalReport medicalReport = new MedicalReport(medRecord,doctorId, frame);
            frame.Content = medicalReport;
        }

        private void medicalHistoryClick(object sender, RoutedEventArgs e)
        {
            PatientMedicalHistory patientMedicalHistory = new PatientMedicalHistory();
        }

        private void LabaratoryTestClick(object sender, RoutedEventArgs e)
        {
            LabaratoryResultsPage labaratoryResultsPage = new LabaratoryResultsPage();
            frame.Content = labaratoryResultsPage;
        }
    }
}
