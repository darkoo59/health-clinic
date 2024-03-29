﻿using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for AppointmentDetailsPage.xaml
    /// </summary>
    public partial class AppointmentDetailsPage : Page
    {
        private Appointment appointment;
        private MedicalRecordController medicalRecordController;
        private Anamnesis anamnesis;
        public AppointmentDetailsPage(Appointment appointment)
        {
            this.appointment = appointment;
            InitializeComponent();
            medicalRecordController = new MedicalRecordController();
            anamnesis = medicalRecordController.GetAnamnesis(appointment);
            Appointment.Content = new AnamnesisPage(anamnesis);
            var converter = new System.Windows.Media.BrushConverter();
            Anamensis.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Anamensis.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Diagnosis.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Diagnosis.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
        }

        private void Anamnesis_Click(object sender, RoutedEventArgs e)
        {
            Appointment.Content = new AnamnesisPage(anamnesis);
            var converter = new System.Windows.Media.BrushConverter();
            Anamensis.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Anamensis.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Diagnosis.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Diagnosis.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
        }

        private void Diagnosis_Click(object sender, RoutedEventArgs e)
        {
            Appointment.Content = new DiagnosisPage(anamnesis, medicalRecordController.GetPrescriptions(appointment));
            var converter = new System.Windows.Media.BrushConverter();
            Anamensis.Background = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Anamensis.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF3183CB");
            Diagnosis.Background = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
            Diagnosis.BorderBrush = (SolidColorBrush)converter.ConvertFromString("#FF60BBC9");
        }
    }
}
