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
using Controller;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for EditAnamnesis.xaml
    /// </summary>
    public partial class EditAnamnesis : Window
    {
        private AnamnesisController anamnesisController;
        private DoctorAppointmentController doctorAppointmentController;
        
        private PatientMedicalRecordController patientMedicalRecordController;
        private int DoctorId;
        private Anamnesis anamnesis;
        public EditAnamnesis( Anamnesis anamnesis,   AnamnesisController anamnesisController,DoctorAppointmentController doctorAppointmentController)
        {
            InitializeComponent();
            this.anamnesisController = anamnesisController;
            this.anamnesis = anamnesis;
            this.doctorAppointmentController = doctorAppointmentController;
            PatientTxt.Text = anamnesis._MedicalRecord._Patient._Name + anamnesis._MedicalRecord._Patient._Surname;
            MedicalRecordTxt.Text = anamnesis._MedicalRecord._Id.ToString();
            DoctorTxt.Text = anamnesis._Doctor._Name + anamnesis._Doctor._Surname;
            Editanam.Text = DateTime.Now.ToString();
            DiagnosisTxt.Text = anamnesis._Diagnosis;
            AnamnesisTxt.Text = anamnesis._Anamensis;
            DoctorId = anamnesis._Doctor._Id;
            DateTime date = anamnesis._Date;
            Patient pat = anamnesis._MedicalRecord._Patient;
            ExaminatonTxt.Text = (doctorAppointmentController.doctorAppointmentService.FindAppointmentByDateAndPatient(date, pat, DoctorId))._Time.Start.ToString();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Parse(Editanam.Text);
            string diagnosis = DiagnosisTxt.Text;
            string medical_report = AnamnesisTxt.Text;
            Doctor doctor = anamnesis._Doctor;
            MedicalRecord med = anamnesis._MedicalRecord;
            //TimeInterval tl = 
            Anamnesis anam = new Anamnesis(doctor, med, date, null, diagnosis, medical_report);
            anamnesisController.Update(anam);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
