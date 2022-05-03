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
        private Medicine medicine;
        private DateTime prescriptionDate;
        private string strength;
        private TimeInterval timeInterval;
        private Doctor doctor;
        private string dosage;
        private int numberOfDays;
        private bool flag;


        public Prescription(Medicine medicine, DateTime prescriptionDate, string strength, TimeInterval timeInterval, Doctor doctor, string dosage, int numberOfDays)


        { 
            this.medicine = medicine;
            this.prescriptionDate = prescriptionDate;
            this.doctor = doctor;
            this.dosage = dosage;
            this.numberOfDays = numberOfDays;
            this.timeInterval = timeInterval;
            
            this._Flag = true;
            
        }

        public string _Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
            }
        }
        public string _Dosage
        {
            get
            {
                return dosage;
            }
            set
            {
                dosage = value;
            }
        }
        public Medicine _Medicine
        {
            get
            {
                return medicine;
            }
            set
            {
                medicine = value;
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
        public DateTime _PrescriptionDate
        {
            get
            {
                return prescriptionDate;
            }
            set
            {
                prescriptionDate = value;
            }
        }
        public Doctor _Doctor

        
        {
            get
            {
                return doctor;
            }
            set
            {
                doctor = value;
                
            }
        }

        
        public int _NumberOfDays
        {
            get
            {
                return numberOfDays;
            }
            set
            {
                numberOfDays = value;
            }
        }
        public bool _Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }
        


    }
}
