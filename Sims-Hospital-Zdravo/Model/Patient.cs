/***********************************************************************
 * Module:  Patient.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using System;
using static Enums.Enums;
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