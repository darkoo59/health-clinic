using Controller;
using Model;
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
    /// Interaction logic for Diagnosis.xaml
    /// </summary>
    public partial class DiagnosisPage : Page
    {
        public App app;
        public MedicalRecordController medicalRecordController;
        public Appointment appointment;
        public Anamnesis anamnesis;
        public DiagnosisPage(Appointment appointment, Anamnesis anamnesis)
        {
            InitializeComponent();
            app = Application.Current as App;
            medicalRecordController = app._recordController;
            this.anamnesis = anamnesis;
            this.appointment = appointment;
            if(anamnesis != null)DiagnosisLabel.Content = anamnesis._Diagnosis;
        }

        private void Notes_LostFocus(object sender, RoutedEventArgs e)
        {
            medicalRecordController.AddNotes(appointment, anamnesis, Notes.Text);
        }
    }
}
