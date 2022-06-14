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
using Sims_Hospital_Zdravo.ViewModel;


namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for LabaratoryResultsPage.xaml
    /// </summary>
    public partial class LabaratoryResultsPage : Page
    {
        public LabaratoryResultsPage()
        {
            InitializeComponent();
            this.DataContext = new LabaratoryTestViewModel();
        }

        private void NewTestClick(object sender, RoutedEventArgs e)
        {
            LabaratoryTestForm labaratoryTestForm = new LabaratoryTestForm();
        }
    }
}
