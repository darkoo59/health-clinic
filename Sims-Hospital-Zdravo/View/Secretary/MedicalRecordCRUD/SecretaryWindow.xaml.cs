using Controller;
using Model;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
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
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {
        private MedicalRecordController _medicalController;
        private SecretaryAppointmentController _secretaryAppointmentController;
        public SecretaryWindow()
        {
            InitializeComponent();
            this._medicalController = new MedicalRecordController();
            _secretaryAppointmentController = new SecretaryAppointmentController();
            this.DataContext = this;
            UpdateGridView();
            ContentGrid.ItemsSource = this._medicalController.ReadAll();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            InsertRecordWindow insertWindow = new InsertRecordWindow();
            insertWindow.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ContentGrid.SelectedItem != null)
            {
                try
                {
                    MedicalRecord medical = (MedicalRecord)ContentGrid.SelectedValue;
                    Patient patient = medical.Patient;
                    UpdateRecordWindow updateWindow = new UpdateRecordWindow(patient, medical) { DataContext = ContentGrid.SelectedItem };
                    updateWindow.Show();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Please select medical record first!", "Select medical record", MessageBoxButton.OK);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                _medicalController.Delete((MedicalRecord)ContentGrid.SelectedItem);
            }
        }

        private void UpdateGridView()
        {
            ContentGrid.AutoGenerateColumns = false;
            ContentGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Name";
            dataColumn.Binding = new Binding("Patient.Name");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Surname";
            dataColumn.Binding = new Binding("Patient.Surname");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Jmbg";
            dataColumn.Binding = new Binding("Patient.Jmbg");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Gender";
            dataColumn.Binding = new Binding("Gender");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Blood type";
            dataColumn.Binding = new Binding("BloodType");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Marital status";
            dataColumn.Binding = new Binding("MaritalStatus");
            ContentGrid.Columns.Add(dataColumn);
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }
    }
}
