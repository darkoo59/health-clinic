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
        public EditRequestWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EditRequestWindow_Loaded);
            //this._freeDaysRequest = request;
            _requestControler = new RequestForFreeDaysController();
            _notificationController = new NotificationController();
            comboStatus.ItemsSource = Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>();
        }

        void EditRequestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Check DataContext Property here - Value is not null
            _freeDaysRequest = (FreeDaysRequest)DataContext;
            comboStatus.SelectedItem = _freeDaysRequest.Status;
            txtReason.Text = _freeDaysRequest.ReasonForFreeDays;
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
                    FreeDaysRequest request = new FreeDaysRequest(_freeDaysRequest.TimeInterval, _freeDaysRequest.Doctor, _freeDaysRequest.ReasonForFreeDays,
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
    }
}
