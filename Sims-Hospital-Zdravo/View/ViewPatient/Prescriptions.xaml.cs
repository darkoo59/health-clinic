using Model;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Prescriptions.xaml
    /// </summary>
    public partial class Prescriptions : Page
    {
        public List<Prescription> prescriptions;
        public ObservableCollection<string> medicines;
        public Appointment appointment;
        public Prescriptions(List<Prescription> prescriptions, Appointment appointment)
        {
            this.appointment = appointment;
            this.prescriptions = prescriptions;
            InitializeComponent();
            PrescriptionList.ItemsSource = InitializeList();
            if (prescriptions.Count != 0)
            {
                Parameters.Content = new PrescriptionParameters(prescriptions.First(), appointment);
                var converter = new System.Windows.Media.BrushConverter();
                List<ListViewItem> listViewItems = (List<ListViewItem>)PrescriptionList.ItemsSource;
                foreach (ListViewItem listViewItem in listViewItems)
                {
                    listViewItem.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
                    listViewItem.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
                    break;
                }
            }
        }
        public ObservableCollection<string> InitializeList() 
        {
            medicines = new ObservableCollection<string>();
            foreach (Prescription prescription in prescriptions)
            {
                medicines.Add(prescription.Medicine.Name);
            }
            return medicines;
        }

        private void PrescriptionList_Selected(object sender, RoutedEventArgs e)
        {
            int index = medicines.IndexOf((string)PrescriptionList.SelectedItem);
            Parameters.Content = new PrescriptionParameters(prescriptions.ElementAt(index), appointment);
            var converter = new System.Windows.Media.BrushConverter();
            List<ListViewItem> listViewItems = (List<ListViewItem>)PrescriptionList.ItemsSource;
            foreach (ListViewItem listViewItem in listViewItems)
            {
                listViewItem.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
                listViewItem.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            }
            ListViewItem prescription = (ListViewItem)PrescriptionList.SelectedItem;
            prescription.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            prescription.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
        }
    }
}
