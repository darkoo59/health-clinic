﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;

namespace Model
{
    public class TimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [JsonConstructor]
        public TimeInterval(DateTime Start, DateTime End)
        {
            this.Start = Start;
            this.End = End;
        }

        public TimeInterval(TimeInterval interval)
        {
            this.Start = interval.Start;
            this.End = interval.End;
        }

        public bool IsOverlaping(TimeInterval interval)
        {
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

        public bool IsSameOrNextDate(TimeInterval timeInterval)
        {
            return End.CompareTo(timeInterval.Start) == 0 || End.AddDays(1).CompareTo(timeInterval.Start) == 0;
        }

        public bool IsThereGapInDates(TimeInterval newInterval)
        {
            return End.AddDays(1).CompareTo(newInterval.Start) < 0;
        }

        public bool IsThereGapInIntervals(TimeInterval newInterval)
        {
            return End.CompareTo(newInterval.Start) < 0;
        }

        public bool IntervalsTouching(TimeInterval newInterval)
        {
            return End.CompareTo(newInterval.Start) == 0;
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