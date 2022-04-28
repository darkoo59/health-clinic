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

namespace Sims_Hospital_Zdravo.View.View.Secretary
{
    /// <summary>
    /// Interaction logic for AddAllergensWindow.xaml
    /// </summary>
    public partial class AddAllergensWindow : Window
    {
        private ListBox AllergensListParrent;
        private List<String> NotAllergic;

        public AddAllergensWindow(ListBox allergensList,List<String> notAllergic)
        {
            InitializeComponent();
            AllergensListParrent = allergensList;
            NotAllergic = notAllergic;
            AllergensList.ItemsSource = notAllergic;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            foreach(String str in AllergensList.SelectedItems)
            {
                AllergensListParrent.Items.Add(str);
            }
            this.Close();
        }
    }
}
