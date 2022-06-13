using Sims_Hospital_Zdravo.Controller;
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

namespace Sims_Hospital_Zdravo.View.ViewPatient
{
    /// <summary>
    /// Interaction logic for NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        private NotesController notesController;
        private Frame frame;
        public NotesPage(Frame frame)
        {
            InitializeComponent();
            notesController = new NotesController();
            this.frame = frame;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = (DateTime)Date.SelectedDate;
            string[] time = Time.Text.Split(':');
            dateTime = dateTime.AddHours(Int32.Parse(time[0]));
            dateTime = dateTime.AddMinutes(Int32.Parse(time[1]));
            notesController.Create(new Notes(dateTime,Notes.Text));
            frame.Content = new TherapyPage(frame);
        }
    }
}
