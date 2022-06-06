using Controller;
using Model;
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
    /// Interaction logic for TherapyPage.xaml
    /// </summary>
    public partial class TherapyPage : Page
    {
        private MedicalRecordController recordController;
        private App app;
        private List<DateTime> dateTimes;
        public TherapyPage()
        {
            app = Application.Current as App;
            recordController = app._recordController;
            InitializeComponent();
            FindWeek();
            Schedule.AutoGenerateColumns = false;
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "";
            data_column.Width = 21;
            Schedule.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = dateTimes.ElementAt(0).ToString("dd/MM");
            data_column.Width = 90;
            Schedule.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = dateTimes.ElementAt(1).ToString("dd/MM");
            data_column.Width = 90;
            Schedule.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = dateTimes.ElementAt(2).ToString("dd/MM");
            data_column.Width = 90;
            Schedule.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = dateTimes.ElementAt(3).ToString("dd/MM");
            data_column.Width = 90;
            Schedule.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = dateTimes.ElementAt(4).ToString("dd/MM");
            data_column.Width = 90;
            Schedule.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = dateTimes.ElementAt(5).ToString("dd/MM");
            data_column.Width = 90;
            Schedule.Columns.Add(data_column);
            data_column = new DataGridTextColumn();
            data_column.Header = dateTimes.ElementAt(6).ToString("dd/MM");
            data_column.Width = 90;
            Schedule.Columns.Add(data_column);
        }
        private List<TherapySchedule> InitList()
        {
            List<TherapySchedule> therapySchedules = new List<TherapySchedule>();
            for (int i = 0; i < 24; i++)
            {
                therapySchedules.Add(SetRow(i));
            }
            return therapySchedules;
        }
        private TherapySchedule SetRow(int i)
        {
            string[] days = { "", "", "", "", "", "", "" };
            int j = 0;
            foreach (DateTime dateTime in dateTimes)
            {
                DateTime date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                date = date.AddHours(i);
                days[j] = FindMedicine(new TimeInterval(date, date.AddHours(1)));
                j++;
            }
            return new TherapySchedule(parseIntToHour(i), days[0], days[1], days[2], days[3], days[4], days[5], days[6]);
        }
        private string parseIntToHour(int i)
        {
            if (i < 10)
            {
                return "0" + i;
            }
            else 
            {
                return "" + i;
            }
        }
        private string FindMedicine(TimeInterval timeInterval)
        {
            return "";  
        }
        private void FindWeek()
        {
            DateTime dateTime = DateTime.Now;
            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;
            dateTimes = new List<DateTime>();
            for (int i = FindDay(dayOfWeek); i < FindDay(dayOfWeek) + 7; i++)
            {
                dateTimes.Add(newDay(i));
            }
        }
        private int FindDay(DayOfWeek dayOfWeek)
        {
            if (DayOfWeek.Monday == dayOfWeek) return 0;
            if (DayOfWeek.Tuesday == dayOfWeek) return -1;
            if (DayOfWeek.Wednesday == dayOfWeek) return -2;
            if (DayOfWeek.Thursday == dayOfWeek) return -3;
            if (DayOfWeek.Friday == dayOfWeek) return -4;
            if (DayOfWeek.Saturday == dayOfWeek) return -5;
            return -6;
        }
        private DateTime newDay(int i) 
        {
            return DateTime.Now.AddDays(i);
        }
    }
}
