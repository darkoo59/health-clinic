using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
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
using System.Windows.Shapes;
using Controller;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.View.Secretary.FreeDays
{
    /// <summary>
    /// Interaction logic for EditRequestWindow.xaml
    /// </summary>
    public partial class EditRequestWindow : Window
    {
        
        private FreeDaysRequest _freeDaysRequest;
        private RequestForFreeDaysController _requestControler;
        private NotificationController _notificationController;
        public EditRequestWindow(FreeDaysRequest request)
        {
            InitializeComponent();
            this._freeDaysRequest = request;
            _requestControler = new RequestForFreeDaysController();
            _notificationController = new NotificationController();
            comboStatus.ItemsSource = Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>();
            comboStatus.SelectedItem = _freeDaysRequest.Status;
            txtReason.Text = _freeDaysRequest.ReasonForfreeDays;
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if ((RequestStatus)comboStatus.SelectedValue == _freeDaysRequest.Status)
            {
                MessageBox.Show("Please change request status first!", "Change status", MessageBoxButton.OK);
            }
            else
            {
                try
                {
                    FreeDaysRequest request = new FreeDaysRequest(_freeDaysRequest.TimeInterval, _freeDaysRequest.Doctor, _freeDaysRequest.ReasonForfreeDays,
                        (RequestStatus)comboStatus.SelectedValue);
                    Notification notification = new FreeDaysNotification(txtReason.Text, _notificationController.GenerateId(),
                        request);
                    _requestControler.UpdateRequestAndNotify(request, notification);
                    MessageBox.Show("Request succesffully updated!", "Successfully updated!", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryHome window = new SecretaryHome();
            window.Show();
            this.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            ExaminationWindow window = new ExaminationWindow();
            window.Show();
            this.Close();
        }

        private void MedicalRecord_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryWindow window = new SecretaryWindow();
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
