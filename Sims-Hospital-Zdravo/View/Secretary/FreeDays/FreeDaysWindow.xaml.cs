﻿using Sims_Hospital_Zdravo.Controller;
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

        private App app;
        private RequestForFreeDaysController _freeDaysController;
        public FreeDaysWindow(RequestForFreeDaysController controller)
        {
            app = Application.Current as App;
            InitializeComponent();
            this._freeDaysController = controller;
            this.DataContext = this;
            UpdateGridView();
            ContentGrid.ItemsSource = this._freeDaysController.ReadAll();
        }

        private void UpdateGridView()
        {
            ContentGrid.AutoGenerateColumns = false;
            ContentGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Name";
            dataColumn.Binding = new Binding("Doctor._Name");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Surname";
            dataColumn.Binding = new Binding("Doctor._Surname");
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

        private void MedicalRecord_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryWindow window = new SecretaryWindow(app._recordController);
            window.Show();
            this.Close();
        }

        private void Equipment_Click(object sender, MouseButtonEventArgs e)
        {
            SuppliesHome window = new SuppliesHome();
            window.Show();
            this.Close();
        }
    }
}
