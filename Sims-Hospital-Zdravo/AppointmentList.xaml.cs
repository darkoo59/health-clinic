using Controller;
using Model;
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
    /// Interaction logic for AppointmentList.xaml
    /// </summary>
    public partial class AppointmentList : Window
    {
        public AppointmentPatientController appointmentPatientController;
        public AppointmentList(AppointmentPatientController appointmentPatientController, Appointment appointment)
        {
            InitializeComponent();
            this.appointmentPatientController = appointmentPatientController;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
