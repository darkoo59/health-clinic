/***********************************************************************
 * Module:  Room.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;

namespace Model
{
   public class MedicalRecord
   {
      private Patient Patient;
      private GenderType Gender;
      private BloodType BloodType;
      private MaritalType MaritalStatus;
   
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
      
      public GenderType _Gender
      {
         get
         {
            return Gender;
         }
         set
         {
            if (this.Gender != value)
               this.Gender = value;
         }
      }
      
      public BloodType _BloodType
      {
         get
         {
            return BloodType;
         }
         set
         {
            if (this.BloodType != value)
               this.BloodType = value;
         }
      }
      
      public MaritalType _MaritalStatus
      {
         get
         {
            return MaritalStatus;
         }
         set
         {
            if (this.MaritalStatus != value)
               this.MaritalStatus = value;
         }
      }
   
   }
}