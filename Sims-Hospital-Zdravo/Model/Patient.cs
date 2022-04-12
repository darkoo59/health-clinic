/***********************************************************************
 * Module:  Patient.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using System;
namespace Model
{
   public class Patient
   {
      private int Id;
      private String Name;
      private String Surname;
      private DateTime BirthDate;
      private String Email;
      private String Jmbg;
      private String PhoneNumber;

        public Patient(int id, String name, String surname, DateTime birthDate, String email, String jmbg, String phoneNumber)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Email = email;
            this.Jmbg = jmbg;
            this.PhoneNumber = phoneNumber;
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
      
      public String _Name
      {
         get
         {
            return Name;
         }
         set
         {
            if (this.Name != value)
               this.Name = value;
         }
      }
      
      public String _Surname
      {
         get
         {
            return Surname;
         }
         set
         {
            if (this.Surname != value)
               this.Surname = value;
         }
      }
      
      public DateTime _BirthDate
      {
         get
         {
            return BirthDate;
         }
         set
         {
            if (this.BirthDate != value)
               this.BirthDate = value;
         }
      }
      
      public String _Email
      {
         get
         {
            return Email;
         }
         set
         {
            if (this.Email != value)
               this.Email = value;
         }
      }
      
      public String _Jmbg
      {
         get
         {
            return Jmbg;
         }
         set
         {
            if (this.Jmbg != value)
               this.Jmbg = value;
         }
      }
      
      public String _PhoneNumber
      {
         get
         {
            return PhoneNumber;
         }
         set
         {
            if (this.PhoneNumber != value)
               this.PhoneNumber = value;
         }
      }
   
   }
}