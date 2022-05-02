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
using Sims_Hospital_Zdravo.Model;
using Model;


namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for AnamnesisWindow.xaml
    /// </summary>
    public partial class AnamnesisWindow : Window
    {
        private AnamnesisController anamnesisController;
        private DoctorAppointmentController doctorAppointment;
        private MedicalRecord medicalRecord;
        private Appointment appointment;
        private int doctorId;
        public AnamnesisWindow(AnamnesisController anamnesisController,DoctorAppointmentController doctorAppointmentController, MedicalRecord medicalRecord,Appointment app)
        {
            InitializeComponent();
            this.anamnesisController = anamnesisController;
            this.medicalRecord = medicalRecord;
            this.appointment = app;
            this.doctorAppointment = doctorAppointmentController;
            PatientTxt.Text = medicalRecord._Patient._Name + medicalRecord._Patient._Surname;
            MedicalRecordTxt.Text = medicalRecord._Id.ToString();
            ExaminatonTxt.Text = app._Time.Start.ToString();
            DoctorTxt.Text = app._Doctor._Name + app._Doctor._Surname;
        }

        private void SaveAnamnesis_Click(object sender, RoutedEventArgs e)
        {
            string diagnosis = DiagnosisTxt.Text;
            string medical_report = AnamnesisTxt.Text;
            Doctor doctor = appointment._Doctor;
            doctorId = doctor._Id;
            TimeInterval timeInterval = appointment._Time;
            DateTime date = appointment._Time.Start;
            Patient patient = medicalRecord._Patient;
            Anamnesis anamnesis = new Anamnesis(doctor, medicalRecord, date, timeInterval, diagnosis, medical_report);
            anamnesisController.Create(anamnesis);
            doctorAppointment.DeleteAfterExaminationIsDone(date, doctorId, patient);
            Close();
            

            //anamnesisController.Create
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
