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
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils.PDF;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for ExaminationWindow.xaml
    /// </summary>
    public partial class ExaminationWindow : Window, IUpdateFilesObserver
    {
        private SecretaryAppointmentController _secretaryAppointmentController;
        public List<Appointment> appointments;
        private App app;
        public ExaminationWindow()
        {
            app = Application.Current as App;
            InitializeComponent();
            this.DataContext = this;
            this._secretaryAppointmentController = new SecretaryAppointmentController();
            appointmentsDatePicker.SelectedDate = DateTime.Today;
            UpdateGridView();

            appointments = _secretaryAppointmentController.ReadAllAppointmentsForDate(DateTime.Parse(appointmentsDatePicker.SelectedDate.ToString()));
            GridAppointments.ItemsSource = appointments;
        }

        private void appDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            GridAppointments.ItemsSource = null;
            NotifyUpdated();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to cancel this appointment?", "Cancel appointment", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    _secretaryAppointmentController.Delete((Appointment)GridAppointments.SelectedItem);
                    NotifyUpdated();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reschedule_Click(object sender, RoutedEventArgs e)
        {
            if (GridAppointments.SelectedItem != null)
            {
                RescheduleExaminationWindow rescheduleWindow = new RescheduleExaminationWindow((Appointment)GridAppointments.SelectedValue) { DataContext = GridAppointments.SelectedItem };
                rescheduleWindow.Show();
            }
            else
            {
                if(app._currentLanguage.Equals("en-US"))
                    MessageBox.Show("Appointment isn't selected", "Please select appointment", MessageBoxButton.OK);
                else 
                    MessageBox.Show("Pregled nije odabran", "Odaberite pregled", MessageBoxButton.OK);
            }
        }

        public void NotifyUpdated()
        {
            appointments = _secretaryAppointmentController.ReadAllAppointmentsForDate(DateTime.Parse(appointmentsDatePicker.SelectedDate.ToString()));
            GridAppointments.ItemsSource = appointments;
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            NewAppointmentChooseDate chooseDate = new NewAppointmentChooseDate();
            chooseDate.Show();
        }

        private void UpdateGridView()
        {
            GridAppointments.AutoGenerateColumns = false;
            GridAppointments.CanUserSortColumns = false;
            DataGridTextColumn dataColumn = new DataGridTextColumn();
            if (app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Doctor name";
            else
                dataColumn.Header = "Ime doktora";
            dataColumn.Binding = new Binding("Doctor.Name");
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Doctor surname";
            else
                dataColumn.Header = "Prezime doktora";
            dataColumn.Binding = new Binding("Doctor.Surname");
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Patient name";
            else
                dataColumn.Header = "Ime pacijenta";
            dataColumn.Binding = new Binding("Patient.Name");
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Patient surname";
            else 
                dataColumn.Header = "Prezime pacijenta";
            dataColumn.Binding = new Binding("Patient.Surname");
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "Start at";
            else
                dataColumn.Header = "Počinje u";
            dataColumn.Binding = new Binding("Time.Start");
            GridAppointments.Columns.Add(dataColumn);
            dataColumn = new DataGridTextColumn();
            if(app._currentLanguage.Equals("en-US"))
                dataColumn.Header = "End at";
            else 
                dataColumn.Header = "Završava u";
            dataColumn.Binding = new Binding("Time.End");
            GridAppointments.Columns.Add(dataColumn);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            NotifyUpdated();
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

        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            EmergencyExaminationWindow window = new EmergencyExaminationWindow();
            window.Show();
        }
        
        private void OpenPDF_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAppointmentsPdfCreator pdfCreator = new SecretaryAppointmentsPdfCreator();
            pdfCreator.PrintSurvey();
            System.Diagnostics.Process.Start(@"..\..\Resources\weeklyAppointments.pdf");
        }
    }
}
