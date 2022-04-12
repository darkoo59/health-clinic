using Controller;
using Model;
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
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {
        private MedicalRecordController medicalController;
        public SecretaryWindow(MedicalRecordController controller)
        {
            InitializeComponent();
            this.medicalController = controller;
            this.DataContext = this;
            ContentGrid.ItemsSource = this.medicalController.ReadAll();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            InsertRecordWindow insertWindow = new InsertRecordWindow(medicalController);
            insertWindow.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord medical = (MedicalRecord)ContentGrid.SelectedValue;
            int patientId = medical._PatientId;
            UpdateRecordWindow updateWindow = new UpdateRecordWindow(medicalController,patientId) { DataContext = ContentGrid.SelectedItem };
            updateWindow.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                medicalController.Delete((MedicalRecord)ContentGrid.SelectedItem);
            }
        }
    }
}
