using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Repository;
using Sims_Hospital_Zdravo.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
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
    public partial class PatientCreate : Page, INotifyPropertyChanged
    {
        private AppointmentPatientController appointmentPatientController;
        private PatientMedicalRecordController patientMedicalRecordController;
        private AccountController accountController;
        private ObservableCollection<string> doctors;
        private ObservableCollection<string> doctorordate;
        private string pattern = @"\d?\d:\d\d";
        private Frame frame;
        private Timer timer;
        App app;
        public PatientCreate(Frame frame)
        {
            this.frame = frame;
            app = Application.Current as App;
            this.accountController = app._accountController;
            InitializeComponent();
            this.DataContext = this;
            this.appointmentPatientController = new AppointmentPatientController(app._accountRepository);
            patientMedicalRecordController = new PatientMedicalRecordController();
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
            ValidateDate.Visibility = Visibility.Hidden;
            ValidateTime.Visibility = Visibility.Hidden;
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
                DateTime date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                if (m.Success)
                {
                    string[] time = TimeText.Text.Split(':');
                    date = date.AddHours(Int32.Parse(time[0]));
                    date = date.AddMinutes(Int32.Parse(time[1]));
                }
                else
                {
                    throw new Exception("Time format must be HH:mm (10:00)");                
                }
                if (datePicker.SelectedDate == null)
                {
                    throw new Exception("You must pick a date");
                }
                Patient patient = patientMedicalRecordController.FindPatientById(accountController.GetLoggedAccount().Id);
                TimeInterval timeInterval = new TimeInterval(date, date.AddMinutes(30));
                Appointment appointment = new Appointment(null, doctor, patient, timeInterval, AppointmentType.EXAMINATION);
                string priority = DateOrDoctors.SelectedItem.ToString();
                appointmentPatientController.ValidateAppointment(appointment);
                frame.Content = new AppointmentList(frame, appointment, priority);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                if (ex.Message.Equals("You must pick a date") || ex.Message.Equals("You can not time travel"))
                {
                    ValidateDate.Visibility = Visibility.Visible;
                    ValidateTime.Visibility = Visibility.Hidden;
                    ValidateDate.Text = ex.Message;
                }
                else
                {
                    ValidateDate.Visibility = Visibility.Hidden;
                    ValidateTime.Visibility = Visibility.Visible;
                    ValidateTime.Text = ex.Message;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<IDemoCommand> commands = new List<IDemoCommand>
            {
                new SelectComboBoxCommand(Doctors,"Grigorije Kucanski"),
                new SelectDateCommand(datePicker,new DateTime(2022,6,19)),
                new FillTextBoxCommand(TimeText,"10:30"),
                new SelectComboBoxCommand(DateOrDoctors, "Date")
            };
            timer = new Timer(1000);
            timer.Elapsed += (Object source, ElapsedEventArgs e1) => TimerCallback(commands);
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private void TimerCallback(List<IDemoCommand> commands)
        {
            if (commands.Count > 0)
            {
                commands[0].Execute();
                commands.RemoveAt(0);
            }
            else
            {
                timer.Stop();
                Patient patient = patientMedicalRecordController.FindPatientById(accountController.GetLoggedAccount().Id);
                TimeInterval timeInterval = new TimeInterval(dateTime, dateTime.AddMinutes(30));
                Appointment appointment = new Appointment(null, doctor, patient, timeInterval, AppointmentType.EXAMINATION);
                App.Current.Dispatcher.Invoke((Action)delegate { AppointmentList appointmentList = new AppointmentList(frame, appointment, "Date"); frame.Content = appointmentList; appointmentList.PlayDemo(); });
            }
        }
    }
}
