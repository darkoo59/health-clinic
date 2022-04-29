using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Model
{
    public class TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeInterval(DateTime Start, DateTime End)
        {
            this.Start = Start;
            this.End = End;
        }

        public string toDateString()
        {
            return Start.ToString("MMMM dd, yyyy") + " - " + End.ToString("MMMM dd, yyyy");
        }

        public override string ToString()
        {
            return Start + " - " + End;
        }
    }
}