using Controller;
using Model;
using Sims_Hospital_Zdravo.Model;
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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for PrescriptionParameters.xaml
    /// </summary>
    public partial class PrescriptionParameters : Page
    {
        public DateTime dateTime;
        public Prescription prescription;
        public App app;
        public MedicalRecordController medicalRecordController;
        public Appointment appointment;
        public PrescriptionParameters(Prescription prescription, Appointment appointment)
        {
            app = Application.Current as App;
            medicalRecordController = app._recordController;
            this.appointment = appointment;
            InitializeComponent();
            this.prescription = prescription;
            if (prescription.StartDate != null) 
            {
                Date.SelectedDate = prescription.StartDate;
                Time.Text = prescription.StartDate.ToString("{0:HH:mm}");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = (DateTime)Date.SelectedDate;
            string[] time = Time.Text.Split(':');
            dateTime = dateTime.AddHours(Int32.Parse(time[0]));
            dateTime = dateTime.AddMinutes(Int32.Parse(time[1]));
            if (prescription.StartDate == null)
            {
                medicalRecordController.AddStartDate(dateTime, prescription, appointment);
            }
            else if(prescription.StartDate.CompareTo(DateTime.Now) < 0)
            {
                medicalRecordController.AddStartDate(dateTime, prescription, appointment);
            }
        }
    }
}
