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
using Sims_Hospital_Zdravo.Controller;
using Controller;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for SearchPatient.xaml
    /// </summary>
    public partial class SearchPatient : Page
    {
        private AnamnesisController anamnesisController;
        private DoctorAppointmentController doctorAppointmentController;
        private MedicalRecordController medicalRecordController;
        private Frame frame;
        public SearchPatient(AnamnesisController anamnesisController,DoctorAppointmentController doctorAppointmentController,MedicalRecordController medicalRecordController,Frame frame)
        {
            InitializeComponent();
            this.DataContext = this;
            this.frame = frame;
            this.anamnesisController = anamnesisController;
            this.doctorAppointmentController = doctorAppointmentController;
            this.medicalRecordController = medicalRecordController;
            MedicalRecordDataGrid.ItemsSource = medicalRecordController.ReadAll();
        }

        private void PatientMedicalRecordClick(object sender, RoutedEventArgs e)
        {
            
            MedicalRecord medicalRecord = MedicalRecordDataGrid.SelectedValue as MedicalRecord;
            if (medicalRecord != null)
            {
                PatientTabs patientTabs = new PatientTabs(medicalRecord,frame);
                frame.Content = patientTabs;
            }
            else
            {
                MessageBox.Show("Chose whose medical record you want to see.");
            }
        }
    }
}
