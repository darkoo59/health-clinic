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
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Controller;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for RequestForFreeDaysForm.xaml
    /// </summary>
    public partial class RequestForFreeDaysForm : Page
    {
        private DoctorAppointmentController doctorAppointmentController;
        private RequestForFreeDaysController requestForFreeDaysController;
        private int doctorId;
        public RequestForFreeDaysForm( DoctorAppointmentController doctorAppointmentController,RequestForFreeDaysController requestForFreeDaysController,int doctorId)
        {
            InitializeComponent();
            this.doctorAppointmentController = doctorAppointmentController;
            this.requestForFreeDaysController = requestForFreeDaysController;
            this.doctorId = doctorId;


        }

        public void UrgentVacationChecked(FreeDaysRequest request)
        {
            if (UrgentBox.IsChecked == true)
            {
                requestForFreeDaysController.CreateUrgent(request);
            }
            else
            {
                requestForFreeDaysController.Create(request);
            }
        }


        private void SendRequestClick(object sender, RoutedEventArgs e)
        {
            string reasonForDaysOff = ReasonForFreeDaysTxt.Text;
            string fromDate = FromDateTxt.Text;
            string toDate = ToDateTxt.Text;
            Doctor doctor = doctorAppointmentController.getDoctor(doctorId);
            TimeInterval timeInteral = new TimeInterval(DateTime.Parse(fromDate),DateTime.Parse(toDate));
            FreeDaysRequest request = new FreeDaysRequest(timeInteral, doctor, reasonForDaysOff,RequestStatus.PENDING);
            UrgentVacationChecked(request);
        }
    }
}
