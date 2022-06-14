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
        public Appointment appointment;
        public DiagnosisPage(Anamnesis anamnesis, List<Prescription> prescriptions)
        {
            InitializeComponent();
            if (anamnesis != null)
            {
                DiagnosisLabel.Content = anamnesis.Diagnosis;
            }
            if (prescriptions != null && prescriptions.Count == 0)
            {
                string s = "";
                bool flag = true;
                foreach (Prescription prescription in prescriptions)
                {
                    if (flag)
                    {
                        s = s + prescription.Medicine.Name;
                    }
                    else
                    {
                        s = s + ", " + prescription.Medicine.Name;
                    }
                }
                PrescriptionsLabel.Content = s;
            }
        }
    }
}
