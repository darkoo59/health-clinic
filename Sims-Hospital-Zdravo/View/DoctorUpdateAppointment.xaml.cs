using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for DoctorUpdateAppointment.xaml
    /// </summary>
    public partial class DoctorUpdateAppointment : Window 
    {
        private DoctorAppointmentController docController;
        private RoomController roomControl;
        public ObservableCollection<string> Patients;
        private int id_app;
        public DoctorUpdateAppointment(DoctorAppointmentController docController,Appointment app,RoomController rom)
        {
            InitializeComponent();
            this.DataContext = this;
            this.docController = docController;
            this.roomControl = rom;

            Patients = new ObservableCollection<string>();
            foreach (Patient pat in this.docController.getPatients())
            {
                Patients.Add(pat._Name + " " + pat._Surname + " " + pat._BirthDate.ToString());

            }
            Patientcb.ItemsSource = Patients;
            Patientcb.Text = app._Patient._Name;
            
            Console.WriteLine(Patientcb.Text);
                TimeInterval dt = app._Time;
                DateTxt.Text = dt.Start.ToString("yyyy-MM-dd");
                TimeTxt.Text = dt.Start.ToString("HH:mm:ss");
                endtime.Text = dt.End.ToString("HH:mm:ss");
                AppType.ItemsSource = Enum.GetValues(typeof(AppointmentType)).Cast<AppointmentType>();
            RoomTxt.Text = app._Room._Id.ToString();
                id_app = app._Id;
            
        }

        private Patient pat;
        public Patient Pat
        {
            get { return pat; }
            set { pat = value; }
        }

        //public virtual string Text { get; set; }

        private void Patientcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = Patientcb.SelectedItem.ToString();
            string[] names = name.Split(' ');
            foreach (Patient pat in this.docController.getPatients())
            {
                if (pat._Name.Equals(names[0]) && pat._Surname.Equals(names[1]))

                {


                    Pat = pat;
                    break;
                }
            }
        }

        //public override string Text
        //{
        //    get
        //    {
        //        return base.Text;
        //    }
        //    set
        //    {
        //        base.Text = value;
        //    }
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id_room = Int32.Parse(RoomTxt.Text);
            Room room = roomControl.FindById(id_room);
            string date = DateTxt.Text;
            string starttime = TimeTxt.Text;
            string end_time = endtime.Text;
            DateTime start = DateTime.Parse(date + " " + starttime);
            DateTime end = DateTime.Parse(date + " " + end_time);
            Doctor doc = this.docController.getDoctor(2);
            
            
            TimeInterval timeInterval = new TimeInterval(start,end);
            Appointment appoi = new Appointment(room,doc,Pat,timeInterval,(AppointmentType)AppType.SelectedValue);
            
            appoi._Id = id_app;
            docController.Update(appoi);
            Close();

        }

       

        
    }
}
