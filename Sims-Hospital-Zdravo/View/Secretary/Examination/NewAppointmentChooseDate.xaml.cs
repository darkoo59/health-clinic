using Model;
using Sims_Hospital_Zdravo.Controller;
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
using Controller;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for NewAppointmentChooseDate.xaml
    /// </summary>
    public partial class NewAppointmentChooseDate : Window
    {

        private SecretaryAppointmentController _appointmentController;
        private MedicalRecordController _medicalRecordController;
        public NewAppointmentChooseDate()
        {
            InitializeComponent();
            this._appointmentController = new SecretaryAppointmentController();
            _medicalRecordController = new MedicalRecordController();
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DatePickerStart.SelectedDate != null && DatePickerEnd.SelectedDate != null)
                {
                    DateTime startDate = DateTime.Parse(DatePickerStart.SelectedDate.ToString());
                    TimeSpan startTime = TimeSpan.Parse(txtStartTime.Text);
                    startDate = startDate.Add(startTime);
                    DateTime endDate = DateTime.Parse(DatePickerEnd.SelectedDate.ToString());
                    TimeSpan endTime = TimeSpan.Parse(txtEndTime.Text);
                    endDate = endDate.Add(endTime);
                    TimeInterval selectedDate = new TimeInterval(startDate, endDate);
                    _appointmentController.ValidateAppointmentInterval(selectedDate);
                    NewAppointmentChoosePatient choosePatient = new NewAppointmentChoosePatient(selectedDate);
                    choosePatient.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Date isn't selected", "Please select date!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateRecord_Click(object sender, RoutedEventArgs e)
        {
            InsertRecordWindow insertRecord = new InsertRecordWindow();
            insertRecord.Show();
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
