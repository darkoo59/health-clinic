/***********************************************************************
 * Module:  Doctor.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Doctor
 ***********************************************************************/

using System;

public enum SpecialtyType
{
    ALLERGY,
    ANEESTHESIOLOGY,
    DERMATOLOGY,
    FAMILY_MEDICINE,
    NEUROLOGY,
    PEDIATRICS,
    UROLOGY,
    SURGERY,
    PSYCHIATRY,
    ITERNAL_MEDICINE
}

namespace Model
{
   public class Doctor : User
   {

        public SpecialtyType _specialty;
        public Doctor (int id,string name,string surname, SpecialtyType specialty)
        {
            this._Id = id;
            this._Name = name;
            this._Surname = surname;
            this.Specialty = specialty;
        }

        public SpecialtyType Specialty
        {
            get
            {
                return _specialty;
            }
            set
            {
                this._specialty = value;
            }
        }

        public override string ToString()
        {
            return _Name + " " +_Surname;
        }
    }
}