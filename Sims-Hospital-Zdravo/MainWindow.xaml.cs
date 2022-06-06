using Controller;
using Sims_Hospital_Zdravo.View;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        App app;

        public MainWindow()
        {
            InitializeComponent();
            app = Application.Current as App;
        }

        private void Patient_Click(object sender, RoutedEventArgs e)
        {
            AppointmentPatientController appointmentPatientController = app._appointmentPatientController;
        }

        private void Secretary_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecordController medicalController = app._recordController;
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Doctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentController doctorAppController = app._doctorAppointmentController;
            RoomController roomControl = new RoomController();
        }
    }
}