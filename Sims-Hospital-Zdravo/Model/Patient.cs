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
            
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Email = email;
            this.Jmbg = jmbg;
            this.PhoneNumber = phoneNumber;
        }

        public Patient(String name,String surname)
        {
            
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = DateTime.Now;
            this.Email = "";
            this.Jmbg = "";
            this.PhoneNumber = "";
        }

        public override string ToString()
    {
        return Name + " " + Surname + " " + ",JMBG:"+Jmbg;
    }
   }
}