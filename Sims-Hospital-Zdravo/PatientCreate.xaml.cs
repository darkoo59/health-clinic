using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public AppointmentPatientController appointmentPatientController;
        public ObservableCollection<string> doctors;
        public ObservableCollection<string> doctorordate;
        string pattern = @"\d?\d:\d\d";

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
            Doctors.SelectedIndex = 1;
            DateOrDoctors.SelectedItem = "Date";
        }
        public DateTime dateTime;
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateTime = datePicker.SelectedDate.Value;
        }
        public Doctor doctor;
        private void Doctors_Selected(object sender, RoutedEventArgs e)
        {
            string name = Doctors.SelectedItem.ToString();
            string[] names = name.Split(' ');
            foreach (Doctor d in this.appointmentPatientController.ReadDoctors())
            {
                if (d._Name.Equals(names[0]) && d._Surname.Equals(names[1]))
                {
                    doctor = d;
                    break;
                }
            }
        }
        Random rnd = new Random();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Match m = Regex.Match(Time.Text, pattern);
                if (m.Success)
                {
                    string[] time = Time.Text.Split(':');
                    dateTime = dateTime.AddHours(Int32.Parse(time[0]));
                    dateTime = dateTime.AddMinutes(Int32.Parse(time[1]));
                }else
                {
                    throw new Exception("Time format must be HH:mm (10:00)");                
                }
                if (datePicker.SelectedDate == null) 
                {
                    throw new Exception("You must pick a date");
                }
                DateTime dateTime1 = new DateTime(1111, 11, 11);
                Patient patient = new Patient(1, "Jovan", "Nikic", dateTime1, "fdafdasf@gmail.com", "321341413", "+38134213");
                TimeInterval timeInterval = new TimeInterval(dateTime, dateTime.AddMinutes(30));
                Appointment appointment = new Appointment(null, doctor, patient, timeInterval, AppointmentType.EXAMINATION);
                string priority = DateOrDoctors.SelectedItem.ToString();
                appointmentPatientController.ValidateAppointment(appointment);
                AppointmentList al = new AppointmentList(appointmentPatientController, appointment, priority);
                al.Show();
                Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
