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
        
        private Room room;

        private Doctor doctor;
        private Patient patient;

        private DateTime DateAndTime;
        private int Id;

        public Appointment(Room room, Doctor doctor,  Patient patient, DateTime dateAndTime, int id)
        {
            this._Room = room;
            this._Doctor = doctor;
            this._Patient = patient;
            this._DateAndTime = dateAndTime;
            this._Id = id;
        }

        public Room _Room { get; set; }

        public Doctor _Doctor { get; set; }
        public Patient _Patient { get; set; }

        public DateTime _DateAndTime { get; set; }
        public int _Id { get; set; }
    }
}