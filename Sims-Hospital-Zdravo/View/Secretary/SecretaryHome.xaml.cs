using Controller;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
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
using Sims_Hospital_Zdravo.View.Secretary.Meetings;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Notifications.Wpf;
using Sims_Hospital_Zdravo.View.Secretary.FreeDays;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for SecretaryHome.xaml
    /// </summary>
    public partial class SecretaryHome : Window
    {
        private MedicalRecordController _medicalRecordController;
        private SecretaryAppointmentController _secretaryAppointmentController;
        private App app;

        public SecretaryHome()
        {
            InitializeComponent();
            app = Application.Current as App;
            this._medicalRecordController = new MedicalRecordController();
            this._secretaryAppointmentController = new SecretaryAppointmentController();
            lblName.Content = app._accountController.GetLoggedAccount().Name + " " + app._accountController.GetLoggedAccount().Surname;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if(TgButton.IsChecked == true)
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

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void MedicalRecord_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryWindow window = new SecretaryWindow();
            window.Show();
            this.Close();
        }

        private void Meetings_Click(object sender, MouseButtonEventArgs e)
        {
            CreateNewMeeting window = new CreateNewMeeting();
            window.Show();
            this.Close();
        }

        private void FreeDays_Click(object sender, MouseButtonEventArgs e)
        {
            FreeDaysWindow window = new FreeDaysWindow();
            window.Show();
            this.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            ExaminationWindow window = new ExaminationWindow();
            window.Show();
            this.Close();
        }

        private void Equipment_Click(object sender, MouseButtonEventArgs e)
        {
            SuppliesHome window = new SuppliesHome();
            window.Show();
            this.Close();
        }
    }
}