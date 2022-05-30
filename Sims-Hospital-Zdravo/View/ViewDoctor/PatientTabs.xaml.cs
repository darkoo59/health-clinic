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
using Sims_Hospital_Zdravo.Model;
using Model;
using Controller;
using System.Collections.ObjectModel;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for PatientTabs.xaml
    /// </summary>
    public partial class PatientTabs : Page
    {
        private MedicalRecordController medicalRecordController;
        private MedicalRecord medicalRecord;
        private Frame frame;
        public PatientTabs(MedicalRecord medicalRecord,Frame frame)
        {
            InitializeComponent();
            this.medicalRecord = medicalRecord;
            PatientMedicalRecordPage patientMedicalRecordPage = new PatientMedicalRecordPage(medicalRecord, frame);
            // this.frame = new Frame();
            FrameForPatient.Content = patientMedicalRecordPage;
        }

        private void MedcialRecordClick(object sender, RoutedEventArgs e)
        {
            PatientMedicalRecordPage patientMedicalRecordPage = new PatientMedicalRecordPage(medicalRecord, frame);
            FrameForPatient.Content = patientMedicalRecordPage;
        }

        private void MedicalHistoryClick(object sender, RoutedEventArgs e)
        {
            PatientMedicalHistory patientMedicalHistory = new PatientMedicalHistory();
            FrameForPatient.Content = patientMedicalHistory;
        }
    }
}
