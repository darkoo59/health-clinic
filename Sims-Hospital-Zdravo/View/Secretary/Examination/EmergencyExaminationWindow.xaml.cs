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
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for EmergencyExaminationWindow.xaml
    /// </summary>
    public partial class EmergencyExaminationWindow : Window
    {
        private App app;
        private SecretaryAppointmentController _secretaryAppointmentController;
        public EmergencyExaminationWindow()
        {
            app = Application.Current as App;
            _secretaryAppointmentController = new SecretaryAppointmentController();
            InitializeComponent();
            ListPatients.ItemsSource = _secretaryAppointmentController.ReadAllPatients();
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)  //TODO
        {
            Patient patient = new Patient("Guest", "Guest");
            ChooseDoctorSpecialityWindow window = new ChooseDoctorSpecialityWindow(patient);
            window.Show();
            this.Close();
        }


        private void Next_Click(object sender, MouseButtonEventArgs e)  //TODO
        {
            try
            {
                if (ListPatients.SelectedItem != null)
                {
                    Patient patient = (Patient)ListPatients.SelectedItem;
                    ChooseDoctorSpecialityWindow window = new ChooseDoctorSpecialityWindow(patient);
                    window.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Patient isn't selected", "Please select patient");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
