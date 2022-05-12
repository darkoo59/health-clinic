using System.Windows;
using System.Windows.Controls;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerMedicines : Page
    {
        private App app;
        private MedicineController medicineController;

        public ManagerMedicines()
        {
            app = Application.Current as App;
            medicineController = app._medicineController;
            InitializeComponent();

            MedicinesTable.ItemsSource = medicineController.ReadAllMedicines();
        }
    }
}