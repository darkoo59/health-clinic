using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using Model;
namespace Sims_Hospital_Zdravo.Model
{
    public class LabaratoryTest
    {
        private String _patient;
        private string _diagnosis;
        private DateTime _date;

        public LabaratoryTest(String patient, string diagnosis, DateTime date)
        {
            _patient = patient;
            _diagnosis = diagnosis;
            _date = date;
        }

        public string Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }
        public string Diagnosis
        {
            get
            {
                return _diagnosis;
            }
            set
            {
                _diagnosis = value;
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }
    }
}
