using Model;
using Sims_Hospital_Zdravo.Interfaces;
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
    /// Interaction logic for UpdateChooseDoctorWindow.xaml
    /// </summary>
    public partial class UpdateChooseDoctorWindow : Window
    {
        private Patient selectedPatient;
        private TimeInterval selectedTime;
        private Room selectedRoom;
        private App app;
        public UpdateChooseDoctorWindow(Patient patient, TimeInterval date, Room room)
        {
            InitializeComponent();
            this.DataContext = this;
            this.app = Application.Current as App;
            this.selectedPatient = patient;
            this.selectedTime = date;
            this.selectedRoom = room;
            ListDoctors.ItemsSource = app._secretaryAppointmentController.FindAvailableDoctorsForInterval(selectedTime);
            comboAppointmentType.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListDoctors.SelectedItem == null)
                    MessageBox.Show("Doctor isn't selected", "Please select doctor!");
                else if (comboAppointmentType.SelectedItem == null)
                    MessageBox.Show("Appointment type isn't selected", "Please select appointment type!");
                else
                {
                    Doctor selectedDoctor = (Doctor)ListDoctors.SelectedItem;
                    Appointment appointment = new Appointment(selectedRoom, selectedDoctor, selectedPatient,
                        selectedTime, (AppointmentType)comboAppointmentType.SelectedValue);
                    appointment._Id = app._secretaryAppointmentController.GenerateId();
                    app._secretaryAppointmentController.Create(appointment);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
