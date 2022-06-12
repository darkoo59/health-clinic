using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sims_Hospital_Zdravo.Utils.Commands
{
    public class SelectDateCommand : IDemoCommand
    {
        private DatePicker datePicker;
        private DateTime dateTime;
        public SelectDateCommand(DatePicker datePicker, DateTime dateTime)
        {
            this.datePicker = datePicker;
            this.dateTime = dateTime;
        }

        public void Execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate { datePicker.SelectedDate = dateTime; });
        }
    }
}
