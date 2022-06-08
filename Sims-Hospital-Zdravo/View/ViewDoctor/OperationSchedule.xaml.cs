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
using Controller;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for OperationSchedule.xaml
    /// </summary>
    public partial class OperationSchedule : Page
    {
        private DoctorAppointmentController doctorAppointmentController;
        private Frame frame;

        public OperationSchedule(DoctorAppointmentController doctorAppointmentController,Frame frame)
        {
            InitializeComponent();
            this.doctorAppointmentController = doctorAppointmentController;
            this.frame = frame;
        }

        private void Newoperation_Click(object sender, RoutedEventArgs e)
        {
            OperationForm operationForm = new OperationForm(doctorAppointmentController);
            frame.Content = operationForm;
        }
    }
}
