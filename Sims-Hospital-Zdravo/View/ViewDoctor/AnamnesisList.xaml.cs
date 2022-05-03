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
using Sims_Hospital_Zdravo.Model;
using System.Collections.ObjectModel;
using Model;
using Controller;
namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for AnamnesisList.xaml
    /// </summary>
    public partial class AnamnesisList : Window
    {

        private AnamnesisController controller;
        private ObservableCollection<Anamnesis> listOfAnamnesisDoneByDoctor;
        private Appointment appointment;
        private int doctorID;
        private Anamnesis anamnesis;
        private DoctorAppointmentController doctorAppointmentController;
        public AnamnesisList(AnamnesisController anamnesisController,int DoctorId,DoctorAppointmentController docController)
        {
            InitializeComponent();
            this.DataContext = this;
            this.controller = anamnesisController;
            this.doctorID = DoctorId;
            this.doctorAppointmentController = docController;
            listOfAnamnesisDoneByDoctor = new ObservableCollection<Anamnesis>();
            listOfAnamnesisDoneByDoctor = anamnesisController.findAnamnesisByDoctor(doctorID);
            AnamnesisListDoctor.ItemsSource  = listOfAnamnesisDoneByDoctor;
            AnamnesisListDoctor.AutoGenerateColumns = false;
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Patient Name ";
            data_column.Binding = new Binding("_MedicalRecord._Patient._Name");
            AnamnesisListDoctor.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Patient Surname";
            data_column.Binding = new Binding("_MedicalRecord._Patient._Surname");
            AnamnesisListDoctor.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Diagnosis";
            data_column.Binding = new Binding("_Diagnosis");
            AnamnesisListDoctor.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Medical report taken:";
            data_column.Binding = new Binding("_Date");
            AnamnesisListDoctor.Columns.Add(data_column);
            



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            anamnesis = AnamnesisListDoctor.SelectedValue as Anamnesis;
            if(anamnesis!= null)
            {
                EditAnamnesis editAnamnesis = new EditAnamnesis(anamnesis, controller, doctorAppointmentController);
                editAnamnesis.Show();
            }
            else 
            {
                MessageBox.Show("Select medical report you want to edit.");
            }
        }
    }
}
