using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.View.Manager.Medicines
{
    public partial class ManagerMedicineInsert : Window
    {
        private App app;
        private MedicineController medicineController;
        private NotificationController notificationController;
        private DoctorAppointmentController doctorAppointmentController;
        public MedicineCreatedNotification _CreatedNotification { get; set; }
        public Medicine Medicine { get; set; }

        public ManagerMedicineInsert()
        {
            app = Application.Current as App;
            medicineController = new MedicineController();
            notificationController = new NotificationController();
            doctorAppointmentController = app._doctorAppointmentController;
            InitializeComponent();

            ComboDoctors.ItemsSource = doctorAppointmentController.ReadAllDoctors();
            MedicineSubstitues.ItemsSource = medicineController.ReadAllMedicines();
            this.KeyDown += new KeyEventHandler(GoBack);
        }

        private void GoBack(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                Close();
            }
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
                Doctor doctor = (Doctor)ComboDoctors.SelectedItem;
                List<Medicine> substitutes = new List<Medicine>(MedicineSubstitues.SelectedItems.Cast<Medicine>());

                this.Medicine = new Medicine(name, strength, allergens, description);
                Medicine.Id = medicineController.GenerateId();
                this.Medicine.Substitution = substitutes;
                this._CreatedNotification = new MedicineCreatedNotification("Medicine " + name + " added!", doctor.Id, this.Medicine, notificationController.GenerateId());
                Close();
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
    }
}