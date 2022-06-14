/***********************************************************************
 * Module:  Appointment.cs
 * Author:  I
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

using System;
using System.ComponentModel;
using System.Collections;
using System.Threading;
using Model;
//using Sims_Hospital_Zdravo.Model;

public enum AppointmentType { OPERATION, EXAMINATION, URGENCY };
namespace Model
{
    public class Appointment : INotifyPropertyChanged
    {

        private Room _room;

        private Doctor _doctor;
        private Patient _patient;
        private AppointmentType _type;
        
        private TimeInterval _time;
        private int _id;
        public static int GlobalId = 1;
        public bool Rated { get; set; }        
        public Appointment(Room room, Doctor doctor, Patient patient, TimeInterval time, AppointmentType type)
        {
            this.Doctor = doctor;
            this.Patient = patient;
            this.Room = room;
            this.Time = time;
            this.Type = type;
            this.Rated = false;
        }
        

        public Doctor Doctor {
            get
            {
                return _doctor;
            }
            set
            {
                this._doctor = value;
                OnPropertyChanged("Doctor");
                
            }
        }
        public Patient Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                this._patient = value;
                OnPropertyChanged("Patient");
                ;
            }
        }

        
        public AppointmentType Type
        {
            get
            {
                return _type;
            }
            set
            {
                this._type = value;
                OnPropertyChanged("Type");
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged("Id");
            }
        }
        public Room Room
        {
            get
            {
                return _room;
            }
            set
            {
                this._room = value;
                OnPropertyChanged("Room");
                
            }
        }

        public TimeInterval Time
        {
            get
            {
                return _time;
            }
            set
            {
                this._time = value;
                OnPropertyChanged("Time");
                
            }
        }


        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CheckIfTimeIntervalInAppointment(TimeInterval timeInterval)
        {
            if (this.Time.Start.CompareTo(timeInterval.Start) <= 0 && this.Time.End.CompareTo(timeInterval.Start) > 0)
            {
                return true;    
            }
            if (this.Time.Start.CompareTo(timeInterval.End) < 0 && this.Time.End.CompareTo(timeInterval.End) >= 0)
            {
                return true;
            }
            return false;
        }
    }
}