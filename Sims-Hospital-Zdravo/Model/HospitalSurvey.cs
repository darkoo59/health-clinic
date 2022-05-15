using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class HospitalSurvey
    {
        public int Kindness { get; set; }
        public int Professional { get; set; }
        public int Hygiene { get; set; }
        public int Application { get; set; }
        public int WaitingRoom { get; set; }

        public HospitalSurvey(int kindness, int professional, int hygiene, int application, int waitingRoom)
        {
            Kindness = kindness;
            Professional = professional;
            Hygiene = hygiene;
            Application = application;
            WaitingRoom = waitingRoom;
        }
    }
}
