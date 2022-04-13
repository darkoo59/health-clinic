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
        
        public ObservableCollection<Appointment> DoctorAppointments = new ObservableCollection<Appointment>();
        private DoctorAppointmentController doctorAppController;
        

        public DoctorCRUDWindow(DoctorAppointmentController doctorAppController)
        {
            InitializeComponent();
            this.DataContext = this;
            this.doctorAppController = doctorAppController;
            DoctorAppointments = doctorAppController.ReadAll(1);

            dataGridDoctorApps.ItemsSource = DoctorAppointments;
            
        }

        /*private void Rooms_Click(object sender, RoutedEventArgs e)
        {
           // DataGridApp.
        }*/
    }

    
}
