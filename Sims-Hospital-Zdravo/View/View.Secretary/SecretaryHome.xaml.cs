using Controller;
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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for SecretaryHome.xaml
    /// </summary>
    public partial class SecretaryHome : Window
    {
        private MedicalRecordController medicalController;
        public SecretaryHome(MedicalRecordController controller)
        {
            InitializeComponent();
            this.medicalController = controller;
        }

        private void MedicalRecordsClick(object sender, RoutedEventArgs e)
        {
            SecretaryWindow secretaryWindow = new SecretaryWindow(medicalController);
            secretaryWindow.Show();
        }

    }
}
