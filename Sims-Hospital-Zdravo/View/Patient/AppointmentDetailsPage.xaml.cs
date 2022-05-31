using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for AppointmentDetailsPage.xaml
    /// </summary>
    public partial class AppointmentDetailsPage : Page
    {
        Appointment appointment;
        App app;
        MedicalRecordController medicalRecordController;
        AnamnesisController anamnesisContorller;
        Anamnesis anamnesis;
        public AppointmentDetailsPage(Appointment appointment)
        {
            this.appointment = appointment;
            app = Application.Current as App;
            InitializeComponent();
            medicalRecordController = app._recordController;
            anamnesisContorller = app._anamnesisController;
            anamnesis = medicalRecordController.GetAnamnesis(appointment);
            Appointment.Content = new AnamnesisPage(anamnesis);
        }

        private void Anamnesis_Click(object sender, RoutedEventArgs e)
        {
            Appointment.Content = new AnamnesisPage(anamnesis);
        }

        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
            Appointment.Content = new Prescriptions(medicalRecordController.GetPrescriptions(appointment), appointment);
        }

        private void Diagnosis_Click(object sender, RoutedEventArgs e)
        {
            Appointment.Content = new DiagnosisPage(appointment, anamnesis);
        }
    }
}
