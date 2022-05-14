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
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Model;
using Controller;
using System.Collections.ObjectModel;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for PatientTabs.xaml
    /// </summary>
    public partial class PatientTabs : Page
    {
        private MedicalRecordController medicalRecordController;
        private MedicalRecord medicalRecord;
        private Frame frame;
        public PatientTabs(MedicalRecord medicalRecord,Frame frame)
        {
            InitializeComponent();
            this.medicalRecord = medicalRecord;
            this.frame = frame;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tc = sender as TabControl;
            if (tc != null)
            {
               TabItem item = tc.SelectedItem as TabItem;
                if(item.Name == "PatientInfoTab")
                {
                    PatientMedicalRecordPage patientMedicalRecordPage = new PatientMedicalRecordPage(medicalRecord,frame);
                    frame.Content = patientMedicalRecordPage;
                }
            }
        }
    }
}
