using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class HospitalSurvey
    {
        private int _kindness;
        private int _professional;
        private int _hygiene;
        private int _application;
        private int _waitingRoom;
        public HospitalSurvey(int kindness, int professional, int hygiene, int application, int waitingRoom)
        {
            _kindness = kindness;
            _professional = professional;
            _hygiene = hygiene;
            _application = application;
            _waitingRoom = waitingRoom;
        }
        public int Kindness { get { return _kindness; } set { this._kindness = value; } }
        public int Professional { get { return _professional; } set { this._professional = value; } }
        public int Hygiene { get { return _hygiene; } set { this._hygiene = value; } }
        public int Application { get { return _application; } set { this._application = value; } }
        public int WaitingRoom { get { return _waitingRoom; } set { this._waitingRoom = value; } }
    }
}
