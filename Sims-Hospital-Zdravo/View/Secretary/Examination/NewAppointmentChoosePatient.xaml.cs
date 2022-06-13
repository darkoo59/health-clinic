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
    /// Interaction logic for NewAppointmentChoosePatient.xaml
    /// </summary>
    public partial class NewAppointmentChoosePatient : Window
    {
        private TimeInterval selectedDate;
        private SecretaryAppointmentController _secretaryAppointmentController;
        public NewAppointmentChoosePatient(TimeInterval selectedDate)
        {
            InitializeComponent();
            this.selectedDate = selectedDate;
            _secretaryAppointmentController = new SecretaryAppointmentController();
            ListPatients.ItemsSource = _secretaryAppointmentController.FindAvailablePatientsForInterval(selectedDate);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListPatients.SelectedItem != null)
                {
                    Patient patient = (Patient)ListPatients.SelectedItem;
                    UpdateChooseRoomWindow chooseRoom = new UpdateChooseRoomWindow(patient, selectedDate);
                    chooseRoom.Show();
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
