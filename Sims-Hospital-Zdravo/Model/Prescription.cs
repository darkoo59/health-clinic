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
        private MedicalRecord medical;
        private Doctor doctor;

        public Prescription(Medicine medicine, DateTime prescriptionDate, MedicalRecord medical, Doctor doctor)
        {
            this.medicine = medicine;
            this.prescriptionDate = prescriptionDate;
            this.medical = medical;
            this.doctor = doctor;
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
        public MedicalRecord _MedicalRecord
        {
            get
            {
                return medical;
            }
            set
            {
                medical = value;

            }
        }
    }
}
