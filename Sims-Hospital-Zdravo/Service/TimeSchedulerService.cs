/***********************************************************************
 * Module:  TimeSchedulerService.cs
 * Author:  stjep
 * Purpose: Definition of the Class Service.TimeSchedulerService
 ***********************************************************************/

using Model;
using System;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class TimeSchedulerService
    {

        private AppointmentRepository appointmentRepository;

        public TimeSchedulerService(AppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        public List<TimeInterval> FindAvailableTimeForInterval(int minutes, Room room1, Room room2)
        {

            List<TimeInterval> ocupied1 = appointmentRepository.GetTimeIntervalsForRoom(room1);
            List<TimeInterval> ocupied2 = appointmentRepository.GetTimeIntervalsForRoom(room2);

            return FindFreeIntervals(ocupied1, ocupied2, minutes);
        }

        public List<TimeInterval> FindAvailableDateRangeForInterval(int days)
        {
            // TODO: implement
            return null;
        }


        private List<TimeInterval> MergeTwoTimeIntervalLists(List<TimeInterval> list1, List<TimeInterval> list2)
        {

            List<TimeInterval> joined = new List<TimeInterval>();
            joined.AddRange(list1);
            joined.AddRange(list2);

            if (joined.Count == 0) return joined;

            List<TimeInterval> sortedList = joined.OrderBy(o => o.Start).ToList();
            List<TimeInterval> mergedList = new List<TimeInterval>();

            int newIntervalCounter = 0;
            foreach (TimeInterval ti in sortedList)
            {
                if (mergedList.Count == 0)
                {
                    mergedList.Add(ti);
                    continue;
                }

                TimeInterval newInterval = mergedList[newIntervalCounter];

                if (newInterval.End.CompareTo(ti.Start) == 0)
                {
                    mergedList[newIntervalCounter].End = ti.End;
                }
                else if (newInterval.End.CompareTo(ti.Start) < 0)
                {
                    mergedList.Add(ti);
                    newIntervalCounter++;
                }

            }
            return mergedList;

        }


        public List<TimeInterval> FindFreeIntervals(List<TimeInterval> unavailable1, List<TimeInterval> unavailable2, int minutes)
        {

            DateTime now = DateTime.Now;
            int newminutes = now.Minute > 30 ? 60 - now.Minute : 30 - now.Minute;
            now = now.AddMinutes(newminutes);
            now = now.AddSeconds(-now.Second);
            DateTime last = now.AddDays(5);

            List<TimeInterval> mergedIntervals = MergeTwoTimeIntervalLists(unavailable1, unavailable2);
            List<TimeInterval> timeIntervals = new List<TimeInterval>();

            foreach (TimeInterval ti in mergedIntervals)
            {
                if (now.CompareTo(ti.Start) > 0) continue;
                if (now.CompareTo(ti.Start) == 0)
                {
                    now = ti.End;
                }

                filterIfIntervalTooShort(minutes, now, ti.Start, timeIntervals);
                now = ti.End;

            }

            if (now.CompareTo(last) < 0)
            {
                filterIfIntervalTooShort(minutes, now, last, timeIntervals);
            }

            return timeIntervals;
        }


        private void filterIfIntervalTooShort(int intervalDuration, DateTime now, DateTime next, List<TimeInterval> intervals)
        {
            TimeSpan diff = next - now;
            if (diff.TotalMinutes >= intervalDuration)
            {
                intervals.Add(new TimeInterval(now, next));
            }
        }


        //public List<TimeInterval> fillListWithFakeTimeIntervals1()
        //{
        //    List<TimeInterval> timeIntervals = new List<TimeInterval>
        //    {
        //        new TimeInterval(new DateTime(2022, 4, 25, 2, 0, 0), new DateTime(2022, 4, 25, 2, 30, 0)),
        //        new TimeInterval(new DateTime(2022, 4, 25, 6, 0, 0), new DateTime(2022, 4, 25, 6, 30, 0)),
        //        new TimeInterval(new DateTime(2022, 4, 25, 6, 30, 0), new DateTime(2022, 4, 25, 7, 0, 0)),
        //    };


        //    return timeIntervals;
        //}

        //public List<TimeInterval> fillListWithFakeTimeIntervals2()
        //{
        //    List<TimeInterval> timeIntervals = new List<TimeInterval>
        //    {
        //        new TimeInterval(new DateTime(2022, 4, 25, 2, 30, 0), new DateTime(2022, 4, 25, 3, 0, 0)),
        //        new TimeInterval(new DateTime(2022, 4, 25, 6, 0, 0), new DateTime(2022, 4, 25, 6, 30, 0)),
        //        new TimeInterval(new DateTime(2022, 4, 25, 7, 0, 0), new DateTime(2022, 4,  25, 7, 30, 0)),
        //    };


        //    return timeIntervals;
        //}

    }
}