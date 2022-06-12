using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sims_Hospital_Zdravo.Model
{
    public class Therapy
    {
        DateTime _from, _to;
        string _eventName;
        bool _isAllDay;
        string _startTimeZone, _endTimeZone;
        Brush _color;
        public Therapy()
        {

        }

        public DateTime From
        {
            get { return _from; }
            set
            {
                _from = value;
            }
        }

        public DateTime To
        {
            get { return _to; }
            set
            {
                _to = value;                
            }
        }

        public bool IsAllDay
        {
            get { return _isAllDay; }
            set
            {
                _isAllDay = value;                
            }
        }
        public string EventName
        {
            get { return _eventName; }
            set
            {
                _eventName = value;
            }
        }
        public string StartTimeZone
        {
            get { return _startTimeZone; }
            set
            {
                _startTimeZone = value;
            }
        }
        public string EndTimeZone
        {
            get { return _endTimeZone; }
            set
            {
                _endTimeZone = value;
            }
        }

        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
            }
        }
    }
}
