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
using Model;
using Controller;
using System.Collections.ObjectModel;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for MedicalReport.xaml
    /// </summary>
    public partial class MedicalReport : Page
    {
        private AnamnesisController anamnesisController;
        private DoctorAppointmentController doctorAppointmentController;
        private ObservableCollection<string> patients;
        private PatientMedicalRecordController patientMedicalRecordController;
        private int DoctorId;
        private DateTime date;
        private Frame frame;
        public MedicalReport(AnamnesisController anamnesisController,DoctorAppointmentController doctorAppointmentController,PatientMedicalRecordController patientMedicalRecordController,int id,Frame frame)
        {
            InitializeComponent();
            this.DoctorId = id;
            this.anamnesisController = anamnesisController;
            this.frame = frame;
            this.doctorAppointmentController = doctorAppointmentController;
            this.patientMedicalRecordController = patientMedicalRecordController;
            DoctorTxt.Text = doctorAppointmentController.getDoctor(DoctorId)._Name + doctorAppointmentController.getDoctor(DoctorId)._Surname;
            patients = new ObservableCollection<string>();
            //ExaminatonTxt.Text = DateTime.Now.ToLongDateString();

            foreach (Patient pat in this.doctorAppointmentController.getPatients())
            {
                patients.Add(pat._Name + " " + pat._Surname + " " + pat._BirthDate.ToString());
            }

            cbPatients.ItemsSource = patients;



        }
        private Patient pat;

        public Patient Pat
        {
            get { return pat; }
            set { pat = value; }
        }

        private void cbPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string name = cbPatients.SelectedItem.ToString();
            string[] names = name.Split(' ');
            Console.WriteLine(names[2] + "hahah");
            foreach (Patient pat in this.doctorAppointmentController.getPatients())
            {
                if (pat._Name.Equals(names[0]) && pat._Surname.Equals(names[1]) && pat._BirthDate.Equals(DateTime.Parse(names[2]+ " "+names[3])))

                {
                    Pat = pat;
                    break;
                }

            }

        }
        private void dateExamination_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
             date = DateTime.Parse(dateExamination.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DateTime date = DateTime.Now;
            string diagnosis = DiagnosisTxt.Text;
            string medical_report = AnamnesisTxt.Text;
            Doctor doctor = doctorAppointmentController.getDoctor(DoctorId);
            MedicalRecord med = patientMedicalRecordController.findMedicalRecordByPatient(Pat);
            Anamnesis anamnesis = new Anamnesis(doctor, med, date, null, diagnosis, medical_report);
            anamnesisController.Create(anamnesis);
            doctorAppointmentController.DeleteAfterExaminationIsDone(date, DoctorId, Pat);
            AnamnesisList anamnesisList = new AnamnesisList(anamnesisController, DoctorId, doctorAppointmentController);
            //Close();
            anamnesisList.Show();


        }

        
    }
}
