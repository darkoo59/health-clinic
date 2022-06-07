using System;
using System.Windows.Controls;
using Sims_Hospital_Zdravo.ViewModel;

namespace Sims_Hospital_Zdravo.View.Manager.Surveys
{
    public partial class ManagerDoctorSurveys : Page
    {
        public ManagerDoctorSurveys()
        {
            Console.WriteLine("Working");
            InitializeComponent();
            DataContext = new ManagerDoctorSurveyModel();
        }
    }
}