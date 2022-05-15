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
        public int Kindness { get; set; }
        public int Professional { get; set; }
        public int RoomHygiene { get; set; }
        public int Waiting { get; set; }
        public int ObtainingInformation { get; set; }
        public Appointment Appointment { get; set; }

        public DoctorSurvey(Appointment appointment, int kindness, int professional, int roomHygiene, int waiting, int obtainingInformation)
        {
            Appointment = appointment;
            Kindness = kindness;
            Professional = professional;
            RoomHygiene = roomHygiene;
            Waiting = waiting;
            ObtainingInformation = obtainingInformation;
        }
    }
}
