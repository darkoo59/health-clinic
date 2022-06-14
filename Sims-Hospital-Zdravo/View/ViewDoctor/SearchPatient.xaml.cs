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
using Sims_Hospital_Zdravo.ViewModel;

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
        private int doctorID;
        private bool _isToolTipVisible;
        public SearchPatient(AnamnesisController anamnesisController,DoctorAppointmentController doctorAppointmentController,MedicalRecordController medicalRecordController,Frame frame,int id,bool tooltip)
        {
            InitializeComponent();
            this._isToolTipVisible = tooltip;
            this.DataContext = new SearchPatientViewModel(_isToolTipVisible);
            this.frame = frame;
            this.doctorID = id;
            this.anamnesisController = new AnamnesisController();
            this.doctorAppointmentController = doctorAppointmentController;
            this.medicalRecordController = medicalRecordController;
            //this._isToolTipVisible = tooltip;
            
            
            
        }

        public bool IsToolTipVisible
        {
            get
            {
                return _isToolTipVisible;
            }
            set
            {
                _isToolTipVisible = value;
            }
        }

        private void PatientMedicalRecordClick(object sender, RoutedEventArgs e)
        {
            
            MedicalRecord medicalRecord = MedicalRecordDataGrid.SelectedValue as MedicalRecord;
            int medicalRecordId = medicalRecord.Id;
            if (medicalRecord != null)
            {
                PatientMedicalRecordPage patientMedicalRecordPage = new PatientMedicalRecordPage(medicalRecord,frame,doctorID);
                frame.Content = patientMedicalRecordPage;
            }
            else
            {
                MessageBox.Show("Chose whose medical record you want to see.");
            }
        }

        private void PrescriptionClick(object sender, RoutedEventArgs e)
        {
            MedicalRecord medicalRecord = MedicalRecordDataGrid.SelectedValue as MedicalRecord;
            
        }

        private void MedicalRecordDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MedicalRecord medicalRecord = MedicalRecordDataGrid.SelectedValue as MedicalRecord;
            if (medicalRecord != null)
            {
                PatientMedicalRecordPage patientMedicalRecordPage = new PatientMedicalRecordPage(medicalRecord, frame, doctorID);
                frame.Content = patientMedicalRecordPage;
            }
        
            else
            {
                MessageBox.Show("Chose whose medical record you want to see.");
            }
        }
    }
}
