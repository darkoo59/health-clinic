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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.Model;
using Model;
using Controller;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for ListOfMedecinesinSystem.xaml
    /// </summary>
    public partial class ListOfMedecinesinSystem : Page
    {
        private MedicineController medicineController;
        private MedicalRecordController medicalRecordController;
        private MedicalRecord medicalRecord;
        private int doctorId;
        private App app;
        private ObservableCollection<Medicine> medicines;
        private Frame frame;
        public ListOfMedecinesinSystem(int id,MedicalRecord medicalRecord,Frame frame)
        {
            InitializeComponent();
            this.app = App.Current as App;
            this.frame = frame;
            this.medicineController = app._medicineController;
            this.medicalRecordController = app._recordController;
            this.medicalRecord = medicalRecord;
            this.doctorId = id;
            
            medicineController.ReturnListOfMedicineToStart();
            medicineController.CheckIfPatientAllergicToMedicine(medicalRecord);
            medicineController.CheckIfPatientAllergicToMedicineIngredients(medicalRecord);
            medicines = medicineController.ReadAllMedicines();
            MedicineListBox.ItemsSource = medicines;
            


        }

        public void DisableIfAllergic()
        {
            ObservableCollection<Medicine> medicines = app._medicineController.ReadAllMedicines();

        }

        private void MedicineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medicine medicine = MedicineListBox.SelectedItem as Medicine;
            PrescriptionWindow prescriptionWindow = new PrescriptionWindow(medicalRecordController, medicalRecord, doctorId, medicine, frame);
            prescriptionWindow.Show();


        }
    }
}
