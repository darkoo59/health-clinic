using Model;
using Sims_Hospital_Zdravo.View.Login;
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
    public partial class PatientDashboard : Window
    {
        public PatientDashboard()
        {
            InitializeComponent();
            var converter = new System.Windows.Media.BrushConverter();
            Home.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Home.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Patient.Content = new HomePatient(Patient);
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            Home.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Home.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Appointment.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Appointment.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Patient.Content = new HomePatient(Patient);
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            Home.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Home.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Appointment.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Appointment.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Patient.Content = new PatientWindow(Patient);
        }

        private void Therapy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Log_Out_Click(object sender, RoutedEventArgs e)
        {
            LoginMainWindow login = new LoginMainWindow();
            login.Show();
            Close();
        }
    }
}
