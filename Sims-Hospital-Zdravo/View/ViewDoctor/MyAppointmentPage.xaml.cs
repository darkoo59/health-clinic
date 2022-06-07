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
using System.Collections.ObjectModel;
using Model;
using Controller;
using Sims_Hospital_Zdravo.ViewModel;

using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for MyAppointmentPage.xaml
    /// </summary>
    public partial class MyAppointmentPage : Page
    {

        public DoctorAppointmentController docController;
        public MedicalRecordController medicalRecordController;
        
        public ObservableCollection<Appointment> appointmentsScheduled;
        private App app;
        private int doctorId;
        private DateTime AppointmentDate;
       
        public MyAppointmentPage(DoctorAppointmentController doctorAppointmentController, AnamnesisController anamnesisController, MedicalRecordController medicalRecordController, int id)
        {
            InitializeComponent();

            app = App.Current as App;
            this.docController = doctorAppointmentController;
            this.DataContext = new MyAppointmentsViewModel(doctorId,docController);
            //DoctorAppointments.ItemsSource = doctorAppointmentController.ReadAll(2);
             
            this.doctorId = id;

            this.medicalRecordController = medicalRecordController;
            
            //this.docController = doctorAppointmentController;

            

            

            


        }



        



        

        private void AppointmentShow_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AppointmentDate = AppointmentShow.SelectedDate.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chose date");
            }


            this.appointmentsScheduled = docController.FilterAppointmentsByDate(AppointmentDate);
            this.DoctorAppointments.ItemsSource = appointmentsScheduled;





        }

        
    }
 }

