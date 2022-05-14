/***********************************************************************
 * Module:  Doctor.cs
 * Author:  stjep
 * Purpose: Definition of the Class Model.Doctor
 ***********************************************************************/

using System;
    public enum SpecaltyType{ALLERGY, ANEESTHESIOLOGY, DERMATOLOGY, FAMILY_MEDICINE, NEUROLOGY, PEDIATRICS, UROLOGY, SURGERY, PSYCHIATRY, ITERNAL_MEDICINE};
namespace Model
{
   public class Doctor : User
   {

        public SpecaltyType _Specialty;
        public Doctor ( int id,string name,string surname,SpecaltyType specaltyType)
        {
            this._Id = id;
            this._Name = name;
            this._Surname = surname;
            this._Specialty = specaltyType;
        }

        public override string ToString()
        {
            return _Name + " " +_Surname;
        }
    }
}