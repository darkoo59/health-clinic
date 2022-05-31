/***********************************************************************
 * Module:  Room.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.Room
 ***********************************************************************/

using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Model;
using Sims_Hospital_Zdravo.Model;

public enum BloodType { APOSITIVE, ANEGATIVE, ABPOSITIVE, ABNEGATIVE, BPOSITIVE, BNEGATIVE, OPOSITIVE, ONEGATIVE };
public enum GenderType { MALE, FEMALE };
public enum MaritalType { MARRIED, UNMARRIED, DIVORCED, WIDOVER };
namespace Model
{
    public class MedicalRecord : INotifyPropertyChanged
    {
        private int _id;
        private Patient _patient;

        private ObservableCollection<Prescription> _prescriptions;
        private ObservableCollection<Anamnesis> _anamnesis;

        private Allergens _patientAllergens;

        private GenderType _gender;
        private BloodType _bloodType;
        private MaritalType _maritalStatus;

        public MedicalRecord(int id,Patient patient,GenderType gender,BloodType blood, MaritalType maritalStatus,Allergens allergens)
        {
            this._id = id;
            this.Patient = patient;
            this.Gender = gender;
            this.BloodType = blood;
            this.MaritalStatus = maritalStatus;
            this._prescriptions = new ObservableCollection<Prescription>();
            this._anamnesis = new ObservableCollection<Anamnesis>();

            this.PatientAllergens = allergens;

        }

        public Patient Patient {
            get
            {
                return _patient;
            }
            set
            {
                if(this._patient != value)
                {
                    this._patient = value;
                    OnPropertyChanged();
                }
            }
        }

        public Allergens PatientAllergens
        {
            get
            {
                return _patientAllergens;
            }
            set
            {
                if (this._patientAllergens != value)
                {
                    this._patientAllergens = value;
                    OnPropertyChanged();
                }
            }
        }

        public GenderType Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if (this._gender != value)
                {
                    this._gender = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (this._id != value)
                {
                    this._id = value;
                    OnPropertyChanged();
                }
            }
        }

        public BloodType BloodType
        {
            get
            {
                return _bloodType;
            }
            set
            {
                if (this._bloodType != value)
                {
                    this._bloodType = value;
                    OnPropertyChanged();
                }
            }
        }

        public MaritalType MaritalStatus
        {
            get
            {
                return _maritalStatus;
            }
            set
            {
                if (this._maritalStatus != value)
                {
                    this._maritalStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Prescription> Prescriptions
        {
            get
            {
                return _prescriptions;
            }
            set
            {
                if(this._prescriptions != value)
                {
                    this._prescriptions = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Anamnesis> Anamnesis
        {
            get
            {
                return _anamnesis;
            }
            set
            {
                if (this._anamnesis != value)
                {
                    this._anamnesis = value;
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