using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class Notes
    {
        private DateTime _reminder;
        private string _text;
        private bool _flag;
        public Notes(DateTime dateTime, string text)
        {
            Reminder = dateTime;
            Text = text;
            Flag = true;
        }
        public DateTime Reminder { get { return _reminder; } set { _reminder = value; } }
        public string Text { get { return _text; } set { _text = value; } }
        public bool Flag { get { return _flag; } set { _flag = value; } }
    }
}
