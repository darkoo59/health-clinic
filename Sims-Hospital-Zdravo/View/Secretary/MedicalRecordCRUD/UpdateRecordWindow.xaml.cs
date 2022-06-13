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

        public UpdateRecordWindow()
        {
            InitializeComponent();
            _medicalRecordController = new MedicalRecordController();
            ComboGender.ItemsSource = Enum.GetValues(typeof(GenderType)).Cast<GenderType>();
            ComboBlood.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            ComboMarital.ItemsSource = Enum.GetValues(typeof(MaritalType)).Cast<MaritalType>();
            /*ComboGender.SelectedIndex = (int)record.Gender;
            ComboBlood.SelectedIndex = (int)record.BloodType;
            ComboMarital.SelectedIndex = (int)record.MaritalStatus;
            foreach (String str in medicalRecord.PatientAllergens.CommonAllergens)
            {
                ListPatientAllergens.Items.Add(str);
            }

            foreach (String str in medicalRecord.PatientAllergens.MedicalAllergens)
            {
                ListPatientMedicalAllergens.Items.Add(str);
            }

            TxtName.Text = patient.Name;
            TxtSurname.Text = patient.Surname;
            TxtBirth.Text = patient.BirthDate.ToString("yyyy-MM-dd");
            TxtEmail.Text = patient.Email;
            TxtJmbg.Text = patient.Jmbg;
            TxtPhone.Text = patient.PhoneNumber;*/
            //ListPatientAllergens.Items.Add(str);
            this.Loaded += new RoutedEventHandler(UpdateRecordWindow_Loaded);

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

        void UpdateRecordWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Check DataContext Property here - Value is not null
            MedicalRecord record = (MedicalRecord)DataContext;

            foreach (String str in record.PatientAllergens.CommonAllergens)
            {
                ListPatientAllergens.Items.Add(str);
            }

            foreach (String str in record.PatientAllergens.MedicalAllergens)
            {
                ListPatientMedicalAllergens.Items.Add(str);
            }

            foreach (String str in _medicalRecordController.ReadAllCommonAllergens())
            {
                if (!ListPatientAllergens.Items.OfType<String>().ToList().Contains(str))
                {
                    ListOtherAllergens.Items.Add(str);
                }
            }

            foreach (String str in _medicalRecordController.ReadAllMedicalAllergens())
            {
                if (!ListPatientMedicalAllergens.Items.OfType<String>().ToList().Contains(str))
                {
                    ListOtherMedicalAllergens.Items.Add(str);
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MedicalRecord record = (MedicalRecord)DataContext;
                Patient patientUpdated = new Patient(TxtName.Text, TxtSurname.Text, DateTime.Parse(TxtBirth.Text), TxtEmail.Text, TxtJmbg.Text, TxtPhone.Text);
                patientUpdated.Id = record.Patient.Id;
                //patientUpdated.Id = patient.Id;
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
                medicalRecordUpdated.Id = record.Id;
                _medicalRecordController.Update(medicalRecordUpdated, patientUpdated);
                System.Windows.MessageBox.Show("Medical record successfully updated!","Successfully updated!",MessageBoxButton.OK);
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