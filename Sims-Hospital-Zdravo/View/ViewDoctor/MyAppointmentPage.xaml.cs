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
        private PatientMedicalRecordController PatientMedicalRecordController;
        public List<Appointment> appointmentsScheduled;
        private App app;
        private int doctorId;
        private DateTime AppointmentDate;
          private bool  _isToolTipVisible;
        private Frame frame;
       
        public MyAppointmentPage( AnamnesisController anamnesisController,  int id,Frame frame)
        {
            InitializeComponent();

            app = App.Current as App;
            this.doctorId = id;
            this.frame = frame;
            this.docController = new DoctorAppointmentController();
            this.DataContext = new MyAppointmentsViewModel(doctorId);
            this._isToolTipVisible = false;
            
             
            

            this.medicalRecordController = new MedicalRecordController();
            
            

            

            

            


        }

        public bool IsToolTipVisible
        {
            get
            {
                return _isToolTipVisible;
            }
            set
            {
                _isToolTipVisible = value;
            }
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

       

        private void CheckdTooltip(object sender, RoutedEventArgs e)
        {
            IsToolTipVisible = false;

        }

        private void uncheckedtooltipclick(object sender, RoutedEventArgs e)
        {
            IsToolTipVisible = true;
        }

        private void NewAppoitmentClick(object sender, RoutedEventArgs e)
        {
            DoctorCRUDWindow doctorCRUDWindow = new DoctorCRUDWindow(docController,doctorId);
            frame.Content = doctorCRUDWindow;
        }
    }
 }

