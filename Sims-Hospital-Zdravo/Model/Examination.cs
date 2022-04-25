using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{

    public enum ExaminationType { OPERATION, CHECKUP};
    class Examination
    {
        private static int idCount = 0;
        private int ExaminationId;
        private ExaminationType ExaminationType;
        private Doctor Doctor;
        private Patient Patient;
        private Room Room;
        private TimeInterval ExaminationDate;

        public Examination(Doctor doctor,Patient patient,Room room,TimeInterval date, ExaminationType type)
        {
            this.Doctor = doctor;
            this.Patient = patient;
            this.Room = room;
            this.ExaminationDate = date;
            this.ExaminationType = type;
            this.ExaminationId = ++(idCount);
        }

        public int _ExaminationId
        {
            get { return ExaminationId; }
            set { this.ExaminationId = value; }
        }

        public ExaminationType _ExaminationType
        {
            get { return ExaminationType; }
            set { this.ExaminationType = value; }
        }

        public Doctor _Doctor
        {
            get { return Doctor; }
            set { this.Doctor = value; }
        }

        public Patient _Patient
        {
            get { return Patient; }
            set { this.Patient = value; }
        }

        public Room _Room
        {
            get { return Room; }
            set { this.Room = value; }
        }

        public TimeInterval _ExaminationDate
        {
            get { return ExaminationDate; }
            set { this.ExaminationDate = value; }
        }



    }
}
