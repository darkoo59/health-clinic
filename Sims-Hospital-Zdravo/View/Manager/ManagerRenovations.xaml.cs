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

        public ManagerRenovations()
        {
            app = Application.Current as App;
            this.renovationController = app._renovationController;
            InitializeComponent();

            RenovationsTable.ItemsSource = renovationController.ReadAll();
        }

        private void BasicRenovation_Click(object sender, RoutedEventArgs e)
        {
            ManagerRenovationBasic basicRenovation = new ManagerRenovationBasic();
            basicRenovation.Show();
        }
    }
}