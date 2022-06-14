using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.ViewModel.PatientViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DoctorSurveyPage.xaml
    /// </summary>
    public partial class DoctorSurveyPage : Page
    {
        public DoctorSurveyPage(Appointment appointment, Frame frame)
        {
            InitializeComponent();
            this.DataContext = new DoctorSurveyViewModel(appointment, frame);
        }
    }
}
