using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Controller;
using System.Collections;
using Repository;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Model;

using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for DoctorCreateAppointment.xaml
    /// </summary>
    public partial class DoctorCreateAppointment : Window, IUpdateFilesObservable
    {
        public DoctorAppointmentController docAppController;
        public RoomController roomController;
        public int id;
        public ObservableCollection<string> patients;

        private List<IUpdateFilesObserver> observers;

        public DoctorCreateAppointment(DoctorAppointmentController docController, RoomController roomControl)
        {
            InitializeComponent();
            observers = new List<IUpdateFilesObserver>();
            this.DataContext = this;
            this.docAppController = docController;
            this.roomController = roomControl;
            patients = new ObservableCollection<string>();
            AppType.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();

            foreach (Patient pat in this.docAppController.getPatients())
            {
                patients.Add(pat._Name + " " + pat._Surname + " " + pat._BirthDate.ToString());
            }

            Patients.ItemsSource = patients;

            var startTime =DateTime.Parse( "8:00");
            var endTime = DateTime.Parse("21:00");
            List<string> time_list = new List<string>();

            while (startTime < endTime)
            {

                time_list.Add(startTime.ToShortTimeString() + " - " + startTime.AddMinutes(30).ToShortTimeString());
                startTime = startTime.AddMinutes(30);
            }
            comboTimeAppointment.ItemsSource = time_list;






        }

        private Patient pat;

        public Patient Pat
        {
            get { return pat; }
            set { pat = value; }
        }

        Random rnd = new Random();

        private void Patients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = Patients.SelectedItem.ToString();
            string[] names = name.Split(' ');
            foreach (Patient pat in this.docAppController.getPatients())
            {
                if (pat._Name.Equals(names[0]) && pat._Surname.Equals(names[1]))

                {
                    Pat = pat;
                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
                


                string appointmentTime = comboTimeAppointment.SelectedItem.ToString();
            string[] array = appointmentTime.Split('-');
            string start = array[0];
            string end = array[1];
            //string start = cbItem.Content.ToString();
            //string end = cbItemEnd.Content.ToString();
            string d = DatePick.Text;
            var dt_start = DateTime.Parse(d + " " + start);
            var dt_end = DateTime.Parse(d + " " + end);


            int numOfRoom = Int32.Parse(RoomTxt.Text);
            Room room = this.roomController.FindById(numOfRoom);
            Doctor doc = this.docAppController.getDoctor(2);
            TimeInterval timeInterval = new TimeInterval(dt_start, dt_end);
            try { 
            Appointment app = new Appointment(room, doc, Pat, timeInterval, (AppointmentType)AppType.SelectedValue);
            app._Id = this.docAppController.GenerateId();
            
                docAppController.Create(app);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NotifyUpdated();
            Close();
        }

        public void AddObserver(IUpdateFilesObserver observer)
        {
            observers.Add(observer);
        }

        public void NotifyUpdated()
        {
            foreach (IUpdateFilesObserver observer in observers)
            {
                observer.NotifyUpdated();
            }
        }

        public void RemoveObserver(IUpdateFilesObserver observer)
        {
            observers.Remove(observer);
        }
    }
}