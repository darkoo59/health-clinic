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
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Controller;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for AnamnesisListPage.xaml
    /// </summary>
    public partial class AnamnesisListPage : Page, IUpdateFilesObserver
    {
        
        private AnamnesisController controller;
        private List<Anamnesis> listOfAnamnesisDoneByDoctor;
        private int doctorID;
        private Anamnesis anamnesis;
        private MedicalRecord medicalRecord;
        private DoctorAppointmentController doctorAppointmentController;
        private Frame frame;
        public AnamnesisListPage(int doctorId,MedicalRecord medicalRecord,Frame frame)
        {

            InitializeComponent();
            this.DataContext = this;
            //this.app = App.Current as App;
            this.controller = new AnamnesisController();
            this.doctorID = doctorId;
            this.frame = frame;
            this.medicalRecord = medicalRecord;
            this.doctorAppointmentController = new DoctorAppointmentController();
            listOfAnamnesisDoneByDoctor = new List<Anamnesis>();
            AnamnesisListDoctor.ItemsSource = controller.FindAnamnesisByPatient(medicalRecord);
            




        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            anamnesis = AnamnesisListDoctor.SelectedValue as Anamnesis;
            if (anamnesis != null)
            {
                EditAnamnesis editAnamnesis = new EditAnamnesis(anamnesis, medicalRecord);
                //editAnamnesis.Show();
            }
            else
            {
                MessageBox.Show("Select medical report you want to edit.");
            }
        }

        public void NotifyUpdated()
        {
            listOfAnamnesisDoneByDoctor = controller.FindAnamnesisByDoctor(doctorID);
            AnamnesisListDoctor.ItemsSource = listOfAnamnesisDoneByDoctor;
        }

        private void ButtoneditClick(object sender, RoutedEventArgs e)
        {
            anamnesis = AnamnesisListDoctor.SelectedValue as Anamnesis;
            if (anamnesis != null)
            {
                EditAnamnesis editAnamnesis = new EditAnamnesis(anamnesis, medicalRecord);
               // editAnamnesis.Show();
            }
            else
            {
                MessageBox.Show("Select medical report you want to edit.");
            }
        }

        private void MedicalreportClick(object sender, RoutedEventArgs e)
        {
            MedicalReport medicalReport = new MedicalReport(medicalRecord, doctorID, frame);
            frame.Content = medicalReport;
        }
    }
}
    

