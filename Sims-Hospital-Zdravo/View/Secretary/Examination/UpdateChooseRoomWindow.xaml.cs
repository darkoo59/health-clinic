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
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for UpdateChooseRoomWindow.xaml
    /// </summary>
    public partial class UpdateChooseRoomWindow : Window
    {
        private Patient selectedPatient;
        private TimeInterval selectedTimeInterval;
        private App app;
        public UpdateChooseRoomWindow(Patient patient, TimeInterval startEndDate)
        {
            InitializeComponent();
            this.app = Application.Current as App;
            this.selectedPatient = patient;
            this.selectedTimeInterval = startEndDate;
            ListRooms.ItemsSource = app.secretaryAppointmentController.FindAvailableRoomsForInterval(startEndDate);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room selectedRoom = (Room)ListRooms.SelectedItem;
                UpdateChooseDoctorWindow chooseDoctorWindow = new UpdateChooseDoctorWindow(selectedPatient, selectedTimeInterval, selectedRoom);
                chooseDoctorWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
