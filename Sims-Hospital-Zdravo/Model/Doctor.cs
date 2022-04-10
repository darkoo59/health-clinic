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
      private Specialty Specialty { get; set; }
   
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
   
   }
}