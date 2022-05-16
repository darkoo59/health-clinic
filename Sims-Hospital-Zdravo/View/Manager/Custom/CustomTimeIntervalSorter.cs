using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.View.Manager.Custom
{
    public class CustomTimeIntervalSorter : IComparer
    {
        private int direction;

        public CustomTimeIntervalSorter(int direction)
        {
            this.direction = direction;
        }

        public int Compare(object x, object y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, x)) return -1 * direction;
            if (ReferenceEquals(null, y)) return 1 * direction;


            RenovationAppointment renx = x as RenovationAppointment;
            RenovationAppointment reny = y as RenovationAppointment;

            int comparedStarts = renx.Time.Start.CompareTo(reny.Time.Start);
            int comparedEnds = renx.Time.End.CompareTo(reny.Time.End);

            if (comparedStarts != 0) return comparedStarts * direction;
            return comparedEnds * direction;
        }
    }
}