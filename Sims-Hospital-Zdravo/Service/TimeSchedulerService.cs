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
using System.Collections.ObjectModel;

namespace Service
{
    public class TimeSchedulerService
    {
        private AppointmentRepository _appointmentRepository;
        private RenovationRepository _renovationRepository;
        private RelocationAppointmentRepository _relocationRepository;

        public TimeSchedulerService(AppointmentRepository appointmentRepository,
            RenovationRepository renovationRepository, RelocationAppointmentRepository relocationRepository)
        {
            this._appointmentRepository = appointmentRepository;
            this._renovationRepository = renovationRepository;
            this._relocationRepository = relocationRepository;
        }

        public List<TimeInterval> FindReservedTimeForRooms(Room room1, Room room2)
        {
            List<TimeInterval> takenIntervalsRoom1 = CaptureAllTakenIntervalsForRoom(room1.Id);
            List<TimeInterval> takenIntervalsRoom2 = CaptureAllTakenIntervalsForRoom(room2.Id);
            List<TimeInterval> takenIntervals = new List<TimeInterval>(takenIntervalsRoom1.Concat(takenIntervalsRoom2));

            takenIntervals = takenIntervals.OrderBy(o => o.Start).ToList();
            takenIntervals = CompactIntervals(takenIntervals, IntervalsTouching, IsThereGapInIntervals);
            return takenIntervals;
        }

        public List<TimeInterval> FindReservedDatesForRooms(List<Room> rooms)
        {
            List<TimeInterval> takenIntervals = CaptureAllTakenIntervalsForRooms(rooms);
            takenIntervals = takenIntervals.OrderBy(o => o.Start).ToList();

            takenIntervals = CompactIntervals(takenIntervals, IntervalsTouching, IsThereGapInIntervals);
            takenIntervals = ConvertIntervalsToTakenDates(takenIntervals);

            return takenIntervals;
        }

        public List<TimeInterval> FindReservedDatesForRoom(Room room)
        {
            List<TimeInterval> takenIntervals = CaptureAllTakenIntervalsForRoom(room.Id);
            takenIntervals = takenIntervals.OrderBy(o => o.Start).ToList();

            takenIntervals = CompactIntervals(takenIntervals, IntervalsTouching, IsThereGapInIntervals);
            takenIntervals = ConvertIntervalsToTakenDates(takenIntervals);

            return takenIntervals;
        }

        public bool IsRoomFreeInInterval(int roomId, TimeInterval ti)
        {
            List<Appointment> appointments = _appointmentRepository.FindByRoomId(roomId);
            foreach (Appointment app in appointments)
            {
                DateTime start = app._Time.Start;
                DateTime end = app._Time.End;
                if (start.CompareTo(ti.Start) < 0 && end.CompareTo(ti.Start) > 0) return false;
                if (start.CompareTo(ti.End) < 0 && end.CompareTo(ti.End) > 0) return false;
                if (start.CompareTo(ti.Start) > 0 && end.CompareTo(ti.End) < 0) return false;
                if (start.CompareTo(ti.Start) == 0 && end.CompareTo(ti.End) == 0) return false;
            }

            return true;
        }

        public bool IsRoomFreeInDateInterval(int roomId, TimeInterval ti)
        {
            List<TimeInterval> intervals = CaptureAllTakenIntervalsForRoom(roomId);
            intervals = intervals.OrderBy(o => o.Start).ToList();
            intervals = CompactIntervals(intervals, IntervalsTouching, IsThereGapInIntervals);

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

        public bool IsDoctorFreeInInterval(int doctorId, TimeInterval ti)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByDoctorId(doctorId);
            foreach (Appointment app in appointments)
            {
                DateTime start = app._Time.Start;
                DateTime end = app._Time.End;
                if (start.CompareTo(ti.Start) < 0 && end.CompareTo(ti.Start) > 0) return false;
                if (start.CompareTo(ti.End) < 0 && end.CompareTo(ti.End) > 0) return false;
                if (start.CompareTo(ti.Start) > 0 && end.CompareTo(ti.End) < 0) return false;
                if (start.CompareTo(ti.Start) == 0 || end.CompareTo(ti.End) == 0) return false;
            }

            return true;
        }

        public bool IsDoctorFreeInIntervalWithoutSelectedAppointment(int doctorId, Appointment appointment)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByDoctorId(doctorId);
            foreach (Appointment app in appointments)
            {
                if (app._Id != appointment._Id)
                {
                    DateTime start = app._Time.Start;
                    DateTime end = app._Time.End;
                    if (start.CompareTo(appointment._Time.Start) < 0 && end.CompareTo(appointment._Time.Start) > 0) return false;
                    if (start.CompareTo(appointment._Time.End) < 0 && end.CompareTo(appointment._Time.End) > 0) return false;
                    if (start.CompareTo(appointment._Time.Start) > 0 && end.CompareTo(appointment._Time.End) < 0) return false;
                    if (start.CompareTo(appointment._Time.Start) == 0 && end.CompareTo(appointment._Time.End) == 0) return false;
                }
            }

            return true;
        }

        public bool IsPatientFreeInInterval(int patientId, TimeInterval ti)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByPatientId(patientId);
            foreach (Appointment app in appointments)
            {
                DateTime start = app._Time.Start;
                DateTime end = app._Time.End;
                if (start.CompareTo(ti.Start) < 0 && end.CompareTo(ti.Start) > 0) return false;
                if (start.CompareTo(ti.End) < 0 && end.CompareTo(ti.End) > 0) return false;
                if (start.CompareTo(ti.Start) > 0 && end.CompareTo(ti.End) < 0) return false;
                if (start.CompareTo(ti.Start) == 0 && end.CompareTo(ti.End) == 0) return false;
                if (start.CompareTo(ti.Start) == 0 || end.CompareTo(ti.End) == 0) return false;
            }

            return true;
        }

        public bool IsPatientFreeInIntervalWithoutSelectedAppointment(int patientId, Appointment appointment)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByPatientId(patientId);
            foreach (Appointment app in appointments)
            {
                if (app._Id != appointment._Id)
                {
                    DateTime start = app._Time.Start;
                    DateTime end = app._Time.End;
                    if (start.CompareTo(appointment._Time.Start) < 0 && end.CompareTo(appointment._Time.Start) > 0) return false;
                    if (start.CompareTo(appointment._Time.End) < 0 && end.CompareTo(appointment._Time.End) > 0) return false;
                    if (start.CompareTo(appointment._Time.Start) > 0 && end.CompareTo(appointment._Time.End) < 0) return false;
                    if (start.CompareTo(appointment._Time.Start) == 0 && end.CompareTo(appointment._Time.End) == 0) return false;
                }
            }

            return true;
        }

        private List<TimeInterval> CaptureAllTakenIntervalsForRooms(List<Room> rooms)
        {
            List<TimeInterval> timeIntervals = new List<TimeInterval>();
            foreach (Room room in rooms)
            {
                timeIntervals.AddRange(CaptureAllTakenIntervalsForRoom(room.Id));
            }

            return timeIntervals;
        }

        private List<TimeInterval> CaptureAllTakenIntervalsForRoom(int roomId)
        {
            List<TimeInterval> takenIntervalsApp = _appointmentRepository.GetTimeIntervalsForRoom(new Room(-1, roomId, 0, "", 10));
            List<TimeInterval> takenIntervalRenovation = _renovationRepository.FindTakenIntervalsForRoom(roomId);
            List<TimeInterval> takenIntervalRelocation = _relocationRepository.FindTakenIntervalsForRoom(roomId);

            return new List<TimeInterval>(takenIntervalRelocation.Concat(takenIntervalRenovation).Concat(takenIntervalsApp));
        }

        private List<TimeInterval> ConvertIntervalsToTakenDates(List<TimeInterval> intervals)
        {
            List<TimeInterval> dates = new List<TimeInterval>(intervals.Select(x => new TimeInterval(x.Start.Date, x.End.Date)));
            dates = CompactIntervals(dates, IsSameOrNextDate, IsThereGapInDates);
            return dates;
        }

        private List<TimeInterval> CompactIntervals(List<TimeInterval> dateIntervals, Func<TimeInterval, TimeInterval, bool> condition1, Func<TimeInterval, TimeInterval, bool> condition2)
        {
            List<TimeInterval> compactedIntervals = new List<TimeInterval>();

            int newIntervalCounter = 0;

            foreach (TimeInterval dateInterval in dateIntervals)
            {
                if (compactedIntervals.Count == 0)
                {
                    compactedIntervals.Add(dateInterval);
                    continue;
                }

                TimeInterval timeInterval = compactedIntervals[newIntervalCounter];
                if (condition1(timeInterval, dateInterval))
                {
                    compactedIntervals[newIntervalCounter].End = dateInterval.End;
                }

                else if (condition2(timeInterval, dateInterval))
                {
                    compactedIntervals.Add(dateInterval);
                    newIntervalCounter++;
                }
            }

            return compactedIntervals;
        }

        public Appointment FindAppointmentByDate(DateTime date, int id, Patient pat)
        {
            foreach (Appointment app in _appointmentRepository.FindByDoctorId(id))
            {
                if (app._Time.Start.Date.Equals(date.Date))
                {
                    if (app._Patient._Jmbg.Equals(pat._Jmbg))
                    {
                        return app;
                    }
                }
            }

            return null;
        }

        private bool IsSameOrNextDate(TimeInterval timeInterval, TimeInterval dateInterval)
        {
            return timeInterval.End.CompareTo(dateInterval.Start) == 0 || timeInterval.End.AddDays(1).CompareTo(dateInterval.Start) == 0;
        }

        private bool IsThereGapInDates(TimeInterval baseInterval, TimeInterval newInterval)
        {
            return baseInterval.End.AddDays(1).CompareTo(newInterval.Start) < 0;
        }

        private bool IsThereGapInIntervals(TimeInterval baseInterval, TimeInterval newInterval)
        {
            return baseInterval.End.CompareTo(newInterval.Start) < 0;
        }

        private bool IntervalsTouching(TimeInterval baseInterval, TimeInterval newInterval)
        {
            return baseInterval.End.CompareTo(newInterval.Start) == 0;
        }
    }
}