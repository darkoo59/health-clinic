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
using Sims_Hospital_Zdravo.Model;
using Model;
using Controller;
using System.Collections.ObjectModel;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for ScheduleAppointmentSpecialist.xaml
    /// </summary>
    public partial class ScheduleAppointmentSpecialist : Page
    {
        private DoctorAppointmentController doctorAppointmentController;
        private AppointmentPatientController patientController;
        private RoomController roomController;
        private App app;
        ObservableCollection<string> patients;
        ObservableCollection<string> doctors;
        ObservableCollection<string> specialties;

        public ScheduleAppointmentSpecialist(DoctorAppointmentController doctorAppointmentController)
        {
            InitializeComponent();
            this.doctorAppointmentController = doctorAppointmentController;


            app = App.Current as App;
            patientController = app._appointmentPatientController;
            this.roomController = new RoomController();
            var startTime = DateTime.Parse("8:00");
            var endTime = DateTime.Parse("21:00");
            TypeOfAppointment.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
            List<string> time_list = new List<string>();

            while (startTime < endTime)
            {
                time_list.Add(startTime.ToShortTimeString() + " - " + startTime.AddMinutes(30).ToShortTimeString());
                startTime = startTime.AddMinutes(30);
            }

            SpecialistAppointmentTimeComboBox.ItemsSource = time_list;
            SpecialistComboBox.ItemsSource = Enum.GetValues(typeof(SpecialtyType)).Cast<SpecialtyType>();
        }


        List<Doctor> FillDoctorComboBox()
        {
            if (SpecialistComboBox.SelectedValue != null)
                return doctorAppointmentController.FindDoctorsBySpecalty((SpecialtyType)SpecialistComboBox.SelectedValue);
            else
                throw new Exception("You must choose sepcilaty first");
        }

        private void SpecialistComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            ComboBoxItem ChosenDoctor = DoctorComboBox.SelectedItem as ComboBoxItem;
        }

        private Patient patient1;

        public Patient _Patient1
        {
            get { return patient1; }
            set { patient1 = value; }
        }

        public Patient FindPatient()
        {
            string patient = PatientTxt.Text;
            Console.WriteLine("haahahsahahhjs" + patient);
            string[] patientFull = patient.Split(' ');
            foreach (Patient pat in this.doctorAppointmentController.GetPatients())
            {
                if (pat.Name.Equals(patientFull[0]) && pat.Surname.Equals(patientFull[1]))
                {
                    _Patient1 = pat;
                }
            }

            return _Patient1;
        }

        private void SpecialistButton_Click(object sender, RoutedEventArgs e)
        {
            string dateAppoitment = dateTxt.Text;
            string timeAppointment = SpecialistAppointmentTimeComboBox.SelectedItem.ToString();
            Console.WriteLine();

            string[] times = timeAppointment.Split('-');
            var dt_start = DateTime.Parse(dateAppoitment + " " + times[0]);
            var dt_end = DateTime.Parse(dateAppoitment + " " + times[1]);
            TimeInterval timeInterval = new TimeInterval(dt_start, dt_end);
            int numOfRoom = Int32.Parse(RoomText.Text);
            Room room = this.roomController.FindById(numOfRoom);
            Patient pat = FindPatient();
            int doctorId = doctor.Id;
            Appointment appointment = new Appointment(room, doctor, pat, timeInterval, (AppointmentType)TypeOfAppointment.SelectedValue);
            doctorAppointmentController.Create(appointment);
        }

        public Doctor doctor;

        private void DoctorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = DoctorComboBox.SelectedItem.ToString();
            string[] names = name.Split(' ');
            foreach (Doctor d in this.patientController.ReadDoctors())
            {
                if (d.Name.Equals(names[0]) && d.Surname.Equals(names[1]))
                {
                    doctor = d;
                    break;
                }
            }
        }
    }
}