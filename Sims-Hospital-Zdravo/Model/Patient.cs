/***********************************************************************
 * Module:  Patient.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using Newtonsoft.Json;
using Sims_Hospital_Zdravo.Model;
using System;

namespace Model
{
    public class Patient : User
    {

        [JsonConstructor]
        public Patient( String name, String surname, DateTime birthDate, String email, String jmbg,
            String phoneNumber)
        {
            this._Name = name;
            this._Surname = surname;
            this._BirthDate = birthDate;
            this._Email = email;
            this._Jmbg = jmbg;
            this._PhoneNumber = phoneNumber;
        }

        public Patient(String name,String surname)
        {
            this._Name = name;
            this._Surname = surname;
            this._BirthDate = DateTime.Now;
            this._Email = "";
            this._Jmbg = "";
            this._PhoneNumber = "";
        }

        public override string ToString()
    {
        return _Name + " " + _Surname + " " + ",JMBG:"+_Jmbg;
    }
   }
}