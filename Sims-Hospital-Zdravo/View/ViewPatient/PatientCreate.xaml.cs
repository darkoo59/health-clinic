using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    public class StringToTime : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                string pattern = @"\d?\d:\d\d";
                var s = value as string;
                Match m = Regex.Match(s, pattern);
                if (m.Success)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Time format must be HH:mm (10:00)");                
                }
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PatientCreate : Page, INotifyPropertyChanged
    {
        public AppointmentPatientController appointmentPatientController;
        public AccountController accountController;
        public ObservableCollection<string> doctors;
        public ObservableCollection<string> doctorordate;
        string pattern = @"\d?\d:\d\d";
        private Frame frame;
        App app;
        public PatientCreate(Frame frame)
        {
            this.frame = frame;
            app = Application.Current as App;
            this.accountController = app._accountController;
            InitializeComponent();
            this.DataContext = this;
            this.appointmentPatientController = app._appointmentPatientController;
            doctors = new ObservableCollection<string>();
            doctorordate = new ObservableCollection<string>();
            doctorordate.Add("Doctor");
            doctorordate.Add("Date");
            DateOrDoctors.ItemsSource = doctorordate;
            foreach (Doctor doctor in this.appointmentPatientController.ReadDoctors())
            {
                doctors.Add(doctor.Name + " " + doctor.Surname);
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
        private string _time;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string Time
        {
            get 
            {
                return _time;
            }
            set 
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Doctors_Selected(object sender, RoutedEventArgs e)
        {
            string name = Doctors.SelectedItem.ToString();
            string[] names = name.Split(' ');
            foreach (Doctor d in this.appointmentPatientController.ReadDoctors())
            {
                if (d.Name.Equals(names[0]) && d.Surname.Equals(names[1]))
                {
                    doctor = d;
                    break;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Match m = Regex.Match(TimeText.Text, pattern);
                if (m.Success)
                {
                    string[] time = TimeText.Text.Split(':');
                    dateTime = dateTime.AddHours(Int32.Parse(time[0]));
                    dateTime = dateTime.AddMinutes(Int32.Parse(time[1]));
                }
                else
                {
                    //throw new Exception("Time format must be HH:mm (10:00)");                
                }
                if (datePicker.SelectedDate == null)
                {
                    //throw new Exception("You must pick a date");
                }
                Patient patient = (Patient)accountController.GetLoggedAccount();
                TimeInterval timeInterval = new TimeInterval(dateTime, dateTime.AddMinutes(30));
                Appointment appointment = new Appointment(null, doctor, patient, timeInterval, AppointmentType.EXAMINATION);
                string priority = DateOrDoctors.SelectedItem.ToString();
                appointmentPatientController.ValidateAppointment(appointment);
                frame.Content = new AppointmentList(frame, appointment, priority);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
