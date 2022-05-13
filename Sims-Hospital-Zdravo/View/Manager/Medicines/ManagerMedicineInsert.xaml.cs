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
            string name = TxtMedicineName.Text;
            string allergens = TxtAllergens.Text;
            string description = TxtDescription.Text;
            string strength = TxtStrength.Text;
            Medicine medicine = new Medicine(name, strength, allergens, description);
            Doctor doctor = (Doctor)ComboDoctors.SelectedItem;
            MedicineApprovalNotification notification = new MedicineApprovalNotification("Medicine " + name + " added!", doctor._Id, medicine, notificationController.GenerateId());
            medicineController.CreateMedicineWithNotifyingDoctor(medicine, notification);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}