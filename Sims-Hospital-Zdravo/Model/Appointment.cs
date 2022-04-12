/***********************************************************************
 * Module:  Appointment.cs
 * Author:  I
 * Purpose: Definition of the Class Model.Appointment
 ***********************************************************************/

using System;
namespace Model
{
   public class Appointment
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
               this.DateAndTime = value;
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
               this.Room = value;
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
               this.DoctorId = value;
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

        public static implicit operator ArrayList(Appointment v)
        {
            throw new NotImplementedException();
        }
    }
}