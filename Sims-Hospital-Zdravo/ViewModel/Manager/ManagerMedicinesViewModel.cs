using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Manager.Medicines;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public class ManagerMedicinesViewModel : INotifyPropertyChanged
    {
        public List<Medicine> Medicines { get; set; }

        public ICommand InsertMedicine_Command => new InsertMedicine_Command();

        public ICommand UpdateMedicine_Command => new UpdateMedicine_Command();

        private object selectedItem;
        private MedicineController medicineController;

        public ManagerMedicinesViewModel()
        {
            medicineController = new MedicineController();
            Medicines = medicineController.ReadAllMedicines().ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class InsertMedicine_Command : ICommand
    {
        private MedicineController medicineController;

        public InsertMedicine_Command()
        {
            this.medicineController = new MedicineController();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ManagerMedicineInsert medicineInsert = new ManagerMedicineInsert();
            if (medicineInsert.ShowDialog() == false)
            {
                Medicine medicine = medicineInsert.Medicine;
                Notification notification = medicineInsert._CreatedNotification;

                if (medicine != null)
                {
                    medicineController.CreateMedicineWithNotifyingDoctor(medicine, notification);
                    MessageBox.Show("Success");
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }

    public class UpdateMedicine_Command : ICommand
    {
        private MedicineController medicineController;

        public UpdateMedicine_Command()
        {
            medicineController = new MedicineController();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Medicine medicine = parameter as Medicine;
            if (medicine == null)
            {
                MessageBox.Show("Medicine should be selected");
                return;
            }

            if (medicine.Status != MedicineStatus.ABORTED)
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
                Notification notification = medicineUpdate.CreatedNotification;

                if (med != null)
                {
                    medicineController.ResubmitMedicineWithNotifyingDoctor(med, notification);
                    MessageBox.Show("Success");
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}