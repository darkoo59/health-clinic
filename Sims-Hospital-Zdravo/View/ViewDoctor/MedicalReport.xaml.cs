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
using Sims_Hospital_Zdravo.Model;
using Model;
using Controller;
using System.Collections.ObjectModel;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for MedicalReport.xaml
    /// </summary>
    public partial class MedicalReport : Page
    {
        private AnamnesisController anamnesisController;
        private DoctorAppointmentController doctorAppointmentController;
        private MedicalRecord medicalRecord;
        private PatientMedicalRecordController patientMedicalRecordController;
        private int DoctorId;
        private App app;
        private DateTime date;
        private Frame frame;
        public MedicalReport(MedicalRecord medicalRecord,int id,Frame frame)
        {
            InitializeComponent();
            this.DoctorId = id;
            this.anamnesisController = new AnamnesisController();
            this.frame = frame;
            this.medicalRecord = medicalRecord;
            this.doctorAppointmentController = app._doctorAppointmentController;
            this.patientMedicalRecordController = new PatientMedicalRecordController();
            SelectedPatient(medicalRecord);


        }
        private Patient pat;

        public Patient Pat
        {
            get { return pat; }
            set { pat = value; }
        }
        private void SelectedPatient(MedicalRecord medicalRecord)
        {
            PatTxt.Text = medicalRecord.Patient.Name +" " + medicalRecord.Patient.Surname;
            
        }
       
        private void dateExamination_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
             date = DateTime.Parse(dateExamination.Text);
        }
        

        private void SaveMedicalReporClick(object sender, RoutedEventArgs e)
        {
            string diagnosis = DiagnosisTxt.Text;
            string medical_report = AnamnesisTxt.Text;
            Doctor doctor = doctorAppointmentController.GetDoctor(DoctorId);
            //MedicalRecord med = patientMedicalRecordController.findMedicalRecordByPatient(Pat);
            Anamnesis anamnesis = new Anamnesis(doctor,medicalRecord.Patient, date, null, diagnosis, medical_report);

            anamnesisController.Create(anamnesis);
            medicalRecord.Anamnesis.Add(anamnesis);
            
        }
    }
}
