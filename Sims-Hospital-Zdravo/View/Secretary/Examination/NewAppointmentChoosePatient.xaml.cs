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

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for NewAppointmentChoosePatient.xaml
    /// </summary>
    public partial class NewAppointmentChoosePatient : Window
    {
        private TimeInterval selectedDate;
        private App app;
        public NewAppointmentChoosePatient(TimeInterval selectedDate)
        {
            InitializeComponent();
            this.selectedDate = selectedDate;
            app = Application.Current as App;
            ListPatients.ItemsSource = app._secretaryAppointmentController.FindAvailablePatientsForInterval(selectedDate);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)ListPatients.SelectedItem;
            UpdateChooseRoomWindow chooseRoom = new UpdateChooseRoomWindow(patient, selectedDate);
            chooseRoom.Show();
            this.Close();
        }
    }
}
