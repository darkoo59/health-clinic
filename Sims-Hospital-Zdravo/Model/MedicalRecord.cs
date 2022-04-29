/***********************************************************************
 * Module:  Room.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;

public enum BloodType { APOSITIVE, ANEGATIVE, ABPOSITIVE, ABNEGATIVE, BPOSITIVE, BNEGATIVE, OPOSITIVE, ONEGATIVE };
public enum GenderType { MALE, FEMALE };
public enum MaritalType { MARRIED, UNMARRIED, DIVORCED, WIDOVER };
namespace Model
{
    public class MedicalRecord : INotifyPropertyChanged
    {
        private int Id;
        private Patient Patient;
        private List<String> Allergens;

        private GenderType Gender;
        private BloodType BloodType;
        private MaritalType MaritalStatus;

        public MedicalRecord(int id,Patient patient,GenderType gender,BloodType blood, MaritalType maritalStatus,List<String> allergens)
        {
            this._Id = id;
            this._Patient = patient;
            this._Gender = gender;
            this._BloodType = blood;
            this._MaritalStatus = maritalStatus;
            this._Allergens = allergens;
        }

        public Patient _Patient {
            get
            {
                return Patient;
            }
            set
            {
                if(this.Patient != value)
                {
                    this.Patient = value;
                    OnPropertyChanged();
                }
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
                {
                    this.Gender = value;
                    OnPropertyChanged();
                }
            }
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
                {
                    this.Id = value;
                    OnPropertyChanged();
                }
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
                {
                    this.BloodType = value;
                    OnPropertyChanged();
                }
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
                {
                    this.MaritalStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<String> _Allergens
        {
            get
            {
                return Allergens;
            }
            set
            {
                if(this.Allergens != value)
                {
                    this.Allergens = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}