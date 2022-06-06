using Model;
using Notifications.Wpf;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Login;
using Sims_Hospital_Zdravo.View.ViewPatient;
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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for PatientDashboard.xaml
    /// </summary>
    public partial class PatientDashboard : Window, INotificationObserver
    {
        App app;
        private bool flag;
        private NotificationManager notificationManager;
        public PatientDashboard()
        {
            app = Application.Current as App;
            InitializeComponent();
            app._taskScheduleTimer.AddObserver(this);
            var converter = new System.Windows.Media.BrushConverter();
            Home.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Home.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Patient.Content = new HomePatient(Patient);
            notificationManager = new NotificationManager();
            flag = true;
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                Notifications.Content = new NotificationsPage();
                flag = false;
            } 
            else
            {
                Notifications.Content = null;
                flag = true;    
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            Home.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Home.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Appointment.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Appointment.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Therapy.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Therapy.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Profile.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Profile.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Patient.Content = new HomePatient(Patient);
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            Home.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Home.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Appointment.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Appointment.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Therapy.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Therapy.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Profile.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Profile.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Patient.Content = new PatientWindow(Patient);
        }

        private void Therapy_Click(object sender, RoutedEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            Home.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Home.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Appointment.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Appointment.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Therapy.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Therapy.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Profile.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Profile.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Patient.Content = new TherapyPage();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            Home.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Home.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Appointment.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Appointment.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Therapy.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Therapy.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Profile.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Profile.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Patient.Content = new ProfilePage(Patient);
        }

        private void Log_Out_Click(object sender, RoutedEventArgs e)
        {
            LoginMainWindow login = new LoginMainWindow();
            login.Show();
            Close();
        }
        public void Notify(Notification notification)
        {
            if (typeof(MedicineCreatedNotification).IsInstanceOfType(notification)) return;
            notificationManager.Show(
                new NotificationContent { Title = "Medicine notification", Message = notification.Content },
                areaName: "WindowArea", expirationTime: TimeSpan.FromSeconds(30));
        }
    }
}
