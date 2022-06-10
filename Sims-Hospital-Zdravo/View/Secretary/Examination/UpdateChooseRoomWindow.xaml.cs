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
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for UpdateChooseRoomWindow.xaml
    /// </summary>
    public partial class UpdateChooseRoomWindow : Window
    {
        private Patient selectedPatient;
        private TimeInterval selectedTimeInterval;
        private SecretaryAppointmentController _secretaryAppointmentController;
        public UpdateChooseRoomWindow(Patient patient, TimeInterval startEndDate)
        {
            InitializeComponent();
            this.selectedPatient = patient;
            this.selectedTimeInterval = startEndDate;
            _secretaryAppointmentController = new SecretaryAppointmentController();
            ListRooms.ItemsSource = _secretaryAppointmentController.FindAvailableRoomsForInterval(startEndDate);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListRooms.SelectedItem != null)
                {
                    Room selectedRoom = (Room)ListRooms.SelectedItem;
                    UpdateChooseDoctorWindow chooseDoctorWindow =
                        new UpdateChooseDoctorWindow(selectedPatient, selectedTimeInterval, selectedRoom);
                    chooseDoctorWindow.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Room isn't selected", "Please select room!");
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
