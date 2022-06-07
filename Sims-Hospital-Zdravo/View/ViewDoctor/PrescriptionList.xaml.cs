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
using Sims_Hospital_Zdravo.Model;
using Controller;
using System.Collections.ObjectModel;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for PrescriptionList.xaml
    /// </summary>
    public partial class PrescriptionList : Page
    {
        private MedicalRecordController medicalRecordController;
        private MedicalRecord medicalRecord;
        private List<Prescription> prescriptions;
        private int doctorId;
        private Frame frame;
        public PrescriptionList(MedicalRecordController medicalRecordController,MedicalRecord medicalRecord,int id, Frame frame)
        {
            InitializeComponent();
            this.DataContext = this;
            this.doctorId = id;
            this.frame = frame;
            this.medicalRecordController = medicalRecordController;
            this.medicalRecord = medicalRecord;
            this.prescriptions = medicalRecord.Prescriptions;
            PrescriptionListPatient.ItemsSource = medicalRecordController.GetPrescriptionsByMedicalRecord(medicalRecord);
            PrescriptionListPatient.AutoGenerateColumns = false;
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Medicine";
            data_column.Binding = new Binding("_Medicine._Name");
            PrescriptionListPatient.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = "Strength";
            data_column.Binding = new Binding("_Strength");
            PrescriptionListPatient.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = "Dosage";
            data_column.Binding = new Binding("_Dosage");
            PrescriptionListPatient.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = "Date";
            data_column.Binding = new Binding("_PrescriptionDate");
            PrescriptionListPatient.Columns.Add(data_column);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //PrescriptionWindow prescriptionWindow = new PrescriptionWindow(medicalRecordController, medicalRecord,doctorId,frame);
            //frame.Content = prescriptionWindow;
            //prescriptionWindow.Show();
            //Close();

        }
    }
}
