using System;
using System.Windows;
using System.Windows.Controls;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Manager.Medicines;
using Sims_Hospital_Zdravo.ViewModel;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerMedicines : Page
    {
        // private App app;
        // private MedicineController medicineController;
        // private Frame ManagerContent;

        public ManagerMedicines()
        {
            this.DataContext = new ManagerMedicinesViewModel();
            InitializeComponent();
        }
    }
}