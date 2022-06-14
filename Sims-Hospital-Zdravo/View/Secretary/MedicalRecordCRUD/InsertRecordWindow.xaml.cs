using Controller;
using Model;
using Sims_Hospital_Zdravo.Interfaces;
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
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for InsertRecordWindow.xaml
    /// </summary>
    public partial class InsertRecordWindow : Window
    {
        private MedicalRecordController _medicalRecordController;
        public MedicalRecord MedicalRecord { get; set; }
        public Patient Patient { get; set; }
        public InsertRecordWindow()
        {
            InitializeComponent();
            this._medicalRecordController = new MedicalRecordController();
            ComboBlood.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            ComboGender.ItemsSource = Enum.GetValues(typeof(GenderType)).Cast<GenderType>();
            ComboMarital.ItemsSource = Enum.GetValues(typeof(MaritalType)).Cast<MaritalType>();
            AllergensList.ItemsSource = _medicalRecordController.ReadAllCommonAllergens();
            MedicalAllergensListBox.ItemsSource = _medicalRecordController.ReadAllMedicalAllergens();
        }



        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
                this.Patient = new Patient(TxtName.Text, TxtSurname.Text, DateTime.Parse(TxtBirth.Text), TxtEmail.Text, TxtJmbg.Text, TxtPhone.Text);
                List<String> allergensItems = new List<String>();
                List<String> medicalAllergensList = new List<String>();
                foreach (string str in AllergensList.SelectedItems)
                {
                    allergensItems.Add(str);
                }
                foreach (string str in MedicalAllergensListBox.SelectedItems)
                {
                    medicalAllergensList.Add(str);
                }

                Allergens allergens = new Allergens();
                allergens.CommonAllergens = allergensItems;
                allergens.MedicalAllergens = medicalAllergensList;
                this.MedicalRecord = new MedicalRecord(Patient, (GenderType)ComboGender.SelectedValue, (BloodType)ComboBlood.SelectedValue, (MaritalType)ComboMarital.SelectedValue, allergens);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        
        private void Validate()
        {
            ValidateNumber(TxtJmbg.Text, "Jmbg");
            ValidateComboBoxSelected();
        }

        private void ValidateComboBoxSelected()
        {
            if (ComboGender.SelectedIndex == -1)
                throw new Exception("Gender type should be selected");
            if (ComboBlood.SelectedIndex == -1)
                throw new Exception("Blood type should be selected");
            if (ComboMarital.SelectedIndex == -1)
                throw new Exception("Marital status should be selected");
        }

        private void ValidateNumber(string text, string property)
        {
            int number;
            bool isValid = Int32.TryParse(text, out number);
            if (!isValid)
                throw new Exception(property + " should be number!");
        }

    }
}
