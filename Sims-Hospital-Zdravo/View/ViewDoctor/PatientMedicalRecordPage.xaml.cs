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
using Sims_Hospital_Zdravo.ViewModel.dd;

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
        public PatientMedicalRecordPage(MedicalRecord med, Frame frame,int id)
        {
            InitializeComponent();
            this.medRecord = med;
            this.doctorId = id;
            this.frame = frame;
            this.DataContext = new PatientMedicalRecordViewModel(med.Id);
            


        }

        private void PrescribeButton_Click(object sender, RoutedEventArgs e)
        {
            ListOfMedecinesinSystem listOfMedecinesinSystem = new ListOfMedecinesinSystem(doctorId,medRecord,frame);
            frame.Content = listOfMedecinesinSystem;
        }

       

        private void MedicalreprotClick(object sender, RoutedEventArgs e)
        {
           AnamnesisListPage anamnesisListPage = new AnamnesisListPage(doctorId,medRecord,frame);
            frame.Content = anamnesisListPage;
        }

        private void medicalHistoryClick(object sender, RoutedEventArgs e)
        {
            PatientMedicalHistory patientMedicalHistory = new PatientMedicalHistory();
            frame.Content = patientMedicalHistory;
        }

        private void LabaratoryTestClick(object sender, RoutedEventArgs e)
        {
            LabaratoryResultsPage labaratoryResultsPage = new LabaratoryResultsPage();
            frame.Content = labaratoryResultsPage;
        }

        private void therapyButton_Click(object sender, RoutedEventArgs e)
        {
            TherapyPage therapyPage = new TherapyPage(medRecord.Id,medRecord,frame);
            frame.Content = therapyPage;
        }
    }
}
