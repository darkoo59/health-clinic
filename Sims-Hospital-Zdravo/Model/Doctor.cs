/***********************************************************************
 * Module:  Doctor.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Doctor
 ***********************************************************************/

using System;

namespace Model
{
   public class Doctor : User
   {
        private String Specialty;

        public String _Specialty { get; set; }
        public Doctor ( int id,string name,string surname)
        {
            this._Id = id;
            this._Name = name;
            this._Surname = surname;
        }
    }
}