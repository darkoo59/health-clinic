using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sims_Hospital_Zdravo.ViewModel.PatientViewModel
{
    public class TherapyViewModel
    {
        public TherapyViewModel()
        {
            this.Events = new ObservableCollection<Therapy>();
            IntializeAppoitments();
        }

        public ObservableCollection<Therapy> Events
        {
            get;
            set;
        }
        private void IntializeAppoitments()
        {
            Therapy therapy = new Therapy();
            DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            therapy.From = from.AddHours(8);
            DateTime to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            therapy.To = to.AddHours(9);
            therapy.EventName = "Bromazepan";
            therapy.Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD80073"));
            therapy.EndTimeZone = "";
            therapy.IsAllDay = false;
            therapy.StartTimeZone = "";
            this.Events.Add(therapy);  
        }
    }
}
