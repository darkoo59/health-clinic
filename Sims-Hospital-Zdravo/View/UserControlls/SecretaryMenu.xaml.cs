using Sims_Hospital_Zdravo.View.Login;
using Sims_Hospital_Zdravo.View.Secretary.About;
using Sims_Hospital_Zdravo.View.Secretary.Contact;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
using Sims_Hospital_Zdravo.View.Secretary.FreeDays;
using Sims_Hospital_Zdravo.View.Secretary.Meetings;
using Sims_Hospital_Zdravo.View.Secretary.Profile;
using Sims_Hospital_Zdravo.View.Secretary.Settings;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;
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

namespace Sims_Hospital_Zdravo.View.UserControlls
{
    /// <summary>
    /// Interaction logic for SecretaryMenu.xaml
    /// </summary>
    public partial class SecretaryMenu : UserControl
    {
        private App app;
        public SecretaryMenu()
        {
            app = Application.Current as App;
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_profile.Visibility = Visibility.Collapsed;
                tt_about.Visibility = Visibility.Collapsed;
                tt_meetings.Visibility = Visibility.Collapsed;
                tt_free_days.Visibility = Visibility.Collapsed;
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
                tt_free_days.Visibility = Visibility.Visible;
                tt_equipment.Visibility = Visibility.Visible;
                tt_appointments.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_medical_records.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_sign_out.Visibility = Visibility.Visible;
            }
        }

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            SecretaryHome window = new SecretaryHome();
            window.Show();
            myWindow.Close();
        }

        private void MedicalRecord_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);        
            SecretaryWindow window = new SecretaryWindow();
            window.Show();
            myWindow.Close();
        }

        private void Meetings_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);        
            CreateNewMeeting window = new CreateNewMeeting();
            window.Show();
            myWindow.Close();
        }

        private void FreeDays_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            FreeDaysWindow window = new FreeDaysWindow();
            window.Show();
            myWindow.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            ExaminationWindow window = new ExaminationWindow();
            window.Show();
            myWindow.Close();
        }

        private void Equipment_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            SuppliesHome window = new SuppliesHome();
            window.Show();
            myWindow.Close();
        }

        private void Profile_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            SecretaryProfileWindow window = new SecretaryProfileWindow();
            window.Show();
            myWindow.Close();
        }

        private void SignOut_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            app._accountController.Logout();
            LoginMainWindow window = new LoginMainWindow();
            window.Show();
            myWindow.Close();
        }

        private void About_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            AboutWindow window = new AboutWindow();
            window.Show();
            myWindow.Close();
        }

        private void Contact_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            ContactWindow window = new ContactWindow();
            window.Show();
            myWindow.Close();
        }

        private void Settings_Click(object sender, MouseButtonEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            SettingsWindow window = new SettingsWindow();
            window.Show();
            myWindow.Close();
        }
    }
}
