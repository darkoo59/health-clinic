using Sims_Hospital_Zdravo.ViewModel.PatientViewModel;
using Syncfusion.UI.Xaml.Scheduler;
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

namespace Sims_Hospital_Zdravo.View.ViewPatient
{
    /// <summary>
    /// Interaction logic for WeeklyCalendarPage.xaml
    /// </summary>
    public partial class WeeklyCalendarPage : Page
    {
        public SfScheduler Schedule { get; set; }
        public WeeklyCalendarPage()
        {
            InitializeComponent();
            this.DataContext = new TherapyViewModel();
        }
    }
}
