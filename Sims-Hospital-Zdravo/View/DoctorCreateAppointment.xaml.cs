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
using Sims_Hospital_Zdravo.Model;
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
        private DoctorAppointmentController docAppController;
        private RoomController roomController;
        private List<IUpdateFilesObserver> observers;
        private int id;
        public DoctorCreateAppointment(DoctorAppointmentController docController,RoomController roomControl)
        {
            InitializeComponent();
            observers = new List<IUpdateFilesObserver>();
            this.DataContext = this;
            this.docAppController = docController;
            this.roomController = roomControl;
            patients = new ObservableCollection<string>();

            foreach(Patient pat in this.docAppController.getPatients())
            {
                patients.Add(pat._Name + " " +  pat._Surname + " " + pat._BirthDate.ToString());

            }
            Patients.ItemsSource = patients;


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

            //String date = dateTxt.Text;
            ComboBoxItem cbItem = cbTime.SelectedItem as ComboBoxItem;
            ComboBoxItem cbItemEnd = cbTimeEnd.SelectedItem as ComboBoxItem;
            
            
                string start = cbItem.Content.ToString();
            string end = cbItemEnd.Content.ToString();
                string d = DatePick.Text;
                var dt_start = DateTime.Parse(d + " " + start);
                 var dt_end = DateTime.Parse(d + " " + end);
                
            
            int numOfRoom = Int32.Parse(RoomTxt.Text);
            Room room = this.roomController.FindById(numOfRoom);
            Doctor doc = this.docAppController.getDoctor(2);
            TimeInterval timeInterval = new Model.TimeInterval(dt_start, dt_end);
           // Patient pat = PatientSelected();
            Appointment app = new Appointment(room,doc,Pat,timeInterval);
            docAppController.Create(app);
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
