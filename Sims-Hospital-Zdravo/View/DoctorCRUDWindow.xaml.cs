using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for DoctorCRUDWindow.xaml
    /// </summary>
    public partial class DoctorCRUDWindow : Window
    {

        public ObservableCollection<Appointment> DoctorAppointments;
        private DoctorAppointmentController doctorAppController;
        private RoomController roomController;
        private Appointment app;
        public Appointment App
        {
            get { 
                return app; 
            }
            set {
                app = value; 
            }
        }

        public DoctorCRUDWindow(DoctorAppointmentController doctorAppController)
        {
            InitializeComponent();
            this.DataContext = this;
            this.doctorAppController = doctorAppController;
            DoctorAppointments = doctorAppController.ReadAll(2);
            Console.WriteLine("Duzina liste appointmenta je " + DoctorAppointments.Count);
            foreach(Appointment app in DoctorAppointments)
            {
                Console.WriteLine(app._Doctor._Id);
            }
            
            dataGridDoctorApps.ItemsSource = DoctorAppointments;
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this appointment?", "Delete", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    doctorAppController.DeleteByID((Appointment)dataGridDoctorApps.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editAppointment_Click(object sender, RoutedEventArgs e)
        {
            app = dataGridDoctorApps.SelectedValue as Appointment;
           DoctorUpdateAppointment editAppointment = new DoctorUpdateAppointment(doctorAppController,app,roomController);
            editAppointment.Show();

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            DoctorCreateAppointment addAppointment = new DoctorCreateAppointment(doctorAppController,roomController);
            addAppointment.Show();

        }
    }

    
}
