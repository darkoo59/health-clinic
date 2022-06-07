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
using Notifications.Wpf;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View;
using Sims_Hospital_Zdravo.View.Login;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for DoctorMain.xaml
    /// </summary>
    public partial class DoctorMain : Window, INotificationObserver
    {
        private DoctorAppointmentController docController;
        private MedicalRecordController medicalRecordController;
        private AnamnesisController anamnesisController;
        private PatientMedicalRecordController patientMedicalRecordController;
        private RequestForFreeDaysController requestForFreeDaysController;
        private DoctorAppointmentController doctorAppointmentController;
        private NotificationController notificationController;
        private NotificationManager notificationManager;
        private AccountController accountController;
        private Frame frame;

        private int doctorId;

        private App app;

        public DoctorMain(int DoctorId)
        {
            app = App.Current as App;
            InitializeComponent();
            this.doctorId = DoctorId;
            this.docController = app._doctorAppointmentController;
            this.accountController = app._accountController;
            this.medicalRecordController = app._recordController;
            this.anamnesisController = app._anamnesisController;
            this.patientMedicalRecordController = app._patientMedRecController;
            this.requestForFreeDaysController = app._requestForFreeDaysController;
            this.doctorAppointmentController = app._doctorAppointmentController;
            this.notificationController = this.app._notificationController;
            notificationManager = new NotificationManager();
            this.app._taskScheduleTimer.AddObserver(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorCRUDWindow CrudWindow = new DoctorCRUDWindow(docController);
            //CrudWindow.Show();
        }

        private void Button_Click_Appointment(object sender, RoutedEventArgs e)
        {
            MyAppointments myAppointments = new MyAppointments(docController, anamnesisController, medicalRecordController, doctorId);
            myAppointments.Show();
        }

        private void button_medical_report_click(object sender, RoutedEventArgs e)
        {
            //MedicalReport medicalReportWindow = new MedicalReport(anamnesisController, docController, patientMedicalRecordController, doctorId);
            //medicalReportWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //AnamnesisList anamnesisList = new AnamnesisList( doctorId, medicalRecord);
            //anamnesisList.Show();
        }

        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)


        {
            SearchPatient searchPatient = new SearchPatient(anamnesisController, docController, medicalRecordController, FrameForMain,doctorId);
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
            RequestForFreeDaysForm requestForFreeDaysForm = new RequestForFreeDaysForm(doctorAppointmentController, requestForFreeDaysController, doctorId);
            FrameForMain.Content = requestForFreeDaysForm;
        }

        private void MedcinesClick(object sender, RoutedEventArgs e)
        {
            DoctorMedicines doctorMedicines = new DoctorMedicines(FrameForMain);
            FrameForMain.Content = doctorMedicines;
        }

        public void Notify(Notification notification)
        {
            if (notification as MedicineCreatedNotification != null)
            {
                MedicineCreatedNotification medicineCreatedNotification = notification as MedicineCreatedNotification;
                notificationManager.Show(
               new NotificationContent { Title = "Medicine notification", Message = "Medicine " + medicineCreatedNotification.Medicine.Name + " is waiting for approval!" },
               areaName: "DoctorWindowArea", expirationTime: TimeSpan.FromSeconds(30));

                notificationController.Delete(notification);
            }
            else if (notification as MeetingCreatedNotifications != null)
            {
                MeetingCreatedNotifications meetingNotification = notification as MeetingCreatedNotifications;
                notificationManager.Show(
                new NotificationContent { Title = "Meeting notification", Message = "You have new meeting at " + meetingNotification.MeetingStart.ToString() },
                areaName: "DoctorWindowArea", expirationTime: TimeSpan.FromSeconds(30));
                notificationController.Delete(notification);
            }
            else if (notification as FreeDaysNotification != null)
            {
                FreeDaysNotification freeDaysNotification = notification as FreeDaysNotification;
                notificationManager.Show(
                    new NotificationContent { Title = "Free days notification", Message = freeDaysNotification.Explanation },
                    areaName: "DoctorWindowArea", expirationTime: TimeSpan.FromSeconds(30));
                notificationController.Delete(notification);
            }
        }

        private void AppointmentsClick(object sender, RoutedEventArgs e)
        {
            DoctorCRUDWindow doctorCRUDWindow = new DoctorCRUDWindow(docController);
            FrameForMain.Content = doctorCRUDWindow;
        }

        private void AnamnesisClick(object sender, RoutedEventArgs e)
        {
          // MedicalReport medicalReport = new MedicalReport( doctorId,FrameForMain);
            //FrameForMain.Content = medicalReport;
        }


        public void Logout()
        {
            accountController.Logout();
            LoginMainWindow loginMainWindow = new LoginMainWindow();
            loginMainWindow.Show();
            Close();
        }

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            Logout();


        }

        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
            //ViewRequestForFreeDays viewRequestForFreeDays = new ViewRequestForFreeDays(doctorId);
            //FrameForMain.Content = viewRequestForFreeDays;
        }
    }
}