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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {
        private MedicalRecordController medicalController;
        private App app;
        public SecretaryWindow(MedicalRecordController controller)
        {
            app = Application.Current as App;
            InitializeComponent();
            this.medicalController = controller;
            this.DataContext = this;
            UpdateGridView();
            ContentGrid.ItemsSource = this.medicalController.ReadAll();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            InsertRecordWindow insertWindow = new InsertRecordWindow(medicalController);
            insertWindow.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ContentGrid.SelectedItem != null)
            {
                try
                {
                    MedicalRecord medical = (MedicalRecord)ContentGrid.SelectedValue;
                    Patient patient = medical._Patient;
                    UpdateRecordWindow updateWindow = new UpdateRecordWindow(medicalController, patient, medical) { DataContext = ContentGrid.SelectedItem };
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
                medicalController.Delete((MedicalRecord)ContentGrid.SelectedItem);
            }
        }

        private void UpdateGridView()
        {
            ContentGrid.AutoGenerateColumns = false;
            ContentGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Name";
            dataColumn.Binding = new Binding("_Patient._Name");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Surname";
            dataColumn.Binding = new Binding("_Patient._Surname");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Jmbg";
            dataColumn.Binding = new Binding("_Patient._Jmbg");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Gender";
            dataColumn.Binding = new Binding("_Gender");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Blood type";
            dataColumn.Binding = new Binding("_BloodType");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Marital status";
            dataColumn.Binding = new Binding("_MaritalStatus");
            ContentGrid.Columns.Add(dataColumn);
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_profile.Visibility = Visibility.Collapsed;
                tt_about.Visibility = Visibility.Collapsed;
                tt_meetings.Visibility = Visibility.Collapsed;
                tt_accounts.Visibility = Visibility.Collapsed;
                tt_equipment.Visibility = Visibility.Collapsed;
                tt_appointments.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_medical_records.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_sign_out.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_profile.Visibility = Visibility.Visible;
                tt_about.Visibility = Visibility.Visible;
                tt_meetings.Visibility = Visibility.Visible;
                tt_accounts.Visibility = Visibility.Visible;
                tt_equipment.Visibility = Visibility.Visible;
                tt_appointments.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_medical_records.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_sign_out.Visibility = Visibility.Visible;
            }
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

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryHome window = new SecretaryHome();
            window.Show();
            this.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            ExaminationWindow window = new ExaminationWindow(app._secretaryAppointmentController);
            window.Show();
            this.Close();
        }
    }
}
