using System;
using System.Windows;
using System.Windows.Controls;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Manager.Medicines;

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
            ManagerMedicineInsert medicineInsert = new ManagerMedicineInsert();
            if (medicineInsert.ShowDialog() == false)
            {
                Medicine medicine = medicineInsert.Medicine;
                Notification notification = medicineInsert._CreatedNotification;
                if (medicine != null)
                {
                    medicineController.CreateMedicineWithNotifyingDoctor(medicine, notification);
                }
            }
            // ManagerContent.Source = new Uri("Medicines/ManagerMedicineInsert.xaml", UriKind.Relative);
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

        private void UpdateMedicine_Click(object sender, RoutedEventArgs e)
        {
            Medicine medicine = (Medicine)MedicinesTable.SelectedItem;
            if (medicine == null)
            {
                MessageBox.Show("Medicine should be selected");
                return;
            }

            if (medicine._Status != MedicineStatus.ABORTED)
            {
                MessageBox.Show("Medicine is already accepted or pending!");
                return;
            }

            ManagerMedicineUpdate medicineUpdate = new ManagerMedicineUpdate(medicine)
            {
                DataContext = medicine
            };
            if (medicineUpdate.ShowDialog() == false)
            {
                Medicine med = medicineUpdate.Medicine;
                Notification notification = medicineUpdate._CreatedNotification;
                if (med != null)
                {
                    medicineController.ResubmitMedicineWithNotifyingDoctor(medicine, notification);
                }
            }
        }
    }
}