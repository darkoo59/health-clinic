using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PatientCreate : Window
    {
        /*public PatientCreate()
        {
            InitializeComponent();
        }*/
        public AppointmentPatientController appointmentPatientController;
        public ObservableCollection<string> doctors;
        public ObservableCollection<string> doctorordate;

        public PatientCreate(AppointmentPatientController appointmentPatientController)
        {
            InitializeComponent();
            this.DataContext = this;
            this.appointmentPatientController = appointmentPatientController;
            doctors = new ObservableCollection<string>();
            doctorordate = new ObservableCollection<string>();
            doctorordate.Add("Doctor");
            doctorordate.Add("Date");
            DateOrDoctors.ItemsSource = doctorordate;
            foreach (Doctor doctor in this.appointmentPatientController.ReadDoctors())
            {
                doctors.Add(doctor._Name + " " + doctor._Surname);
            }
            Doctors.ItemsSource = doctors;
        }
        public DateTime dateTime;
        private DateTime _DateTime { get; set; }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _DateTime = datePicker.SelectedDate.Value;
        }
        public Doctor doctor;
        private Doctor _Doctor { get; set; }
        private void Doctors_Selected(object sender, RoutedEventArgs e)
        {
            string name = Doctors.SelectedItem.ToString();
            string[] names = name.Split(' ');
            foreach (Doctor doctor in this.appointmentPatientController.ReadDoctors())
            {
                if (doctor._Name.Equals(names[0]) && doctor._Surname.Equals(names[1]))
                {
                    _Doctor = doctor;
                    break;
                }
            }
        }
        Random rnd = new Random();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] time = Time.Text.Split(':');
            _DateTime = _DateTime.AddHours(Int32.Parse(time[0]));
            _DateTime = _DateTime.AddMinutes(Int32.Parse(time[1]));
            DateTime dateTime = new DateTime(1111,11,11);
            Patient patient = new Patient(1, "Jovan", "Nikic", dateTime, "fdafdasf@gmail.com", "321341413", "+38134213");
            Appointment appointment = new Appointment(new Room(),_Doctor, patient, _DateTime, rnd.Next());
            appointmentPatientController.Create(appointment);
            Close();
        }
    }
}
