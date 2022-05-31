using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PatientUpdate.xaml
    /// </summary>
    public partial class PatientUpdate : Page
    {
        public AppointmentPatientController appointmentPatientController;
        public Appointment appointment;
        string pattern = @"\d?\d:\d\d";
        private DateTime dateTime;
        private Frame patient;
        App app;
        public PatientUpdate(Frame frame, Appointment appointment)
        {
            this.patient = frame;
            InitializeComponent();
            app = Application.Current as App;
            this.DataContext = this;
            this.appointmentPatientController = app._appointmentPatientController;
            this.appointment = appointment;
            dateTime = appointment.Time.Start;
            if (appointment.Time.Start.Hour < 10)
            {
                Time.Text ="0" + appointment.Time.Start.Hour;
            }
            else
            {
                Time.Text ="" +appointment.Time.Start.Hour;
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
            datePicker.SelectedDate = appointment.Time.Start;
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateTime = datePicker.SelectedDate.Value;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            try
            {
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
                MessageBox.Show(ex.Message);
                if (ex.Message.Equals("You are blocked"))
                {
                    
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            patient.Content = new PatientWindow(patient);
        }
    }
}
