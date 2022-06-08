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
    public partial class ManagerMedicineUpdate : Window
    {
        private App app;
        private MedicineController medicineController;
        private NotificationController notificationController;
        private DoctorAppointmentController doctorAppointmentController;
        public MedicineCreatedNotification CreatedNotification { get; set; }
        public Medicine Medicine { get; set; }


        public ManagerMedicineUpdate(Medicine medicine)
        {
            Medicine = medicine;
            app = Application.Current as App;
            medicineController = new MedicineController();
            notificationController = new NotificationController();
            doctorAppointmentController = app._doctorAppointmentController;
            InitializeComponent();

            ComboDoctors.ItemsSource = doctorAppointmentController.ReadAllDoctors();
            MedicineSubstitues.ItemsSource = FilterSelfFromMedicines(medicineController.ReadAllMedicines().ToList());
            this.KeyDown += new KeyEventHandler(GoBack);
        }

        private void GoBack(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                Close();
            }
        }


        private void SaveMedicine_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                Validate();

                Doctor doctor = (Doctor)ComboDoctors.SelectedItem;
                List<Medicine> substitutes = new List<Medicine>();
                if (MedicineSubstitues.SelectedItems.Count != 0)
                {
                    substitutes = new List<Medicine>(MedicineSubstitues.SelectedItems.Cast<Medicine>());
                }

                Console.WriteLine(notificationController + " controller");
                Console.WriteLine(doctor + " doctor");
                FillMedicine();
                Medicine.Substitution = substitutes;
                CreatedNotification = new MedicineCreatedNotification("Medicine " + TxtMedicineName.Name + " added!", doctor.Id, this.Medicine, notificationController.GenerateId());
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private void FillMedicine()
        {
            string name = TxtMedicineName.Text;
            string allergens = TxtAllergens.Text;
            string description = TxtDescription.Text;
            string strength = TxtStrength.Text;
            List<Medicine> substitutes = new List<Medicine>(MedicineSubstitues.SelectedItems.Cast<Medicine>());
            Medicine.Name = name;
            Medicine.Allergens = allergens;
            Medicine.Description = description;
            Medicine.Strength = strength;
            Medicine.Substitution = substitutes;
            Medicine.Status = MedicineStatus.PENDING;
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

        private List<Medicine> FilterSelfFromMedicines(List<Medicine> medicines)
        {
            return medicines.Where(medicine => medicine.Id != this.Medicine.Id).ToList();
        }
    }
}