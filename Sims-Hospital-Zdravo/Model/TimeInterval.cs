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

        public bool IsOverlaping(TimeInterval interval)
        {
            Console.WriteLine(interval);
            Console.WriteLine(this.Start + " " + this.End);
            return IsIntervalInside(interval, this)
                   || IsIntervalInside(this, interval)
                   || IsDateInsideInterval(this, interval.Start)
                   || IsDateInsideInterval(this, interval.End);
        }
        
        public bool IsLongerThanDuration(double duration)
        {
            return (this.End - this.Start).TotalHours >= duration;
        }

        private bool IsIntervalInside(TimeInterval outside, TimeInterval inside)
        {
            return outside.Start <= inside.Start && outside.End >= inside.End;
        }

        private bool IsDateInsideInterval(TimeInterval interval, DateTime date)
        {
            return interval.Start <= date && interval.End >= date;
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