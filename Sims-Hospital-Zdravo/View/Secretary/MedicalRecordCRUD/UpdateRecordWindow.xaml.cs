using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
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
    /// Interaction logic for UpdateRecordWindow.xaml
    /// </summary>
    public partial class UpdateRecordWindow : Window
    {
        private MedicalRecordController _medicalRecordController;
        private MedicalRecord medicalRecord;
        private Patient patient;

        public UpdateRecordWindow(Patient patient, MedicalRecord record)
        {
            InitializeComponent();
            _medicalRecordController = new MedicalRecordController();
            this.medicalRecord = record;
            this.patient = patient;
            ComboGender.ItemsSource = Enum.GetValues(typeof(GenderType)).Cast<GenderType>();
            ComboBlood.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            ComboMarital.ItemsSource = Enum.GetValues(typeof(MaritalType)).Cast<MaritalType>();
            foreach (String str in medicalRecord.PatientAllergens.CommonAllergens)
            {
                ListPatientAllergens.Items.Add(str);
            }

            foreach (String str in medicalRecord.PatientAllergens.MedicalAllergens)
            {
                ListPatientMedicalAllergens.Items.Add(str);
            }

            TxtName.Text = patient._Name;
            TxtSurname.Text = patient._Surname;
            TxtBirth.Text = patient._BirthDate.ToString("yyyy-MM-dd");
            TxtEmail.Text = patient._Email;
            TxtJmbg.Text = patient._Jmbg;
            TxtPhone.Text = patient._PhoneNumber;
            foreach (String str in _medicalRecordController.ReadAllCommonAllergens())
            {
                if (!medicalRecord.PatientAllergens.CommonAllergens.Contains(str))
                {
                    ListOtherAllergens.Items.Add(str);
                }
            }

            foreach (String str in _medicalRecordController.ReadAllMedicalAllergens())
            {
                if (!medicalRecord.PatientAllergens.MedicalAllergens.Contains(str))
                {
                    ListOtherMedicalAllergens.Items.Add(str);
                }
            }

            //Images listeners

            ImageToLeftCommon.MouseLeftButtonDown += (s, e) =>
            {
                imageToLeftCommonFunctionality();
            };

            ImageToRightCommon.MouseLeftButtonDown += (s, e) =>
            {
                ImageToRightCommonFunctionality();
            };

            ImageToLeftMedical.MouseLeftButtonDown += (s, e) =>
            {
                imageToLeftMedicalFunctionality();
            };

            ImageToRightMedical.MouseLeftButtonDown += (s, e) =>
            {
                ImageToRightMedicalFunctionality();
            };
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient patientUpdated = new Patient(TxtName.Text, TxtSurname.Text, DateTime.Parse(TxtBirth.Text), TxtEmail.Text, TxtJmbg.Text, TxtPhone.Text);
                patientUpdated._Id = patient._Id;
                List<String> allergens = new List<String>();
                List<String> medicalAllergens = new List<String>();
                foreach (String str in ListPatientAllergens.Items)
                {
                    allergens.Add(str);
                }
                foreach (String str in ListPatientMedicalAllergens.Items)
                {
                    medicalAllergens.Add(str);
                }

                Allergens updatedAllergens = new Allergens();
                updatedAllergens.CommonAllergens = allergens;
                updatedAllergens.MedicalAllergens = medicalAllergens;
                MedicalRecord medicalRecordUpdated = new MedicalRecord(patientUpdated, (GenderType)ComboGender.SelectedValue, (BloodType)ComboBlood.SelectedValue, (MaritalType)ComboMarital.SelectedValue,
                    updatedAllergens);
                _medicalRecordController.Update(medicalRecordUpdated, patientUpdated);
                Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void imageToLeftCommonFunctionality()
        {
            foreach (String str in ListOtherAllergens.SelectedItems)
            {
                ListPatientAllergens.Items.Add(str);
            }

            ListOtherAllergens.Items.Clear();
            foreach (String str in _medicalRecordController.ReadAllCommonAllergens())
            {
                if (!ListPatientAllergens.Items.Contains(str))
                    ListOtherAllergens.Items.Add(str);
            }
        }

        private void ImageToRightCommonFunctionality()
        {
            foreach (String str in ListPatientAllergens.SelectedItems)
            {
                ListOtherAllergens.Items.Add(str);
            }

            ListPatientAllergens.Items.Clear();
            foreach (String str in _medicalRecordController.ReadAllCommonAllergens())
            {
                if (!ListOtherAllergens.Items.Contains(str))
                    ListPatientAllergens.Items.Add(str);
            }
        }

        private void imageToLeftMedicalFunctionality()
        {
            foreach (String str in ListOtherMedicalAllergens.SelectedItems)
            {
                ListPatientMedicalAllergens.Items.Add(str);
            }

            ListOtherMedicalAllergens.Items.Clear();
            foreach (String str in _medicalRecordController.ReadAllMedicalAllergens())
            {
                if (!ListPatientMedicalAllergens.Items.Contains(str))
                    ListOtherMedicalAllergens.Items.Add(str);
            }
        }

        private void ImageToRightMedicalFunctionality()
        {
            foreach (String str in ListPatientMedicalAllergens.SelectedItems)
            {
                ListOtherMedicalAllergens.Items.Add(str);
            }

            ListPatientMedicalAllergens.Items.Clear();
            foreach (String str in _medicalRecordController.ReadAllMedicalAllergens())
            {
                if (!ListOtherMedicalAllergens.Items.Contains(str))
                    ListPatientMedicalAllergens.Items.Add(str);
            }
        }

        private void ListViewItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
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