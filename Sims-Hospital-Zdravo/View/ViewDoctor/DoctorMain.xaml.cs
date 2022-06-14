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
        private bool tooltip;
        private int doctorId;
        
        private App app;

        public DoctorMain(int DoctorId)
        {
            app = App.Current as App;
            InitializeComponent();
            this.doctorId = DoctorId;
            this.docController = new DoctorAppointmentController();
            this.accountController = app._accountController;
            this.medicalRecordController = new MedicalRecordController();
            this.anamnesisController = new AnamnesisController ();
            this.patientMedicalRecordController = new PatientMedicalRecordController();
            this.requestForFreeDaysController = new RequestForFreeDaysController();
            this.doctorAppointmentController = new DoctorAppointmentController();
            this.notificationController = new NotificationController();
            notificationManager = new NotificationManager();
            this.app._taskScheduleTimer.AddObserver(this);
            MyAppointmentPage myAppointmentPage = new MyAppointmentPage(anamnesisController, DoctorId,FrameForMain);
            FrameForMain.Content = myAppointmentPage;
            this.tooltip = myAppointmentPage.IsToolTipVisible;
            WizardWindow wizardWindow = new WizardWindow();
            wizardWindow.Show();

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DoctorCRUDWindow CrudWindow = new DoctorCRUDWindow(docController);
            //CrudWindow.Show();
        }

        private void Button_Click_Appointment(object sender, RoutedEventArgs e)
        {
            MyAppointmentPage myAppointments = new MyAppointmentPage(anamnesisController, doctorId,FrameForMain);
            FrameForMain.Content = myAppointments;
            
        }

        

        

        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)


        {
            SearchPatient searchPatient = new SearchPatient(anamnesisController, docController, medicalRecordController, FrameForMain, doctorId,tooltip);
            FrameForMain.Content = searchPatient;
        }

        //private void MyAppointmentMenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    MyAppointmentPage appointmentPage = new MyAppointmentPage( anamnesisController,  doctorId);
        //    FrameForMain.Content = appointmentPage;
        //}

        private void schedule_Appointment_Click(object sender, RoutedEventArgs e)
        {
            ScheduleAppointmentSpecialist scheduleAppointmentSpecialist = new ScheduleAppointmentSpecialist(docController);
            FrameForMain.Content = scheduleAppointmentSpecialist;
        }

        private void operation_click(object sender, RoutedEventArgs e)
        {
            OperationForm operationform = new OperationForm();
            //FrameForMain.Content = operationform;
            OperationSchedule operationSchedule = new OperationSchedule(docController, FrameForMain);
            FrameForMain.Content = operationSchedule;
        }

        private void DaysOff_Click(object sender, RoutedEventArgs e)
        {
            ViewRequestForFreeDays requestForFreeDaysForm = new ViewRequestForFreeDays(doctorId);
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
            //DoctorCRUDWindow doctorCRUDWindow = new DoctorCRUDWindow(docController);
            //FrameForMain.Content = doctorCRUDWindow;
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

        private void ViewRequestsClick(object sender, RoutedEventArgs e)
        {
            ViewRequestForFreeDays viewRequestForFreeDays = new ViewRequestForFreeDays(doctorId);
            FrameForMain.Content = viewRequestForFreeDays;
        }

        private void MedicalEquipmentClick(object sender, RoutedEventArgs e)
        {
            MedicalEqupiment medicalEqupiment = new MedicalEqupiment();
            FrameForMain.Content = medicalEqupiment;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.A)
            {
                MyAppointmentPage appointmentPage = new MyAppointmentPage(anamnesisController, doctorId, FrameForMain);
                FrameForMain.Content = appointmentPage;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.O)
            {
                OperationSchedule operationSchedule = new OperationSchedule(doctorAppointmentController, FrameForMain);
                FrameForMain.Content =operationSchedule;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.H)
            {
                HelpPage helpPage = new HelpPage();
                FrameForMain.Content = helpPage;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.M)
            {
                DoctorMedicines doctorMedicines = new DoctorMedicines(FrameForMain);
                FrameForMain.Content = doctorMedicines;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.E)
            {
                
            
                    MedicalEqupiment medicalEqupiment = new MedicalEqupiment();
                    FrameForMain.Content = medicalEqupiment;
                }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.R)
            {


                ViewRequestForFreeDays viewRequestForFreeDays = new ViewRequestForFreeDays(doctorId);
                FrameForMain.Content = viewRequestForFreeDays;
            }


        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            HelpPage helpPage = new HelpPage();
            FrameForMain.Content = helpPage;
        }
    }
}