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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;

namespace Sims_Hospital_Zdravo.View.Secretary.Examination
{
    /// <summary>
    /// Interaction logic for ChooseDoctorSpecialityWindow.xaml
    /// </summary>
    public partial class ChooseDoctorSpecialityWindow : Window
    {
        private App app;
        private Patient patient;
        private SecretaryAppointmentController _secretaryAppointmentController;
        public ChooseDoctorSpecialityWindow(Patient patient)
        {
            app = Application.Current as App;
            InitializeComponent();
            this.patient = patient;
            _secretaryAppointmentController = new SecretaryAppointmentController();
            comboSpeciality.ItemsSource = Enum.GetValues(typeof(SpecialtyType)).Cast<SpecialtyType>();
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

        private void Next_Click(object sender, MouseButtonEventArgs e)  //TODO
        {
            try
            {
                if (comboSpeciality.SelectedItem != null)
                {
                    Appointment appointment =
                            _secretaryAppointmentController.FindAndScheduleEmergencyAppointmentIfCan(patient,
                                (SpecialtyType)comboSpeciality.SelectedItem);
                        if (appointment != null)
                        {
                            appointment.Type = AppointmentType.URGENCY;
                            _secretaryAppointmentController.Create(appointment);
                            MessageBox.Show("Emergency appointment successfully scheduled");
                            this.Close();
                        }
                        else
                        {
                            RescheduleForEmergencyWindow window =
                                new RescheduleForEmergencyWindow(patient, (SpecialtyType)comboSpeciality.SelectedItem);
                            window.Show();
                            this.Close();
                        }
                    
            }
                else
                    MessageBox.Show("Speciality isn't selected", "Please select speciality");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
