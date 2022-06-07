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
        private NotesController _notesController;
        private Frame _frame;
        public NotesPage(Frame frame)
        {
            InitializeComponent();
            _notesController = new NotesController();
            _frame = frame;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = (DateTime)Date.SelectedDate;
            string[] time = Time.Text.Split(':');
            dateTime = dateTime.AddHours(Int32.Parse(time[0]));
            dateTime = dateTime.AddMinutes(Int32.Parse(time[1]));
            _notesController.Create(new Notes(dateTime,Notes.Text));
            _frame.Content = new TherapyPage(_frame);
        }
    }
}
