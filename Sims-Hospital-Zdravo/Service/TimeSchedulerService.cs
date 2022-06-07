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
            List<TimeInterval> takenIntervals = CaptureAllTakenIntervalsForRoom(roomId);
            return takenIntervals.All(interval => !interval.IsOverlaping(ti));
        }

        public bool IsRoomFreeInDateInterval(int roomId, TimeInterval ti)
        {
            List<TimeInterval> intervals = CaptureAllTakenIntervalsForRoom(roomId);
            intervals = intervals.OrderBy(o => o.Start).ToList();
            intervals = CompactIntervals(intervals, IntervalsTouching, IsThereGapInIntervals);
            return intervals.All(x => !x.IsOverlaping(ti));
        }


        public bool IsDoctorFreeInInterval(int doctorId, TimeInterval ti)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByDoctorId(doctorId);
            return appointments.All(app => !app.Time.IsOverlaping(ti));
        }

        public bool IsDoctorFreeInIntervalWithoutSelectedAppointment(int doctorId, Appointment appointment)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByDoctorId(doctorId);
            return appointments.Where(app => app.Id != appointment.Id)
                .All(app => !app.Time.IsOverlaping(appointment.Time));
        }

        public bool IsPatientFreeInInterval(int patientId, TimeInterval ti)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByPatientId(patientId);
            return appointments.All(app => !app.Time.IsOverlaping(ti));
        }

        public bool IsPatientFreeInIntervalWithoutSelectedAppointment(int patientId, Appointment appointment)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByPatientId(patientId);
            return appointments.Where(app => app.Id != appointment.Id)
                .All(app => !app.Time.IsOverlaping(appointment.Time));
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

        public TimeInterval CheckifDurationIsLongEnough(Doctor doctor, double duration)
        {
            List<TimeInterval> freeTimeIntervals = FreeTimeIntervalsForDoctor(doctor);
            return (from interval in freeTimeIntervals where interval.IsLongerThanDuration(duration)
                select new TimeInterval(interval.Start, interval.Start.AddHours(duration))).FirstOrDefault();
        }




        public TimeInterval FindIntervalForOperation(Appointment appointment, double duration)
        {
            if (IsDoctorFreeInInterval(appointment.Doctor.Id, appointment.Time))
            {
                return appointment.Time;
            }
            return MakeAppointmentForSurgery(appointment, duration);
        }


        public TimeInterval MakeAppointmentForSurgery(Appointment appointment, double duration)
        {
            if (CheckifDurationIsLongEnough(appointment.Doctor, duration) != null)
            {
                return CheckifDurationIsLongEnough(appointment.Doctor, duration);
            }
            CancelAppointmentsForOperation(appointment);
            return appointment.Time;
        }


        public void CancelAppointmentsForOperation(Appointment appointment)
        {
            ObservableCollection<Appointment> appointments = _appointmentRepository.FindByDoctorId(appointment.Doctor.Id);
            List<Appointment> appointmentsToDelete = appointments.Where(i => i.Time.Start >= appointment.Time.Start && i.Time.End <= appointment.Time.End).ToList();
            foreach (Appointment appToDelete in appointmentsToDelete)
            {
                appointments.Remove(appToDelete);
            }
        }
        public List<TimeInterval> FreeTimeIntervalsForDoctor(Doctor doctor)
        {
            List<TimeInterval> orderedFreeTimeIntervals = GetFreeTimeIntervalsDoctor(doctor);
            AddTimeIntervalFromStartTime(GetOrderedIntervalsForDoctor(doctor), orderedFreeTimeIntervals);
            AddTimeIntervalFromEndTime(GetOrderedIntervalsForDoctor(doctor),orderedFreeTimeIntervals);
            return orderedFreeTimeIntervals;

        }
        public List<TimeInterval> GetOrderedIntervalsForDoctor(Doctor doctor)
        {
            List<TimeInterval> takenIntervals = _appointmentRepository.GetTimeIntervalsForDoctor(doctor.Id);
            var orderedAppointment = takenIntervals.OrderBy(a => a.Start).ToArray();
            return orderedAppointment.ToList();

        }
        public List<TimeInterval> GetFreeTimeIntervalsDoctor(Doctor doctor)
        {
            List<TimeInterval> freeTimeIntervals = new List<TimeInterval>();
            var orderedAppointment = GetOrderedIntervalsForDoctor(doctor).ToArray();
            for (int i = 0; i < orderedAppointment.Length - 1; i++)
            {
                if (!(orderedAppointment[i].End == orderedAppointment[i + 1].Start))
                    freeTimeIntervals.Add(new TimeInterval(orderedAppointment[i].End, orderedAppointment[i + 1].Start));
            }
            return freeTimeIntervals;
        }

        
        public void AddTimeIntervalFromStartTime(List<TimeInterval> orderedTimeIntervals,List<TimeInterval> freeIntervals)
        {

            TimeInterval firstAppointment = orderedTimeIntervals.First();
            string dateAppointment = firstAppointment.Start.Date.ToShortDateString();
            DateTime dateTimeStart = DateTime.Parse(dateAppointment + " " + "8:00");
            if (firstAppointment.Start.Hour > 8)
                freeIntervals.Add(new TimeInterval(dateTimeStart, firstAppointment.Start));
            
        }
        public void AddTimeIntervalFromEndTime(List<TimeInterval> orderedTimeIntervals,List<TimeInterval> freeIntervals)
        {
            TimeInterval lastAppointment = orderedTimeIntervals.Last();
            string dateAppointment = lastAppointment.Start.Date.ToShortDateString();
            DateTime dateTimeEnd = DateTime.Parse(dateAppointment + " " + "21:00");
            if (lastAppointment.End.Hour < 21)
                freeIntervals.Add(new TimeInterval(lastAppointment.End, dateTimeEnd));
        }


            private List<TimeInterval> CaptureAllTakenIntervalsForRoom(int roomId)
        {
            List<TimeInterval> takenIntervalsApp = _appointmentRepository.GetTimeIntervalsForRoom(roomId);
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
                if (app.Time.Start.Date.Equals(date.Date))
                {
                    if (app.Patient.Jmbg.Equals(pat.Jmbg))
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