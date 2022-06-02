using System.Windows;
using System.Windows.Controls;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.ViewModel;

namespace Sims_Hospital_Zdravo.View.Manager.Surveys
{
    public partial class ManagerHospitalSurveys : Page
    {
        public ManagerHospitalSurveys()
        {
            InitializeComponent();
            DataContext = new ManagerHospitalSurveyViewModel();
        }
    }
}