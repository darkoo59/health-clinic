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
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Controller;
using Model;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for EditAnamnesis.xaml
    /// </summary>
    public partial class EditAnamnesis : Window, IUpdateFilesObservable
    {
        private AnamnesisController anamnesisController;
        private DoctorAppointmentController doctorAppointmentController;
        private MedicalRecord medicalRecord;
        private App app;
        private PatientMedicalRecordController patientMedicalRecordController;
        private int DoctorId;
        private Anamnesis anamnesis;
        private List<IUpdateFilesObserver> observers;
        public EditAnamnesis( Anamnesis anamnesis, MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.app = App.Current as App;
            this.anamnesisController = app._anamnesisController;
            this.anamnesis = anamnesis;
            this.medicalRecord = medicalRecord;
            observers = new List<IUpdateFilesObserver>();
            this.doctorAppointmentController = app._doctorAppointmentController;
           
            DoctorTxt.Text = anamnesis.Doctor._Name + anamnesis.Doctor._Surname;
            Editanam.Text = DateTime.Now.ToString();
            DiagnosisTxt.Text = anamnesis.Diagnosis;
            AnamnesisTxt.Text = anamnesis.Anamensis;
            DoctorId = anamnesis.Doctor._Id;
            DateTime date = anamnesis.Date;
            //Patient pat = anamnesis._MedicalRecord._Patient;
            ExaminatonTxt.Text = DateTime.Now.Date.ToString();


        }
        private void CreateColumn()
        {
            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            Button editButton = new Button();
            templateColumn.CellTemplate.DataType = editButton;
        }

        public void AddObserver(IUpdateFilesObserver observer)
        {
            observers = new List<IUpdateFilesObserver>();
        }

        public void NotifyUpdated()
        {
            foreach (IUpdateFilesObserver observer in observers)
            {
                observer.NotifyUpdated();
            }
        }

        public void RemoveObserver(IUpdateFilesObserver observer)
        {
            observers.Remove(observer);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Parse(Editanam.Text);
            string diagnosis = DiagnosisTxt.Text;
            string medical_report = AnamnesisTxt.Text;
            Doctor doctor = anamnesis.Doctor;
            //MedicalRecord med = anamnesis._MedicalRecord;
            //TimeInterval tl = 
            Anamnesis anam = new Anamnesis(doctor,medicalRecord.Patient, date, null, diagnosis, medical_report);
            anamnesisController.Update(anam);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
