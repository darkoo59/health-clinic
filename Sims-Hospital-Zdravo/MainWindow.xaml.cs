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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Patient_Click(object sender, RoutedEventArgs e)
        {
            PatientWindow pw = new PatientWindow();
            pw.Show();
        }

        private void Secretary_Click(object sender, RoutedEventArgs e)
        {
            SecretaryWindow secretaryWindow = new SecretaryWindow();
            secretaryWindow.Show();
        }
    }
}
