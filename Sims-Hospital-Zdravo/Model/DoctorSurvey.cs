using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class DoctorSurvey
    {
        private int _kindness;
        private int _doctor;
        private int _roomHygiene;
        private int _waiting;
        private int _obtainingInformation;
        private Appointment _appointment;

        public DoctorSurvey(Appointment appointment, int kindness, int roomHygiene, int waiting, int obtainingInformation, int doctor)
        {
            _appointment = appointment;
            _kindness = kindness;
            _doctor = doctor;
            _roomHygiene = roomHygiene;
            _waiting = waiting;
            _obtainingInformation = obtainingInformation;
        }
        public int Kindness { get { return _kindness; } set{ this._kindness = value; } }
        public int Doctor { get { return _doctor; } set { this._doctor = value; } }
        public int RoomHygiene { get { return _roomHygiene; } set { this._roomHygiene = value; } }
        public int Waiting { get { return _waiting; } set { this._waiting = value; } }
        public int ObtainingInformation { get { return _obtainingInformation; } set { this._obtainingInformation = value; } }
        public Appointment Appointment { get { return _appointment; } set { this._appointment = value; } }
    }
}
