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
using Sims_Hospital_Zdravo.ViewModel.Secretary;
using Xceed.Wpf.Toolkit;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {
        private MedicalRecordController _medicalController;
        private SecretaryAppointmentController _secretaryAppointmentController;
        private MedicalRecordViewModel viewModel;
        private App app;
        public SecretaryWindow()
        {
            InitializeComponent();
            app = Application.Current as App;
            this._medicalController = new MedicalRecordController();
            _secretaryAppointmentController = new SecretaryAppointmentController();
            viewModel = new MedicalRecordViewModel();
            this.DataContext = viewModel;
            UpdateGridView();
            ContentGrid.ItemsSource = this._medicalController.FindAll();
        }

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            viewModel.InsertRecordCommand.Execute(null);
            this.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    viewModel.UpdateRecordCommand.Execute((MedicalRecord)ContentGrid.SelectedValue);
                    this.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.DeleteRecordCommand.Execute((MedicalRecord)ContentGrid.SelectedValue);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void UpdateGridView()
        {
            ContentGrid.AutoGenerateColumns = false;
            ContentGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Name";
            else 
                dataColumn.Header = "Ime";
            dataColumn.Binding = new Binding("Patient.Name");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Surname";
            else 
                dataColumn.Header = "Prezime";
            dataColumn.Binding = new Binding("Patient.Surname");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Jmbg";
            dataColumn.Binding = new Binding("Patient.Jmbg");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Gender";
            else 
                dataColumn.Header = "Pol";
            dataColumn.Binding = new Binding("Gender");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Blood type";
            else 
                dataColumn.Header = "Krvna grupa";
            dataColumn.Binding = new Binding("BloodType");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Marital status";
            else 
                dataColumn.Header = "Bračni status";
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
