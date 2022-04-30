using Sims_Hospital_Zdravo.Controller;
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
    /// Interaction logic for UpdateChoosePatientWindow.xaml
    /// </summary>
    public partial class UpdateChoosePatientWindow : Window
    {
        private SecretaryAppointmentController appointmentController;
        private App app;

        public UpdateChoosePatientWindow(SecretaryAppointmentController controller)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.appointmentController = controller;
            ListPatients.ItemsSource = appointmentController.ReadAllPatients();
        }

        private void btnCreateRecord_Click(object sender, RoutedEventArgs e)
        {
            InsertRecordWindow insertRecord = new InsertRecordWindow(app.recordController);
            insertRecord.Show();
        }
        
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
