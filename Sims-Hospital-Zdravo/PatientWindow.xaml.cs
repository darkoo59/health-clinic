﻿using Controller;
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
    /// Interaction logic for PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        AppointmentPatientController appointmentPatientController;
        public PatientWindow(AppointmentPatientController appointmentPatientController)
        {
            InitializeComponent();
            this.appointmentPatientController = appointmentPatientController;
            this.DataContext = this;
            McDataGrid.ItemsSource = appointmentPatientController.FindByPatientID(1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PatientCreate pc = new PatientCreate(appointmentPatientController);
            pc.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)McDataGrid.SelectedValue;
            PatientUpdate up = new PatientUpdate(appointmentPatientController, appointment) { DataContext = McDataGrid.SelectedItem };
            up.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                appointmentPatientController.Delete((Appointment)McDataGrid.SelectedItem);
            }
        }

        private void McDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
    }
}
