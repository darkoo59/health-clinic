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
                Patient patient = new Patient(_medicalRecordController.GeneratePatientId(), TxtName.Text, TxtSurname.Text, DateTime.Parse(TxtBirth.Text), TxtEmail.Text, TxtJmbg.Text, TxtPhone.Text);
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
                MedicalRecord medicalRecord = new MedicalRecord(_medicalRecordController.GenerateId(), patient, (GenderType)ComboGender.SelectedValue, (BloodType)ComboBlood.SelectedValue, (MaritalType)ComboMarital.SelectedValue, allergens);
                _medicalRecordController.Create(medicalRecord, patient);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_profile.Visibility = Visibility.Collapsed;
                tt_about.Visibility = Visibility.Collapsed;
                tt_meetings.Visibility = Visibility.Collapsed;
                tt_accounts.Visibility = Visibility.Collapsed;
                tt_equipment.Visibility = Visibility.Collapsed;
                tt_appointments.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_medical_records.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_sign_out.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_profile.Visibility = Visibility.Visible;
                tt_about.Visibility = Visibility.Visible;
                tt_meetings.Visibility = Visibility.Visible;
                tt_accounts.Visibility = Visibility.Visible;
                tt_equipment.Visibility = Visibility.Visible;
                tt_appointments.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_medical_records.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_sign_out.Visibility = Visibility.Visible;
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

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryHome window = new SecretaryHome();
            window.Show();
            this.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            ExaminationWindow window = new ExaminationWindow();
            window.Show();
            this.Close();
        }
        
        private void MedicalRecord_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryWindow window = new SecretaryWindow();
            window.Show();
            this.Close();
        }
        
        private void Equipment_Click(object sender, MouseButtonEventArgs e)
        {
            SuppliesHome window = new SuppliesHome();
            window.Show();
            this.Close();
        }
    }
}
