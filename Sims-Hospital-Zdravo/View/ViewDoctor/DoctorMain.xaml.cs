using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
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
        private App app;
        public DoctorMain()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorCRUDWindow CrudWindow = new DoctorCRUDWindow();
            CrudWindow.Show();
        }
        private void Button_Click_Appointment(object sender, RoutedEventArgs e)
        {
            MyAppointments myAppointments = new MyAppointments();
            myAppointments.Show();

        }
    }
}
