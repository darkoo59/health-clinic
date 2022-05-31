﻿using System;
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
using Controller;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for SearchPatient.xaml
    /// </summary>
    public partial class SearchPatient : Page
    {
        private AnamnesisController anamnesisController;
        private DoctorAppointmentController doctorAppointmentController;
        private MedicalRecordController medicalRecordController;
        private Frame frame;
        private int doctorID;
        public SearchPatient(AnamnesisController anamnesisController,DoctorAppointmentController doctorAppointmentController,MedicalRecordController medicalRecordController,Frame frame,int id)
        {
            InitializeComponent();
            this.DataContext = this;
            this.frame = frame;
            this.doctorID = id;
            this.anamnesisController = anamnesisController;
            this.doctorAppointmentController = doctorAppointmentController;
            this.medicalRecordController = medicalRecordController;
            MedicalRecordDataGrid.ItemsSource = medicalRecordController.ReadAll();
            MedicalRecordDataGrid.AutoGenerateColumns = false;
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Patient Name:";
            data_column.Binding = new Binding("Patient._Name");
            MedicalRecordDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "BirthDate";
            data_column.Binding = new Binding("Patient._BirthDate");

            MedicalRecordDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Patient UID";
            data_column.Binding = new Binding("Patient._Jmbg");
            MedicalRecordDataGrid.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = "Patient blood type";
            data_column.Binding = new Binding("BloodType");
            MedicalRecordDataGrid.Columns.Add(data_column);
            
            
        }

        private void PatientMedicalRecordClick(object sender, RoutedEventArgs e)
        {
            
            MedicalRecord medicalRecord = MedicalRecordDataGrid.SelectedValue as MedicalRecord;
            if (medicalRecord != null)
            {
                PatientTabs patientTabs = new PatientTabs(medicalRecord,frame,doctorID);
                frame.Content = patientTabs;
            }
            else
            {
                MessageBox.Show("Chose whose medical record you want to see.");
            }
        }

        private void PrescriptionClick(object sender, RoutedEventArgs e)
        {
            MedicalRecord medicalRecord = MedicalRecordDataGrid.SelectedValue as MedicalRecord;
            //PrescriptionWindow prescriptionWindow = new PrescriptionWindow(medicalRecordController, medicalRecord, doctorID,frame);
            //frame.Content= prescriptionWindow;
        }
    }
}
