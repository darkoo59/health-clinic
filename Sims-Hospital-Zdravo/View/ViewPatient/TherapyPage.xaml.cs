using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
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

namespace Sims_Hospital_Zdravo.View.ViewPatient
{
    /// <summary>
    /// Interaction logic for TherapyPage.xaml
    /// </summary>
    public partial class TherapyPage : Page
    {
        private Frame frame;
        WeeklyCalendarPage weeklyCalendarPage;
        public TherapyPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            weeklyCalendarPage = new WeeklyCalendarPage();
            Calendar.Content = weeklyCalendarPage;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //frame.Content = new NotesPage(frame);
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(weeklyCalendarPage.Schedule,"Schedule");
            }
        }
    }
}
