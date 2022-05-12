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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for InsertRecordWindow.xaml
    /// </summary>
    public partial class InsertRecordWindow : Window
    {
        private MedicalRecordController medicalController;
        public InsertRecordWindow(MedicalRecordController controller)
        {
            InitializeComponent();
            this.medicalController = controller;
            ComboBlood.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            ComboGender.ItemsSource = Enum.GetValues(typeof(GenderType)).Cast<GenderType>();
            ComboMarital.ItemsSource = Enum.GetValues(typeof(MaritalType)).Cast<MaritalType>();
            AllergensList.ItemsSource = medicalController.ReadAllAllergens();
        }



        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient patient = new Patient(medicalController.GeneratePatientId(), TxtName.Text, TxtSurname.Text, DateTime.Parse(TxtBirth.Text), TxtEmail.Text, TxtJmbg.Text, TxtPhone.Text);
                List<String> allergensItems = new List<String>();
                foreach (string str in AllergensList.SelectedItems)
                {
                    allergensItems.Add(str);
                }

                Allergens allergens = new Allergens();
                allergens._Allergens = allergensItems;
                MedicalRecord medicalRecord = new MedicalRecord(medicalController.GenerateId(), patient, (GenderType)ComboGender.SelectedValue, (BloodType)ComboBlood.SelectedValue, (MaritalType)ComboMarital.SelectedValue, allergens);
                medicalController.Create(medicalRecord, patient);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
