using Controller;
using Model;
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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for UpdateRecordWindow.xaml
    /// </summary>
    public partial class UpdateRecordWindow : Window
    {
        private MedicalRecordController medicalController;
        private MedicalRecord medicalRecord;
        private Patient patient;
        public UpdateRecordWindow(MedicalRecordController controller,Patient patient, MedicalRecord record)
        {
            InitializeComponent();
            medicalController = controller;
            this.medicalRecord = record;
            this.patient = patient;
            ComboGender.ItemsSource = Enum.GetValues(typeof(GenderType)).Cast<GenderType>();
            ComboBlood.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            ComboMarital.ItemsSource = Enum.GetValues(typeof(MaritalType)).Cast<MaritalType>();
            AllergensList.ItemsSource = medicalController.ReadAllAllergens();
            TxtName.Text = patient._Name;
            TxtSurname.Text = patient._Surname;
            TxtBirth.Text = patient._BirthDate.ToString("yyyy-MM-dd");
            TxtEmail.Text = patient._Email;
            TxtJmbg.Text = patient._Jmbg;
            TxtPhone.Text = patient._PhoneNumber;
            List<String> allergens = medicalRecord._Allergens;
            List<String> selectedAllergens = new List<String>();
          /*  foreach(ListItem item in AllergensList.Items){
                
            }*/
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient patientUpdated = new Patient(patient._Id, TxtName.Text, TxtSurname.Text, DateTime.Parse(TxtBirth.Text), TxtEmail.Text, TxtJmbg.Text, TxtPhone.Text);
                medicalController.ValidateUpdate(TxtJmbg.Text);
                MedicalRecord medicalRecordUpdated = new MedicalRecord(medicalRecord._Id, patient, (GenderType)ComboGender.SelectedValue, (BloodType)ComboBlood.SelectedValue, (MaritalType)ComboMarital.SelectedValue, new List<String>());
                medicalController.Update(medicalRecordUpdated, patientUpdated);
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
