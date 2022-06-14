using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        string pattern = @"\d?\d:\d\d";
        private Frame frame;
        public NotesPage(Frame frame)
        {
            InitializeComponent();
            notesController = new NotesController();
            this.frame = frame;
            ValidateNotes.Visibility = Visibility.Hidden;
            ValidateDate.Visibility = Visibility.Hidden;
            ValidateTime.Visibility = Visibility.Hidden;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ValidateNotes.Visibility = Visibility.Hidden;
            ValidateDate.Visibility = Visibility.Hidden;
            ValidateTime.Visibility = Visibility.Hidden;
            try
            {
                if (Notes.Text == "")
                {
                    throw new Exception("Enter notes");
                }
                else  if (Date.SelectedDate == null)
                {
                    throw new Exception("Select a date");
                }
                DateTime dateTime = (DateTime)Date.SelectedDate;
                DateTime date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                Match m = Regex.Match(Time.Text, pattern);
                if (m.Success)
                {
                    string[] times = Time.Text.Split(':');
                    date = date.AddHours(Int32.Parse(times[0]));
                    date = date.AddMinutes(Int32.Parse(times[1]));
                }
                else
                {
                    throw new Exception("Time format must be HH:mm(10:00)");
                }
                string[] time = Time.Text.Split(':');
                dateTime = dateTime.AddHours(Int32.Parse(time[0]));
                dateTime = dateTime.AddMinutes(Int32.Parse(time[1]));
                notesController.Create(new Notes(dateTime, Notes.Text));
                frame.Content = new HomePatient(frame);
            }
            catch(Exception ex)
            {
                if (ex.Message.Equals("Select a date"))
                {
                    ValidateNotes.Visibility = Visibility.Hidden;
                    ValidateDate.Visibility = Visibility.Visible;
                    ValidateTime.Visibility = Visibility.Hidden;
                    ValidateDate.Text = ex.Message;
                }
                else if (ex.Message.Equals("Enter notes"))
                {
                    ValidateNotes.Visibility = Visibility.Visible;
                    ValidateDate.Visibility = Visibility.Hidden;
                    ValidateTime.Visibility = Visibility.Hidden;
                    ValidateNotes.Text = ex.Message;
                }
                else
                {
                    ValidateNotes.Visibility = Visibility.Hidden;
                    ValidateDate.Visibility = Visibility.Hidden;
                    ValidateTime.Visibility = Visibility.Visible;
                    ValidateTime.Text = ex.Message;
                }
            }
        }
    }
}
