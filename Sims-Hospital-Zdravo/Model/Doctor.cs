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

        public String _Specialty;
        public Doctor ( int id,string name,string surname)
        {
            this._Id = id;
            this._Name = name;
            this._Surname = surname;
        }

        public override string ToString()
        {
            return _Name + " " +_Surname;
        }
    }
}