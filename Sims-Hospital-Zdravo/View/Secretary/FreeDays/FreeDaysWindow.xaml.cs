using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;
using Sims_Hospital_Zdravo.ViewModel.Secretary;
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

namespace Sims_Hospital_Zdravo.View.Secretary.FreeDays
{
    /// <summary>
    /// Interaction logic for FreeDaysWindow.xaml
    /// </summary>
    public partial class FreeDaysWindow : Window
    {
        
        private RequestForFreeDaysController _freeDaysController;
        private FreeDaysViewModel viewModel;
        private App app;
        public FreeDaysWindow()
        {
            InitializeComponent();
            app = Application.Current as App;
            this._freeDaysController = new RequestForFreeDaysController();
            viewModel = new FreeDaysViewModel();
            this.DataContext = viewModel;
            UpdateGridView();
            ContentGrid.ItemsSource = this._freeDaysController.FindAll();
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
            dataColumn.Binding = new Binding("Doctor.Name");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Surname";
            else 
                dataColumn.Header = "Prezime";
            dataColumn.Binding = new Binding("Doctor.Surname");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Start";
            else 
                dataColumn.Header = "Početak";
            dataColumn.Binding = new Binding("TimeInterval.Start");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "End";
            else 
                dataColumn.Header = "Kraj";
            dataColumn.Binding = new Binding("TimeInterval.End");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Status";
            dataColumn.Binding = new Binding("Status");
            ContentGrid.Columns.Add(dataColumn);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ContentGrid.SelectedItem != null)
            {
                try
                {
                    viewModel.UpdateRequestCommand.Execute(ContentGrid.SelectedItem);
                    this.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Please select medical record first!", "Select medical record", MessageBoxButton.OK);
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
