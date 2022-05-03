/***********************************************************************
 * Module:  Patient.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using Sims_Hospital_Zdravo.Model;
using System;

namespace Model
{
    public class Patient : User
    {
        private Allergens allergens;

        public Patient(int id, String name, String surname, DateTime birthDate, String email, String jmbg,
            String phoneNumber)
        {
            this._Id = id;
            this._Name = name;
            this._Surname = surname;
            this._BirthDate = birthDate;
            this._Email = email;
            this._Jmbg = jmbg;
            this._PhoneNumber = phoneNumber;
        }

        public Allergens _Allergens
        {
            get { return allergens; }
            set { value = allergens; }

        }

        public override string ToString()
    {
        return _Name + " " + _Surname + " " + ",JMBG:"+_Jmbg;
    }
   }
}