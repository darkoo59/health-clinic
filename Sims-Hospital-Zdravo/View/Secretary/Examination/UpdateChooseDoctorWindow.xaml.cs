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

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_profile.Visibility = Visibility.Collapsed;
                tt_about.Visibility = Visibility.Collapsed;
                tt_meetings.Visibility = Visibility.Collapsed;
                tt_accounts.Visibility = Visibility.Collapsed;
                tt_equipment.Visibility = Visibility.Collapsed;
                tt_appointments.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_medical_records.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_sign_out.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_profile.Visibility = Visibility.Visible;
                tt_about.Visibility = Visibility.Visible;
                tt_meetings.Visibility = Visibility.Visible;
                tt_accounts.Visibility = Visibility.Visible;
                tt_equipment.Visibility = Visibility.Visible;
                tt_appointments.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_medical_records.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_sign_out.Visibility = Visibility.Visible;
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

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryHome window = new SecretaryHome();
            window.Show();
            this.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            ExaminationWindow window = new ExaminationWindow(app._secretaryAppointmentController);
            window.Show();
            this.Close();
        }
    }
}
