using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;
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
        public FreeDaysWindow()
        {
            InitializeComponent();
            this._freeDaysController = new RequestForFreeDaysController();
            this.DataContext = this;
            UpdateGridView();
            ContentGrid.ItemsSource = this._freeDaysController.FindAll();
        }

        private void UpdateGridView()
        {
            ContentGrid.AutoGenerateColumns = false;
            ContentGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Name";
            dataColumn.Binding = new Binding("Doctor.Name");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Surname";
            dataColumn.Binding = new Binding("Doctor.Surname");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Start";
            dataColumn.Binding = new Binding("TimeInterval.Start");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "End";
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
                    FreeDaysRequest freeDaysRequest = (FreeDaysRequest)ContentGrid.SelectedValue;
                    EditRequestWindow window = new EditRequestWindow(freeDaysRequest);
                    window.Show();

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
