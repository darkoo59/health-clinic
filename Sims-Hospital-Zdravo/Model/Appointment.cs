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
      private Doctor Doctor;
      private int Id;
      private Patient Patient;
   
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
      
      public Doctor _Doctor
      {
         get
         {
            return Doctor;
         }
         set
         {
            if (this.Doctor != value)
               this.Doctor = value;
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
      
      public Patient _Patient
      {
         get
         {
            return Patient;
         }
         set
         {
            if (this.Patient != value)
               this.Patient = value;
         }
      }
   
   }
}