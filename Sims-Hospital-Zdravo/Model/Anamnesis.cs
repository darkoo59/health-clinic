﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ComponentModel;
namespace Sims_Hospital_Zdravo.Model
{
    public class Anamnesis : INotifyPropertyChanged
    {

        private Doctor doctor;
        private MedicalRecord medicalRecord;
        private DateTime date;
        private TimeInterval timeInterval;
        private string Diagnosis;
        private string Anamensis;
        private int anamnesisID;

        public Anamnesis(Doctor doctor, MedicalRecord medicalRecord, DateTime date, TimeInterval timeInterval, string diagnosis, string anamensis)
        {
            this.doctor = doctor;
            this.medicalRecord = medicalRecord;
            this.date = date;
            this.timeInterval = timeInterval;
            Diagnosis = diagnosis;
            Anamensis = anamensis;
        }

        public Doctor _Doctor
        {
            get { 
                return doctor; 
                }


            set {
                doctor = value;
                OnPropertyChanged("_Doctor");
            }

        }
        public DateTime _Date
        {
            get
            {
                return date;
            }


            set
            {
                date = value;
                OnPropertyChanged("_Date");
            }

        }

        public MedicalRecord _MedicalRecord
        {
            get
            {
                return medicalRecord;
            }


            set
            {
                medicalRecord = value;
                OnPropertyChanged("_MedicalRecord");
            }

        }
        public TimeInterval _TimeInterval
        {
            get
            {
                return timeInterval;
            }


            set
            {
                timeInterval = value;
            }

        }
        public string _Diagnosis
        {
            get
            {
                return Diagnosis;
            }


            set
            {
                Diagnosis = value;
                OnPropertyChanged("_Diagnosis");
            }

        }
        public string _Anamensis
        {
            get
            {
                return Anamensis;
            }
            set
            {
                Anamensis = value;
                OnPropertyChanged("_Anamnesis");
            }
        }
        public int _AnamnesisID
        {
            get
            {
                return anamnesisID;
            }
            set
            {
                anamnesisID = value;
            }
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}