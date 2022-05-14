using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.View.Manager.Medicines
{
    public partial class ManagerMedicineInsert : Page
    {
        private App app;
        private MedicineController medicineController;
        private NotificationController notificationController;
        private DoctorAppointmentController doctorAppointmentController;

        public ManagerMedicineInsert()
        {
            app = Application.Current as App;
            medicineController = app._medicineController;
            notificationController = app._notificationController;
            doctorAppointmentController = app._doctorAppointmentController;
            InitializeComponent();

            ComboDoctors.ItemsSource = doctorAppointmentController.ReadAllDoctors();
        }

        private void SaveMedicine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
                string name = TxtMedicineName.Text;
                string allergens = TxtAllergens.Text;
                string description = TxtDescription.Text;
                string strength = TxtStrength.Text;
                Medicine medicine = new Medicine(name, strength, allergens, description);
                Doctor doctor = (Doctor)ComboDoctors.SelectedItem;
                MedicineApprovalNotification notification = new MedicineApprovalNotification("Medicine " + name + " added!", doctor._Id, medicine, notificationController.GenerateId());
                medicineController.CreateMedicineWithNotifyingDoctor(medicine, notification);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Validate()
        {
            ValidateTxtField(TxtMedicineName, "Medicine Name");
            ValidateTxtField(TxtAllergens, "Allergens");
            ValidateTxtField(TxtDescription, "Description");
            ValidateTxtField(TxtStrength, "Strength");
            ValidateComboSelected(ComboDoctors, "Doctor");
        }

        private void ValidateComboSelected(ComboBox box, string name)
        {
            if (box.SelectedItem == null)
            {
                throw new Exception(name + " should be selected!");
            }
        }

        private void ValidateTxtField(TextBox txt, string name)
        {
            if (txt.Text.Equals(""))
            {
                throw new Exception("Field " + name + " shouldn't be empty!");
            }
        }

        private void ValidateDatePicker(DatePicker datePicker, string name)
        {
            if (datePicker.Text.Equals(""))
            {
                throw new Exception(name + " should have a value!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}