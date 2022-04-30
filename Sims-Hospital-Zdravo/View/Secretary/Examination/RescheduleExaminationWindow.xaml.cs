using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for RescheduleExaminationWindow.xaml
    /// </summary>
    public partial class RescheduleExaminationWindow : Window,IUpdateFilesObservable
    {
        private Appointment selectedAppointment;
        private SecretaryAppointmentController secretaryAppointmentController;
        private List<IUpdateFilesObserver> observers;
        public RescheduleExaminationWindow(Appointment appointment,SecretaryAppointmentController controller)
        {
            InitializeComponent();
            this.observers = new List<IUpdateFilesObserver>();
            this.DataContext = this;
            this.selectedAppointment = appointment;
            this.secretaryAppointmentController = controller;
            startDatePicker.SelectedDate = appointment._Time.Start;
            endDatePicker.SelectedDate = appointment._Time.End;
            txtStartTime.Text = appointment._Time.Start.TimeOfDay.ToString();
            txtEndTime.Text = appointment._Time.End.TimeOfDay.ToString();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string startString = startDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            DateTime startDate = DateTime.Parse(startString);
            string[] startTime = txtStartTime.Text.Split(':');
            startDate = startDate.AddHours(Int32.Parse(startTime[0]));
            startDate = startDate.AddMinutes(Int32.Parse(startTime[1]));
            
            string endString = endDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            DateTime endDate = DateTime.Parse(endString);
            string[] endTime = txtEndTime.Text.Split(':');
            endDate=endDate.AddHours(Int32.Parse(endTime[0]));
            endDate=endDate.AddMinutes(Int32.Parse(endTime[1]));
            //DateTime startDate = DateTime.Parse(startString);
            //DateTime startTemp = DateTime.Parse(txtStartTime.Text);
            /*
              string[] time = Time.Text.Split(':');
                DateTime date = new DateTime(_DateTime.Year,_DateTime.Month,_DateTime.Day);
                date = date.AddHours(Int32.Parse(time[0]));
                date = date.AddMinutes(Int32.Parse(time[1]));
             */
            selectedAppointment._Time.Start = startDate;
            selectedAppointment._Time.End = endDate;
            
            secretaryAppointmentController.Update(selectedAppointment);
            NotifyUpdated();
            Close();
        }

        public void NotifyUpdated()
        {
            foreach (IUpdateFilesObserver observer in observers)
            {
                observer.NotifyUpdated();
            }
        }

        public void AddObserver(IUpdateFilesObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IUpdateFilesObserver observer)
        {
            observers.Remove(observer);
        }
    }
}
