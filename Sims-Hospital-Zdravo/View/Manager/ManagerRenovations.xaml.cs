using System;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerRenovations : Page
    {
        private RenovationController renovationController;
        private App app;
        private Frame ManagerContent;

        public ManagerRenovations()
        {
            app = Application.Current as App;
            this.renovationController = app._renovationController;
            InitializeComponent();

            RenovationsTable.ItemsSource = renovationController.ReadAll();
            RetrieveMainFrame();
        }

        private void BasicRenovation_Click(object sender, RoutedEventArgs e)
        {
            ManagerContent.Source = new Uri("ManagerRenovationBasic.xaml", UriKind.Relative);
        }


        private void RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    ManagerContent = ((ManagerMainWindow)win).ManagerContent;
                }
            }
        }

        private void AdvancedRenovation_Click(object sender, RoutedEventArgs e)
        {
            ManagerContent.Source = new Uri("ManagerRenovationAdvanced.xaml", UriKind.Relative);
        }
    }
}