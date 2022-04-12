using Controller;
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
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        AppointmentPatientController appointmentPatientController;
        public PatientWindow()
        {
            InitializeComponent();
            McDataGrid.ItemsSource = appointmentPatientController.FindByPatientID(1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PatientCreate pc = new PatientCreate();
            pc.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
