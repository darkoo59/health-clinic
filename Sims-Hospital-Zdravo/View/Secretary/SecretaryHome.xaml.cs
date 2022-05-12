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
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for SecretaryHome.xaml
    /// </summary>
    public partial class SecretaryHome : Window
    {
        private MedicalRecordController medicalController;
        private SecretaryAppointmentController appointmentController;
        private App app;

        public SecretaryHome()
        {
            app = Application.Current as App;
            InitializeComponent();
            this.medicalController = app._recordController;
            this.appointmentController = app._secretaryAppointmentController;
        }

        private void MedicalRecordsClick(object sender, RoutedEventArgs e)
        {
            SecretaryWindow secretaryWindow = new SecretaryWindow(medicalController);
            secretaryWindow.Show();
        }

        private void Examination_Click(object sender, RoutedEventArgs e)
        {
            ExaminationWindow examinationWindow = new ExaminationWindow(appointmentController);
            examinationWindow.Show();
        }
    }
}