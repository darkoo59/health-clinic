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

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for NewAppointmentChooseDate.xaml
    /// </summary>
    public partial class NewAppointmentChooseDate : Window
    {

        private SecretaryAppointmentController appointmentController;
        private App app;
        public NewAppointmentChooseDate()
        {
            InitializeComponent();
            app = Application.Current as App;
            this.appointmentController = app._secretaryAppointmentController;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.Parse(DatePickerStart.SelectedDate.ToString());
                TimeSpan startTime = TimeSpan.Parse(txtStartTime.Text);
                startDate = startDate.Add(startTime);
                DateTime endDate = DateTime.Parse(DatePickerEnd.SelectedDate.ToString());
                TimeSpan endTime = TimeSpan.Parse(txtEndTime.Text);
                endDate = endDate.Add(endTime);
                TimeInterval selectedDate = new TimeInterval(startDate, endDate);
                appointmentController.ValidateAppointmentInterval(selectedDate);
                NewAppointmentChoosePatient choosePatient = new NewAppointmentChoosePatient(selectedDate);
                choosePatient.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateRecord_Click(object sender, RoutedEventArgs e)
        {
            InsertRecordWindow insertRecord = new InsertRecordWindow(app._recordController);
            insertRecord.Show();
        }
    }
}
