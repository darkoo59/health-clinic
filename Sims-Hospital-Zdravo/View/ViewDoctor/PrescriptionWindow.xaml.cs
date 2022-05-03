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
using Controller;
using Sims_Hospital_Zdravo.Model;
using System.Collections.ObjectModel;
using DataHandler;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Controller;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for PrescriptionWindow.xaml
    /// </summary>
    public partial class PrescriptionWindow : Window
    {
        private MedicalRecordController medicalRecordController;
        private MedicineController medicineController;
        private App app;
        private int doctorId;
        private ObservableCollection<Medicine> medicines;
        private List<string> meds;
        private Medicine medicine;
        private MedicalRecord medicalRecord;
        private string dosage;
        private string strength;
        private DateTime startDate;
        private DateTime endDate;

        public Medicine _Medicine
        {
            get
            {
                return medicine;
            }
            set
            {
                medicine = value;
            }
        }
        public PrescriptionWindow(MedicalRecordController medicalRecordController, MedicalRecord medicalRecord, int id)
        {
            InitializeComponent();
            this.medicalRecordController = medicalRecordController;
            this.medicalRecord = medicalRecord;
            this.doctorId = id;
            app = App.Current as App;
            this.medicineController = app._medicineController;
            medicines = medicineController.ReadAllMedicines();
            PatientTxt.Text = medicalRecord._Patient._Name + medicalRecord._Patient._Surname;
            // DateTxt.Text = DateTime.Now.ToString();

            meds = new List<string>();

            foreach (Medicine md in medicines)
            {
                meds.Add(md._Name + " " + md._Strength);
            }


            Medicinecb.ItemsSource = meds;


        }
        private void Medicinecb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string med = Medicinecb.SelectedItem.ToString();
            string[] names = med.Split(' ');
            foreach (Medicine md in medicineController.ReadAllMedicines())
            {
                if (md._Name.Equals(names[0]) && md._Strength.Equals(names[1]))
                {
                    _Medicine = md;
                    break;
                }
            }
            Console.WriteLine(_Medicine._Allergens);
            allergensTxt.Text = _Medicine._Allergens;
            descriptionTxt.Text = _Medicine._Description;
        }

        public void validateFields()
        {
            if (strengthTxt.Text == "" || DosageTxt.Text == "" || startDate.Equals(null) || endDate.Equals(null))
            {
                MessageBox.Show("All fields must be filled");

            }
        }
        private void start_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            startDate = start.SelectedDate.Value;

        }

        private void End_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            endDate = End.SelectedDate.Value;
        }
        private void Prescription_Click(object sender, RoutedEventArgs e)
        {
            if (strengthTxt.Text == "" || DosageTxt.Text == "" || startDate.Equals(null) || endDate.Equals(null)|| Medicinecb.SelectedItem==null)
            {
                MessageBox.Show("All fields must be filled");

            }
            else
            {
                strength = strengthTxt.Text;
                dosage = DosageTxt.Text;
                DateTime date = DateTime.Now;

                Console.WriteLine(this.startDate + " " + this.endDate);
                DateTime startDate = DateTime.Parse(start.Text);
                DateTime endDate = DateTime.Parse(End.Text);
                TimeInterval tl = new TimeInterval(startDate, endDate);
                Doctor doctor = app._doctorAppointmentController.getDoctor(doctorId);
                int numOfDays = 20;
                Prescription prescription = new Prescription(_Medicine, date, strength, tl, doctor, dosage, numOfDays);
                medicalRecordController.createPrescription(prescription);

                medicalRecord._Prescriptions.Add(prescription);
                PrescriptionList prescriptionList = new PrescriptionList(medicalRecordController, medicalRecord, prescription, doctorId);
                prescriptionList.Show();
            }
        }

      
    }
}
