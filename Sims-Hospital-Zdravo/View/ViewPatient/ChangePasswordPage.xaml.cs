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

namespace Sims_Hospital_Zdravo.View.ViewPatient
{
    /// <summary>
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        public Frame frame;
        public ChangePasswordPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new ProfilePage(frame);
        }
    }
}
