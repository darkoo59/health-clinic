/***********************************************************************
 * Module:  Appointment.cs
 * Author:  I
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

using System;
using System.ComponentModel;

namespace Model
{
   public class Appointment : INotifyPropertyChanged
   {
      private DateTime DateAndTime;
      private int Room;
      private int DoctorId;
      private int Id;
      private int PatientId;
        public Appointment(int Id, int room, DateTime DateAndTime, int PatientId,int DoctorId) 
        {
            this._Id = Id;
            this._Room = room;
            this._DateAndTime = DateAndTime;
            this._PatientId = PatientId;
            this._DoctorId = DoctorId;
        }
      public DateTime _DateAndTime
      {
         get
         {
            return DateAndTime;
         }
         set
         {
                if (this.DateAndTime != value)
                {
                    this.DateAndTime = value;
                    OnPropertyChanged();
                }
         }
      }
      
      public int _Room
      {
         get
         {
            return Room;
         }
         set
         {
                if (this.Room != value)
                {
                    this.Room = value;

                }
         }
      }
      
      public int _DoctorId
      {
         get
         {
            return DoctorId;
         }
         set
         {
                if (this.DoctorId != value)
                {
                    this.DoctorId = value;
                    OnPropertyChanged();
                }
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
            if (this.Id != value)
               this.Id = value;
         }
      }
      
      public int _PatientId
      {
         get
         {
            return PatientId;
         }
         set
         {
            if (this.PatientId != value)
               this.PatientId = value;
         }
      }

        private void OnPropertyChanged(String name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}