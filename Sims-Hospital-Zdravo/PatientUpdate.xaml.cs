using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PatientUpdate.xaml
    /// </summary>
    public partial class PatientUpdate : Window
    {
        public AppointmentPatientController appointmentPatientController;
        public ObservableCollection<string> doctors;
        public ObservableCollection<string> doctorordate;
        public PatientUpdate(AppointmentPatientController appointmentPatientController,Appointment appointment)
        {
            InitializeComponent();
            this.DataContext = this;
            this.appointmentPatientController = appointmentPatientController;
            _DateTime = _DateTime.AddYears(appointment._DateAndTime.Year);
            _DateTime = _DateTime.AddDays(appointment._DateAndTime.Day);
            _DateTime = _DateTime.AddMonths(appointment._DateAndTime.Month);
            if (appointment._DateAndTime.Hour < 10)
            {
                Time.Text ="0" + appointment._DateAndTime.Hour;
            }
            else
            {
                Time.Text ="" +appointment._DateAndTime.Hour;
            }
            Time.Text = appointment._DateAndTime.Hour + ":";
            if (appointment._DateAndTime.Minute < 10)
            {
                Time.Text = Time.Text + "0" + appointment._DateAndTime.Minute;
            }
            else
            {
                Time.Text = Time.Text + appointment._DateAndTime.Minute;
            }
            doctors = new ObservableCollection<string>();
            doctorordate = new ObservableCollection<string>();
            doctorordate.Add("Doctor");
            doctorordate.Add("Date");
            _Id = appointment._Id;
            DateOrDoctors.ItemsSource = doctorordate;
            foreach (Doctor doctor in this.appointmentPatientController.ReadDoctors())
            {
                doctors.Add(doctor._Name + " " + doctor._Surname);
            }
            Doctors.ItemsSource = doctors;
            foreach (Doctor doctor in appointmentPatientController.ReadDoctors()) 
            {
                if (doctor._Id == appointment._Doctor._Id)
                {
                    _Doctor = doctor;
                    Doctors.SelectedItem = doctor._Name + " " + doctor._Surname; 
                }
            }
        }
        public DateTime dateTime;
        public int Id;
        private int _Id { get; set; }
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] time = Time.Text.Split(':');
            _DateTime = _DateTime.AddHours(Int32.Parse(time[0]));
            _DateTime = _DateTime.AddMinutes(Int32.Parse(time[1]));
            Appointment appointment = new  Appointment(_Id, 100, _DateTime, 1, _Doctor._Id);
            appointmentPatientController.Update(appointment);
            Close();
        }
    }
}
