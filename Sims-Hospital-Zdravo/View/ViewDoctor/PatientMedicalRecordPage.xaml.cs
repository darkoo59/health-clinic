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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.Controller;
using Controller;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecordPage.xaml
    /// </summary>
    public partial class PatientMedicalRecordPage : UserControl
    {
        private DoctorAppointmentController _doctorAppointmentController;
        private MedicalRecordController _medicalRecordController;
        private MedicalRecord medRecord;
        private Appointment app;
        private AnamnesisController anamnesisController;
        private int doctorId;
        private Frame frame;
        public PatientMedicalRecordPage(MedicalRecord med, Frame frame)
        {
            InitializeComponent();
            this.medRecord = med;
            //this.app = app;
            //this.doctorId = id;
            
            //this._medicalRecordController = medicalRecordController;
            this.DataContext = med;
            //this._doctorAppointmentController = doctorAppointmentController;
           // this.anamnesisController = anamnesisController;
            Binding binding = new Binding("_Patient._Name");
            binding.Source = med;
            PatienNameTxt.SetBinding(TextBox.TextProperty, binding);

            binding = new Binding("_Patient._Surname");
            PatientSurnameTxt.SetBinding(TextBox.TextProperty, binding);

            binding = new Binding("_Patient._BirthDate");
            BirthDateTxt.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding("_Gender");
            Gendertxt.SetBinding(TextBox.TextProperty, binding);

            binding = new Binding("_Patient._PhoneNumber");
            numberTxt.SetBinding(TextBox.TextProperty, binding);

            binding = new Binding("_Patient._Address");
            //AdressTxt.SetBinding(TextBox.TextProperty, binding);

            binding = new Binding("_MaritalStatus");
            maritalStatusTxt.SetBinding(TextBox.TextProperty, binding);

            binding = new Binding("_Patient._Jmbg");
           UIDTxt.SetBinding(TextBox.TextProperty, binding);
        }
    }
}
