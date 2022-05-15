using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using Sims_Hospital_Zdravo.Controller;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for DoctorMain.xaml
    /// </summary>
    public partial class DoctorMain : Window
    {
        private DoctorAppointmentController docController;
        private MedicalRecordController medicalRecordController;
        private AnamnesisController anamnesisController;
        private PatientMedicalRecordController patientMedicalRecordController;
        
        private int doctorId;
        
        private App app;
        public DoctorMain(int DoctorId)
        {
            app = App.Current as App;
            InitializeComponent();
            this.doctorId = DoctorId;
            this.docController = app._doctorAppointmentController;
            this.medicalRecordController = app._recordController;
            this.anamnesisController = app._anamnesisController;
            this.patientMedicalRecordController = app._patientMedRecController;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorCRUDWindow CrudWindow = new DoctorCRUDWindow(docController);
            CrudWindow.Show();
        }
        private void Button_Click_Appointment(object sender, RoutedEventArgs e)
        {
            MyAppointments myAppointments = new MyAppointments(docController, anamnesisController,medicalRecordController,doctorId);
            myAppointments.Show();

        }

        private void button_medical_report_click(object sender, RoutedEventArgs e)
        {
            MedicalReport medicalReportWindow = new MedicalReport(anamnesisController, docController, patientMedicalRecordController, doctorId);
            medicalReportWindow.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AnamnesisList anamnesisList = new AnamnesisList(anamnesisController,doctorId,docController);
            anamnesisList.Show();
        }

        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)


        {
            SearchPatient searchPatient = new SearchPatient(anamnesisController, docController, medicalRecordController,FrameForMain);
            FrameForMain.Content = searchPatient;
        }

        private void MyAppointmentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MyAppointmentPage appointmentPage = new MyAppointmentPage(docController, anamnesisController, medicalRecordController, doctorId);
            FrameForMain.Content = appointmentPage;
        }

        private void schedule_Appointment_Click(object sender, RoutedEventArgs e)
        {
            ScheduleAppointmentSpecialist scheduleAppointmentSpecialist = new ScheduleAppointmentSpecialist(docController);
            FrameForMain.Content = scheduleAppointmentSpecialist;
        }

        private void operation_click(object sender, RoutedEventArgs e)
        {
            OperationForm operationform = new OperationForm(docController);
            FrameForMain.Content = operationform;
        }

        private void DaysOff_Click(object sender, RoutedEventArgs e)
        {
            RequestForFreeDaysForm requestForFreeDaysForm = new RequestForFreeDaysForm();
            FrameForMain.Content = requestForFreeDaysForm;
        }
    }
}
