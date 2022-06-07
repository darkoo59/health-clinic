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
using Sims_Hospital_Zdravo.Model;
using Model;
using Controller;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for DoctorMedicines.xaml
    /// </summary>
    public partial class DoctorMedicines : Page
    {
        private MedicineController medicineController;
        private App app;
        private Frame frame;

        public DoctorMedicines(Frame frame)
        {
            InitializeComponent();
            this.DataContext = this;
            this.frame = frame;
            app = App.Current as App;
            medicineController = new MedicineController();
            MedicinesDataGrid.ItemsSource = medicineController.ReadAllMedicines();
            
            
        }

        public void ValidateSelection()
        {
            if( MedicinesDataGrid.SelectedValue == null)
            {
                throw new Exception("Click on medicine you want to review and validate");
            }
        }
        private void To_Validate_Medicine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateSelection();
                Medicine medicine = MedicinesDataGrid.SelectedValue as Medicine;
                ValidateMedicine validateMedicine = new ValidateMedicine(medicine);
                validateMedicine.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                
            }
        }
    }
}
