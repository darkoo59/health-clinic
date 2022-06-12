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
using System.Collections.ObjectModel;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for OperationForm.xaml
    /// </summary>
    public partial class OperationForm : Page
    {
        private DoctorAppointmentController doctorAppointmentController;
        private AppointmentPatientController patientController;
        private RoomController roomController;
        private int doctorID;
        private App app;

        public OperationForm(DoctorAppointmentController doctorAppointmentController)
        {
            InitializeComponent();
            this.doctorAppointmentController = doctorAppointmentController;
            this.app = App.Current as App;
            this.roomController = new RoomController();
            this.patientController = new AppointmentPatientController(app._accountRepository);
            SpecilatyComboBox.ItemsSource = Enum.GetValues(typeof(SpecialtyType)).Cast<SpecialtyType>();
        }

        List<Doctor> FillDoctorComboBox()
        {
            if (SpecilatyComboBox.SelectedValue != null)
                return doctorAppointmentController.FindDoctorsBySpecalty((SpecialtyType)SpecilatyComboBox.SelectedValue);
            else
                throw new Exception("You must choose sepcilaty first");
        }

        private void SpecilatyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DoctorComboBox.ItemsSource = FillDoctorComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void getDoctorID()
        {
            if (doctor != null)
            {
                doctorID = doctor.Id;
            }
            else
            {
                throw new Exception("choose doctor");
            }
        }

        private Doctor doctor;

        public Doctor _Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }

        private void DoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = DoctorComboBox.SelectedItem.ToString();
            string[] names = name.Split(' ');
            foreach (Doctor d in this.patientController.ReadDoctors())
            {
                if (d.Name.Equals(names[0]) && d.Surname.Equals(names[1]))
                {
                    _Doctor = d;
                    Console.WriteLine(_Doctor.Name);
                    break;
                }
            }
        }

        private Patient patient1;

        public Patient Patient1
        {
            get { return patient1; }
            set { patient1 = value; }
        }

        public Patient FindPatient()
        {
            string patient = patientTxt.Text;
            string[] patientFull = patient.Split(' ');
            foreach (Patient pat in this.doctorAppointmentController.GetPatients())
            {
                if (pat.Name.Equals(patientFull[0]) && pat.Surname.Equals(patientFull[1]))
                {
                    Patient1 = pat;
                    //break;
                }
            }

            return Patient1;
        }


        private void ButtonOperation_Click(object sender, RoutedEventArgs e)
        {
            string dateAppoitment = dateTxt.Text;
            string timeOfOperation = TimeTxt.Text;
            double duration = double.Parse(durationTxt.Text);


            var dt_start = DateTime.Parse(dateAppoitment + " " + timeOfOperation);
            var dt_end = dt_start.AddHours(duration);
            // var dt_end = DateTime.Parse(dateAppoitment + " " + endTimeOfOperation);
            TimeInterval timeInterval = new TimeInterval(dt_start, dt_end);
            int numOfRoom = Int32.Parse(RoomTxt.Text);
            Room room = roomController.FindById(numOfRoom);
            Console.WriteLine(room.Id + "hahahahahahahahaha");
            int doctorId = _Doctor.Id;
            Patient pat = FindPatient();
            AppointmentType appointmentType = AppointmentType.OPERATION;
            Appointment appointment = new Appointment(room, _Doctor, pat, timeInterval, appointmentType);
            doctorAppointmentController.UrgentSurgery(appointment, duration);
        }
    }
}