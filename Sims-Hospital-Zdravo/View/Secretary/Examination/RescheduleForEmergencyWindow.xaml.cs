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
using System.Windows.Shapes;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for RescheduleForEmergencyWindow.xaml
    /// </summary>
    public partial class RescheduleForEmergencyWindow : Window
    {
        private Patient patient;
        private SpecialtyType doctorType;
        private SecretaryAppointmentController _secretaryAppointmentController;
        private App app;
        public RescheduleForEmergencyWindow(Patient patient,SpecialtyType type)
        {
            InitializeComponent();
            app = Application.Current as App;
            this.patient = patient;
            this.doctorType = type;
            this.DataContext = this;
            _secretaryAppointmentController = new SecretaryAppointmentController();
            UpdateGridView();
            ContentGrid.ItemsSource =
                _secretaryAppointmentController.FindAllAppointmentsToRescheduleForEmergency(type);
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (ContentGrid.SelectedItem != null)
                {
                    try
                    {
                        EmergencyReschedule emergencyReschedule = (EmergencyReschedule)ContentGrid.SelectedValue;
                        Appointment rescheduleAppointment = emergencyReschedule.Appointment;
                        TimeInterval emergencyTimeInterval = new TimeInterval(
                            emergencyReschedule.Appointment.Time.Start, emergencyReschedule.Appointment.Time.End);
                        rescheduleAppointment.Time.Start = emergencyReschedule.RescheduledDate.Start;
                        rescheduleAppointment.Time.End = emergencyReschedule.RescheduledDate.End;
                        _secretaryAppointmentController.Update(rescheduleAppointment);
                        Appointment emergencyAppointment = new Appointment(emergencyReschedule.Appointment.Room,
                            emergencyReschedule.Appointment.Doctor, patient, emergencyTimeInterval,
                            AppointmentType.URGENCY);
                        _secretaryAppointmentController.Create(emergencyAppointment);
                        MessageBox.Show("Emergency appointment successfully created!", "Appointment created",
                            MessageBoxButton.OK);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            else
            {
                MessageBox.Show("Please select appointment to reschedule!", "Select appointment",
                    MessageBoxButton.OK);
            }
        }

            private void UpdateGridView()
        {
            ContentGrid.AutoGenerateColumns = false;
            ContentGrid.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Start";
            else
                dataColumn.Header = "Početak";
            dataColumn.Binding = new Binding("Appointment._Time.Start");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "End";
            else 
                dataColumn.Header = "Kraj";
            dataColumn.Binding = new Binding("Appointment._Time.End");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Start after rescheduling";
            else
                dataColumn.Header = "Početak nakon pomeranja";
            dataColumn.Binding = new Binding("RescheduledDate.Start");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "End after rescheduling";
            else 
                dataColumn.Header = "Kraj nakon pomeranja";
            dataColumn.Binding = new Binding("RescheduledDate.End");
            ContentGrid.Columns.Add(dataColumn);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
