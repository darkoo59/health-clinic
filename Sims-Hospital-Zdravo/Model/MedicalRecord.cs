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
        //private Patient Patient;


        private int Id;
        private int PatientId;
        private GenderType Gender { get; set; }
        private BloodType BloodType { get; set; }
        private MaritalType MaritalStatus { get; set; }

        public MedicalRecord(int id,int patientId, GenderType genderType, BloodType bloodType, MaritalType martialStatus)
        {
            this.Id = id;
            this.PatientId = patientId;
            this.BloodType = bloodType;
            this.Gender = genderType;
            this.MaritalStatus = martialStatus;
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

        public int _PatientId
        {
            get
            {
                return PatientId;
            }
            set
            {
                if (this.PatientId != value)
                    this.PatientId = value;
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