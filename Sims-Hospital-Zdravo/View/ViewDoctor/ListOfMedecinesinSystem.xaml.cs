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
        private List<Medicine> medicines;
        private Frame frame;
        public ListOfMedecinesinSystem(int id,MedicalRecord medicalRecord,Frame frame)
        {
            InitializeComponent();
            
            this.frame = frame;
            this.medicineController = new MedicineController();
            this.medicalRecordController = new MedicalRecordController();
            this.medicalRecord = medicalRecord;
            this.doctorId = id;
            
            medicineController.ReturnListOfMedicineToStart();
            medicines = medicineController.PatientAllergicToMedicine(medicalRecord);
            MedicineListBox.ItemsSource = medicines;

            
            


        }

        private void StringAllergic()
        {
            foreach(Medicine med in medicineController.ReadAllMedicines())
            {
                if(med.NotAllergic == true)
                {

                }
            }
        }

        private void MedicineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medicine medicine = MedicineListBox.SelectedItem as Medicine;
            PrescriptionWindow prescriptionWindow = new PrescriptionWindow( medicalRecord, doctorId, medicine,frame);
            prescriptionWindow.Show();


        }
    }
}
