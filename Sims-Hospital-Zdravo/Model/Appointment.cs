/***********************************************************************
 * Module:  Appointment.cs
 * Author:  I
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

using System;
using System.ComponentModel;
using System.Collections;
using System.Threading;
namespace Model
{
   public class Appointment : INotifyPropertyChanged
   {
        
        private Room room;
        
        private Doctor doctor;
        private Patient patient;

        private DateTime DateAndTime;
        private int Id;
        public static int GlobalId=1;
        public Appointment(Room room, Doctor doctor,  Patient patient, DateTime dateAndTime)
        {
            this._Room = room;
            this._Doctor = doctor;
            this._Patient = patient;
            this._DateAndTime = dateAndTime;
            
        }
       /* public Appointment(Doctor doctor, Patient patient, DateTime dateAndTime, int id)
        {
            this._Doctor = doctor;
            this._Patient = patient;
            this._DateAndTime = dateAndTime;
            this._Id = id;
        }*/
        //public Room _Room { get; set ; }

        public Doctor _Doctor {
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
        public Patient _Patient {
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

        public DateTime _DateAndTime {
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
        public int _Id {
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
        public Room _Room {
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

        

        private void OnPropertyChanged(string name )
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