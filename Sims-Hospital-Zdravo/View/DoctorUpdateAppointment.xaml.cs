using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for DoctorUpdateAppointment.xaml
    /// </summary>
    public partial class DoctorUpdateAppointment : Window
    {
        private DoctorAppointmentController docController;
        private RoomController roomControl;
       
        public DoctorUpdateAppointment(DoctorAppointmentController docController,Appointment app,RoomController rom)
        {
            InitializeComponent();
            this.DataContext = this;
            this.docController = docController;
            this.roomControl = rom;
            DateTime dt = app._DateAndTime;
            DateTxt.Text = dt.ToString("dd-MM-yyyy");
            TimeTxt.Text = dt.ToString("HH:mm:ss");
            PatientTxt.Text = app._Patient._Name;
            SurnameText.Text = app._Patient._Surname;
            RoomTxt.Text = app._Room._Id.ToString();
        }

        private Patient pat;
        public Patient Pat
        {
            get { return pat; }
            set { pat = value; }
        }
        public Patient PatientSelected()
        {

            foreach (Patient pat in this.docController.getPatients() )
            {
                if (pat._Name.Equals( PatientTxt.Text) && pat._Surname.Equals( SurnameText.Text))
                {
                    Pat = pat;
                    

                }
            }
            return Pat;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id_room = Int32.Parse(RoomTxt.Text);
            Room room = roomControl.FindById(id_room);
            string date = DateTxt.Text;
            string time = TimeTxt.Text;
            DateTime dt = DateTime.Parse(date + " " + time);
            Doctor doc = this.docController.getDoctor(2);
            Patient pat = PatientSelected();
            Appointment app = new Appointment(room,doc,Pat,dt,1);
            
            docController.Update(app);
            Close();

        }



        

    }
}
