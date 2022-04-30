/***********************************************************************
 * Module:  TimeSchedulerService.cs
 * Author:  stjep
 * Purpose: Definition of the Class Service.TimeSchedulerService
 ***********************************************************************/

using Model;
using System;
using Repository;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Sims_Hospital_Zdravo.Repository;

namespace Service
{
    public class TimeSchedulerService
    {
        private AppointmentRepository appointmentRepository;
        private RenovationRepository renovationRepository;
        private RelocationAppointmentRepository relocationRepository;

        public TimeSchedulerService(AppointmentRepository appointmentRepository,
            RenovationRepository renovationRepository, RelocationAppointmentRepository relocationRepository)
        {
            this.appointmentRepository = appointmentRepository;
            this.renovationRepository = renovationRepository;
            this.relocationRepository = relocationRepository;
        }

        public List<TimeInterval> FindReservedTimeForRooms(Room room1, Room room2)
        {
            List<TimeInterval> takenIntervalsRoom1 = CaptureAllTakenIntervalsForRoom(room1._Id);
            List<TimeInterval> takenIntervalsRoom2 = CaptureAllTakenIntervalsForRoom(room2._Id);
            List<TimeInterval> takenIntervals = new List<TimeInterval>(takenIntervalsRoom1.Concat(takenIntervalsRoom2));

            takenIntervals = takenIntervals.OrderBy(o => o.Start).ToList();
            takenIntervals = CompactIntervals(takenIntervals);

            return takenIntervals;
        }

        public List<TimeInterval> FindReservedDatesForRoom(int days, Room room)
        {
            List<TimeInterval> takenIntervals = CaptureAllTakenIntervalsForRoom(room._Id);
            takenIntervals = takenIntervals.OrderBy(o => o.Start).ToList();
            takenIntervals = CompactIntervals(takenIntervals);

            takenIntervals = ConvertIntervalsToTakenDates(takenIntervals);
            return takenIntervals;
        }

        public bool IsRoomFreeInInterval(int roomId, TimeInterval ti)
        {
            List<TimeInterval> intervals = CaptureAllTakenIntervalsForRoom(roomId);
            intervals = intervals.OrderBy(o => o.Start).ToList();
            intervals = CompactIntervals(intervals);

            foreach (TimeInterval app in intervals)
            {
                DateTime start = app.Start;
                DateTime end = app.End;
                if (start.CompareTo(ti.Start) < 0 && end.CompareTo(ti.Start) > 0) return false;
                if (start.CompareTo(ti.End) < 0 && end.CompareTo(ti.End) > 0) return false;
                if (start.CompareTo(ti.Start) > 0 && end.CompareTo(ti.End) < 0) return false;
                if (start.CompareTo(ti.Start) == 0 && end.CompareTo(ti.End) == 0) return false;
            }

            return true;
        }

        public bool IsRoomFreeInDateInterval(int roomId, TimeInterval ti)
        {
            List<Appointment> appointments = appointmentRepository.FindByRoomId(roomId);
            foreach (Appointment app in appointments)
            {
                DateTime startDate = app._Time.Start.Date;
                DateTime endDate = app._Time.End.Date;
                DateTime startDateNew = ti.Start.Date;
                DateTime endDateNew = ti.End.Date;

                if (startDate == startDateNew && endDate == endDateNew) return false;
                if (startDate >= startDateNew && startDate <= endDateNew) return false;
                if (endDate >= startDateNew && endDate <= endDateNew) return false;
                if (startDate <= startDateNew && endDate >= endDateNew) return false;
            }

            return true;
        }

        private List<TimeInterval> CaptureAllTakenIntervalsForRoom(int roomId)
        {
            List<TimeInterval> takenIntervalsApp = appointmentRepository.GetTimeIntervalsForRoom(new Room(-1, roomId, 0));
            List<TimeInterval> takenIntervalRenovation = renovationRepository.FindTakenIntervalsForRoom(roomId);
            List<TimeInterval> takenIntervalRelocation = relocationRepository.FindTakenIntervalsForRoom(roomId);

            return new List<TimeInterval>(takenIntervalRelocation.Concat(takenIntervalRenovation).Concat(takenIntervalsApp));
        }

        private List<TimeInterval> ConvertIntervalsToTakenDates(List<TimeInterval> intervals)
        {
            //TODO
            return new List<TimeInterval>();
        }

        private List<TimeInterval> CompactIntervals(List<TimeInterval> intervals)
        {
            List<TimeInterval> compactedIntervals = new List<TimeInterval>();

            int newIntervalCounter = 0;
            foreach (TimeInterval ti in intervals)
            {
                if (compactedIntervals.Count == 0)
                {
                    compactedIntervals.Add(ti);
                    continue;
                }

                TimeInterval newInterval = compactedIntervals[newIntervalCounter];

                if (newInterval.End.CompareTo(ti.Start) == 0)
                {
                    compactedIntervals[newIntervalCounter].End = ti.End;
                }
                else if (newInterval.End.CompareTo(ti.Start) < 0)
                {
                    compactedIntervals.Add(ti);
                    newIntervalCounter++;
                }
            }

            return compactedIntervals;
        }
    }
}