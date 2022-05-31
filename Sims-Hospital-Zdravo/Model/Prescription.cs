using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Sims_Hospital_Zdravo.Model
{
    public class Prescription
    {
        private Medicine _medicine;
        private DateTime _prescriptionDate;
        private string _strength;
        private TimeInterval _timeInterval;
        private Doctor _doctor;
        private string _dosage;
        private int _numberOfDays;
        private bool _flag;


        public Prescription(Medicine medicine, DateTime prescriptionDate, string strength, TimeInterval timeInterval, Doctor doctor, string dosage, int numberOfDays)


        { 
            this._medicine = medicine;
            this._prescriptionDate = prescriptionDate;
            this._doctor = doctor;
            this._dosage = dosage;
            this._numberOfDays = numberOfDays;
            this._timeInterval = timeInterval;
            
            this.Flag = true;
            
        }

        public string Strength
        {
            get
            {
                return _strength;
            }
            set
            {
                _strength = value;
            }
        }
        public string Dosage
        {
            get
            {
                return _dosage;
            }
            set
            {
                _dosage = value;
            }
        }
        public Medicine Medicine
        {
            get
            {
                return _medicine;
            }
            set
            {
                _medicine = value;
            }
        }
        public TimeInterval TimeInterval
        {
            get
            {
                return _timeInterval;
            }
            set
            {
                _timeInterval = value;
            }
        }
        public DateTime _PrescriptionDate
        {
            get
            {
                return _prescriptionDate;
            }
            set
            {
                _prescriptionDate = value;
            }
        }
        public Doctor _Doctor

        
        {
            get
            {
                return _doctor;
            }
            set
            {
                _doctor = value;
                
            }
        }

        
        public int _NumberOfDays
        {
            get
            {
                return _numberOfDays;
            }
            set
            {
                _numberOfDays = value;
            }
        }
        public bool Flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
            }
        }

        public DateTime _StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

    }
}
