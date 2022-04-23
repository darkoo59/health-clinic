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
        private int Id;
        private Patient Patient;

        private GenderType Gender;
        private BloodType BloodType;
        private MaritalType MaritalStatus;

        public MedicalRecord(int id,Patient patient,GenderType gender,BloodType blood, MaritalType maritalStatus)
        {
            this._Id = id;
            this._Patient = patient;
            this._Gender = gender;
            this._BloodType = blood;
            this._MaritalStatus = maritalStatus;
        }

        public int _Id { get; set; }
        public Patient _Patient { get; set; }

        public GenderType _Gender { get; set; }
        public BloodType _BloodType { get; set; }
        public MaritalType _MaritalStatus { get; set; }
    }
}