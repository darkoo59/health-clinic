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
        private App app;
        private AnamnesisController controller;
        private List<Anamnesis> listOfAnamnesisDoneByDoctor;
        private int doctorID;
        private Anamnesis anamnesis;
        private MedicalRecord medicalRecord;
        private DoctorAppointmentController doctorAppointmentController;
        public AnamnesisListPage(int doctorId,MedicalRecord medicalRecord)
        {

            InitializeComponent();
            this.DataContext = this;
            this.app = App.Current as App;
            this.controller = app._anamnesisController;
            this.doctorID = doctorId;
            this.medicalRecord = medicalRecord;
            this.doctorAppointmentController = app._doctorAppointmentController;
            listOfAnamnesisDoneByDoctor = new List<Anamnesis>();
            //listOfAnamnesisDoneByDoctor = controller.FindAnamnesisByDoctor(doctorID);
            AnamnesisListDoctor.ItemsSource = controller.FindAnamnesisByPatient(medicalRecord);
            AnamnesisListDoctor.AutoGenerateColumns = false;
            DataGridTextColumn data_column = new DataGridTextColumn();
            
            data_column.Header = "Patient Name ";
            data_column.Binding = new Binding("Patient.Name");
            AnamnesisListDoctor.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Patient Surname";
            data_column.Binding = new Binding("Patient.Surname");
            AnamnesisListDoctor.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Diagnosis";
            data_column.Binding = new Binding("Diagnosis");
            AnamnesisListDoctor.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Medical report taken:";
            data_column.Binding = new Binding("Date");
            AnamnesisListDoctor.Columns.Add(data_column);
            DataGridTemplateColumn templateColumn = new DataGridTemplateColumn();
            Button editButton = new Button();
            DataTemplate dataTemplate = new DataTemplate();
            dataTemplate.DataType = editButton.GetType();
            templateColumn.CellTemplate = dataTemplate;
            AnamnesisListDoctor.Columns.Add(templateColumn);




        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            anamnesis = AnamnesisListDoctor.SelectedValue as Anamnesis;
            if (anamnesis != null)
            {
                EditAnamnesis editAnamnesis = new EditAnamnesis(anamnesis, medicalRecord);
                editAnamnesis.Show();
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
                editAnamnesis.Show();
            }
            else
            {
                MessageBox.Show("Select medical report you want to edit.");
            }
        }
    }
}
    

