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

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for ValidateMedicine.xaml
    /// </summary>
    public partial class ValidateMedicine : Window
    {
        private App app;
        private MedicineController medicineController;
        private NotificationController notificationController;
        private Medicine medicine;

        public ReviewMedicineNotification ReviewNotification { get; set; }
        public Medicine Medicine { get; set; }

        public ValidateMedicine(Medicine medicine)
        {
            InitializeComponent();
            medicineController = new MedicineController();
            notificationController = new NotificationController();
            this.medicine = medicine;
            MedicineNameTxt.Text = medicine.Name;
            StrenghtTxt.Text = medicine.Strength;
            UsingForTxt.Text = medicine.Description;
            AllegensTxt.Text = medicine.Allergens;
        }

        private void ValidateMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            string validateMedicine = "Medicine approved";
            string name = medicine.Name;
            medicine.Status = MedicineStatus.ACCEPTED;
            medicineController.Update(medicine);
            MedicineValidationType medicineValidationType = MedicineValidationType.MEDICINE_APPROVED;
            ReviewMedicineNotification reviewMedicineNotification = new ReviewMedicineNotification("Medicine" + name + "approved", validateMedicine, medicine, notificationController.GenerateId(), medicineValidationType);
            medicineController.ValidateMedicineWithNotifyindMenager(medicine, reviewMedicineNotification);
        }

        private void RejectMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            string validateMedicine = ReasonForRejectinMedicineTxt.Text;
            MedicineValidationType medicineValidationType = MedicineValidationType.MEDICINE_REJECTED;
            string name = medicine.Name;
            medicine.Status = MedicineStatus.ABORTED;
            medicineController.Update(medicine);
            ReviewMedicineNotification reviewMedicineNotification = new ReviewMedicineNotification("Medicine" + name + "rejected", validateMedicine, medicine, notificationController.GenerateId(), medicineValidationType);
            medicineController.ValidateMedicineWithNotifyindMenager(medicine, reviewMedicineNotification);
        }
    }
}