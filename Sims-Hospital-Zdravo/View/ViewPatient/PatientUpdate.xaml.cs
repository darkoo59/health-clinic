using Controller;
using Model;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
    /// Interaction logic for PatientUpdate.xaml
    /// </summary>
    public partial class PatientUpdate : Page
    {
        public AppointmentPatientController appointmentPatientController;
        public Appointment appointment;
        string pattern = @"\d?\d:\d\d";
        private DateTime dateTime;
        private Frame patient;
        private Timer timer;
        App app;
        public PatientUpdate(Frame frame, Appointment appointment)
        {
            this.patient = frame;
            InitializeComponent();
            app = Application.Current as App;
            appointmentPatientController = new AppointmentPatientController(app._accountRepository);
            this.DataContext = this;
            this.appointment = appointment;
            dateTime = appointment.Time.Start;
            if (appointment.Time.Start.Hour < 10)
            {
                Time.Text = "0" + appointment.Time.Start.Hour;
            }
            else
            {
                Time.Text = "" + appointment.Time.Start.Hour;
            }
            Time.Text = Time.Text + ":";
            if (appointment.Time.Start.Minute < 10)
            {
                Time.Text = Time.Text + "0" + appointment.Time.Start.Minute;
            }
            else
            {
                Time.Text = Time.Text + appointment.Time.Start.Minute;
            }
            DatePicker.SelectedDate = appointment.Time.Start;
            Doctor.Text = appointment.Doctor.Name + " " + appointment.Doctor.Surname;
            ValidateTime.Visibility = Visibility.Hidden;
            ValidateDate.Visibility = Visibility.Hidden;
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateTime = DatePicker.SelectedDate.Value;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                Match m = Regex.Match(Time.Text, pattern);
                if (m.Success)
                {
                    string[] time = Time.Text.Split(':');
                    date = date.AddHours(Int32.Parse(time[0]));
                    date = date.AddMinutes(Int32.Parse(time[1]));
                }
                else
                {
                    throw new Exception("Time format must be HH:mm (10:00)");
                }
                TimeInterval timeInterval = new TimeInterval(date, date.AddMinutes(30));
                appointmentPatientController.ValidateReshedule(appointment, timeInterval);
                appointment.Time = timeInterval;
                appointmentPatientController.Update(appointment);
                patient.Content = new PatientWindow(patient);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                if (ex.Message.Equals("You can not time travel") || ex.Message.Equals("You cant move appointment more or less than 2 days from original day"))
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
            patient.Content = new PatientWindow(patient);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<IDemoCommand> commands = new List<IDemoCommand>
            {
                new SelectDateCommand(DatePicker,new DateTime(2022,6,11)),
                new FillTextBoxCommand(Time,"10:30"),
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
                App.Current.Dispatcher.Invoke((Action)delegate { patient.Content = new PatientWindow(patient); });
            }
        }
    }
}
