/***********************************************************************
 * Module:  Room.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;
using static Enums.Enums;

namespace Model
{
   public class MedicalRecord
   {
        private Patient Patient;
        private GenderType Gender { get; set; }
        private BloodType BloodType { get; set; }
        private MaritalType MaritalStatus { get; set; }
   
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