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
                Patient patient = new Patient(TxtName.Text, TxtSurname.Text, DateTime.Parse(TxtBirth.Text), TxtEmail.Text, TxtJmbg.Text, TxtPhone.Text);
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
                MedicalRecord medicalRecord = new MedicalRecord(patient, (GenderType)ComboGender.SelectedValue, (BloodType)ComboBlood.SelectedValue, (MaritalType)ComboMarital.SelectedValue, allergens);
                _medicalRecordController.Create(medicalRecord, patient);
                Close();
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
    }
}
