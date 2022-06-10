using System;
using System.Windows.Controls;
using System.Windows.Input;
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
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
        }
    }
}