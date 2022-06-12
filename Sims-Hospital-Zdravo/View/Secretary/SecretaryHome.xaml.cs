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