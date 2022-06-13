using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model;

namespace Sims_Hospital_Zdravo.View.Manager.Surveys
{
    public partial class ManagerSurveys : Page
    {
        public ManagerSurveys()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
        }
    }
}