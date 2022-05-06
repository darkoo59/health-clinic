using System;
using System.Windows;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
            ManagerContent.Source = new Uri("ManagerDashboard.xaml", UriKind.Relative);
        }
    }
}