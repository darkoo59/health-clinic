/***********************************************************************
 * Module:  Room.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;
public enum BloodType { APOSITIVE, ANEGATIVE, ABPOSITIVE, ABNEGATIVE, BPOSITIVE, BNEGATIVE, OPOSITIVE, ONEGATIVE };
public enum GenderType { MALE, FEMALE };
public enum MaritalType { MARRIED, UNMARRIED, DIVORCED, WIDOVER };
namespace Model
{
    public class MedicalRecord
   {
        private Patient patient;

        private GenderType Gender;
        private BloodType BloodType;
        private MaritalType MaritalStatus;
        public Patient _Patient { get; set; }

        public GenderType _Gender { get; set; }
        public BloodType _BloodType { get; set; }
        public MaritalType _MaritalStatus { get; set; }
    }
}