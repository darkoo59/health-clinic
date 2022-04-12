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
        public UpdateRecordWindow(MedicalRecordController controller,int patientId)
        {
            InitializeComponent();
            medicalController = controller;
            ComboGender.ItemsSource = Enum.GetValues(typeof(GenderType)).Cast<GenderType>();
            ComboBlood.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            ComboMarital.ItemsSource = Enum.GetValues(typeof(MaritalType)).Cast<MaritalType>();
            Patient patient = medicalController.findPatientById(patientId);
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
                MedicalRecord medicalRecord = new MedicalRecord(Int32.Parse(TxtMedicalId.Text), Int32.Parse(TxtPatientId.Text), (GenderType)ComboGender.SelectedValue, (BloodType)ComboBlood.SelectedValue, (MaritalType)ComboMarital.SelectedValue);
                Patient patient = new Patient(Int32.Parse(TxtPatientId.Text), TxtName.Text, TxtSurname.Text, DateTime.Parse(TxtBirth.Text), TxtEmail.Text, TxtJmbg.Text, TxtPhone.Text);
                medicalController.Update(medicalRecord, patient);
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /*try
            {
                Room room = new Room(Int32.Parse(FloorTxt.Text), Int32.Parse(RoomIdTxt.Text), (RoomType)RoomTypeCmb.SelectedValue);
                roomController.Update(room);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         */
    }
}
