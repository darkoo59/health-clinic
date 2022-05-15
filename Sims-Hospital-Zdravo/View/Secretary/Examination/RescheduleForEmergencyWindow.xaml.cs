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
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for RescheduleForEmergencyWindow.xaml
    /// </summary>
    public partial class RescheduleForEmergencyWindow : Window
    {
        private App app;
        private Patient patient;
        private SpecialtyType doctorType;
        public RescheduleForEmergencyWindow(Patient patient,SpecialtyType type)
        {
            app = Application.Current as App;
            InitializeComponent();
            this.patient = patient;
            this.doctorType = type;
            this.DataContext = this;
            UpdateGridView();
            ContentGrid.ItemsSource =
                app._secretaryAppointmentController.FindAllAppointmentsToRescheduleForEmergency(type);
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_profile.Visibility = Visibility.Collapsed;
                tt_about.Visibility = Visibility.Collapsed;
                tt_meetings.Visibility = Visibility.Collapsed;
                tt_accounts.Visibility = Visibility.Collapsed;
                tt_equipment.Visibility = Visibility.Collapsed;
                tt_appointments.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_medical_records.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_sign_out.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_profile.Visibility = Visibility.Visible;
                tt_about.Visibility = Visibility.Visible;
                tt_meetings.Visibility = Visibility.Visible;
                tt_accounts.Visibility = Visibility.Visible;
                tt_equipment.Visibility = Visibility.Visible;
                tt_appointments.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_medical_records.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_sign_out.Visibility = Visibility.Visible;
            }
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
                            emergencyReschedule.Appointment._Time.Start, emergencyReschedule.Appointment._Time.End);
                        rescheduleAppointment._Time.Start = emergencyReschedule.RescheduledDate.Start;
                        rescheduleAppointment._Time.End = emergencyReschedule.RescheduledDate.End;
                        app._secretaryAppointmentController.Update(rescheduleAppointment);
                        Appointment emergencyAppointment = new Appointment(emergencyReschedule.Appointment._Room,
                            emergencyReschedule.Appointment._Doctor, patient, emergencyTimeInterval,
                            AppointmentType.URGENCY);
                        app._secretaryAppointmentController.Create(emergencyAppointment);
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
            dataColumn.Header = "Start";
            dataColumn.Binding = new Binding("Appointment._Time.Start");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "End";
            dataColumn.Binding = new Binding("Appointment._Time.End");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "Start after rescheduling";
            dataColumn.Binding = new Binding("RescheduledDate.Start");
            ContentGrid.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            dataColumn.Header = "End after rescheduling";
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

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryHome window = new SecretaryHome();
            window.Show();
            this.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            ExaminationWindow window = new ExaminationWindow(app._secretaryAppointmentController);
            window.Show();
            this.Close();
        }
        
        private void MedicalRecord_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryWindow window = new SecretaryWindow(app._recordController);
            window.Show();
            this.Close();
        }
        
        private void Equipment_Click(object sender, MouseButtonEventArgs e)
        {
            SuppliesHome window = new SuppliesHome();
            window.Show();
            this.Close();
        }
    }
}
