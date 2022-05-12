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
            Patient.Content = new HomePatient();
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Patient.Content = new HomePatient();
        }

        private void Appointment_Click(object sender, RoutedEventArgs e)
        {
            Patient.Content = new PatientWindow(Patient);
        }

        private void Therapy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
