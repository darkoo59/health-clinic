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

        private Room room;

        private Doctor doctor;
        private Patient patient;
        private AppointmentType type;
        private DateTime DateAndTime;
        private TimeInterval time;
        private int Id;
        public static int GlobalId = 1;
        public Appointment(Room room, Doctor doctor, Patient patient, TimeInterval time, AppointmentType type)
        {
            this._Doctor = doctor;
            this._Patient = patient;
            this._Room = room;
            this._Time = time;
            this._Type = type;
        }
        //public Room _Room { get; set ; }

        public Doctor _Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                this.doctor = value;
                OnPropertyChanged("_Doctor");
            }
        }
        public Patient _Patient
        {
            get
            {
                return patient;
            }
            set
            {
                this.patient = value;
                OnPropertyChanged("_Patient");
                OnPropertyChanged("_Patient._Name");
                OnPropertyChanged("_Patient._Surname");
            }
        }

        public DateTime _DateAndTime
        {
            get
            {
                return DateAndTime;
            }
            set
            {
                this.DateAndTime = value;
                OnPropertyChanged("_DateAndTime");
            }
        }
        public AppointmentType _Type
        {
            get
            {
                return type;
            }
            set
            {
                this.type = value;
                OnPropertyChanged("_DateAndTime");
            }
        }
        public int _Id
        {
            get
            {
                return Id;
            }
            set
            {
                this.Id = value;
                OnPropertyChanged("_Id");
            }
        }
        public Room _Room
        {
            get
            {
                return room;
            }
            set
            {
                this.room = value;
                OnPropertyChanged("_Room");
                OnPropertyChanged("_Id");
            }
        }

        public TimeInterval _Time
        {
            get
            {
                return time;
            }
            set
            {
                this.time = value;
                OnPropertyChanged("_Time");
                //OnPropertyChanged("_Id");
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
    }
}