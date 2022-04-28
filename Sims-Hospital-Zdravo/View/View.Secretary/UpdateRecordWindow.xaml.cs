﻿using Controller;
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
using Sims_Hospital_Zdravo.View.View.Secretary;

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
            AllergensList.ItemsSource = medicalRecord._Allergens;
            TxtName.Text = patient._Name;
            TxtSurname.Text = patient._Surname;
            TxtBirth.Text = patient._BirthDate.ToString("yyyy-MM-dd");
            TxtEmail.Text = patient._Email;
            TxtJmbg.Text = patient._Jmbg;
            TxtPhone.Text = patient._PhoneNumber;
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
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void AddAllergens_Click(object sender, RoutedEventArgs e)
        {
            List<String> nonAllergicList = medicalController.ReadAllAllergens();
            foreach(String str in medicalRecord._Allergens){
                nonAllergicList.Remove(str);
            }
            AddAllergensWindow addWindow = new AddAllergensWindow(AllergensList,nonAllergicList);
            addWindow.Show();
        }

        private void RemoveAllergens_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
