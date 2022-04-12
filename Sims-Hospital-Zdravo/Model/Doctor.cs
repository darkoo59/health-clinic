/***********************************************************************
 * Module:  Doctor.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Doctor
 ***********************************************************************/

using System;
using static Enums.Enums;
namespace Model
{
   public class Doctor
   {
      private String Name;
      private String Surname;
      private int DoctorID;
      private DateTime BirthDate;
        

        public DateTime _BirthDate 
        {
            get { 
                return BirthDate; 
            }
            set { 
                BirthDate = value; 
            }
        }

        private String Email;
      private String Jmbg;
      private String PhoneNumber;

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
      
      public int _DoctorID
      {
         get
         {
            return DoctorID;
         }
         set
         {
            if (this.DoctorID != value)
               this.DoctorID = value;
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