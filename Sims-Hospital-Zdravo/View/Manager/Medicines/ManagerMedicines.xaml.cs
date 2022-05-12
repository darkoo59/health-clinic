using System;
using System.Windows;
using System.Windows.Controls;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerMedicines : Page
    {
        private App app;
        private MedicineController medicineController;
        private Frame ManagerContent;

        public ManagerMedicines()
        {
            app = Application.Current as App;
            medicineController = app._medicineController;
            InitializeComponent();

            MedicinesTable.ItemsSource = medicineController.ReadAllMedicines();
            RetrieveMainFrame();
        }

        private void InsertMedicine_Click(object sender, RoutedEventArgs e)
        {
            ManagerContent.Source = new Uri("Medicines/ManagerMedicineInsert.xaml", UriKind.Relative);
        }

        private void RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    ManagerContent = ((ManagerMainWindow)win).ManagerContent;
                }
            }
        }
    }
}