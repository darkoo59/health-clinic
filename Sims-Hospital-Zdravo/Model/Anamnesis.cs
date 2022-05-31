using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.ComponentModel;
namespace Sims_Hospital_Zdravo.Model
{
    public class Anamnesis : INotifyPropertyChanged
    {
        private Patient _patient;
        private Doctor _doctor;
        private DateTime _date;
        private TimeInterval _timeInterval;
        private string _diagnosis;
        private string _anamensis;
        private string _notes;

        public Anamnesis(Doctor doctor,Patient patient,  DateTime date, TimeInterval timeInterval, string diagnosis, string anamensis)
        {
            this._doctor = doctor;
            this._patient = patient;
            this._date = date;
            this._timeInterval = timeInterval;
            this._diagnosis = diagnosis;
            this._anamensis = anamensis;
            Notes = "";
        }
        
        public Patient Patient
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

        public Doctor Doctor
        {
            get { 
                return _doctor; 
                }


            set {
                _doctor = value;
                OnPropertyChanged("Doctor");
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
                OnPropertyChanged("Date");
            }

        }

        
        public TimeInterval _TimeInterval
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
        public string Diagnosis
        {
            get
            {
                return _diagnosis;
            }


            set
            {
                _diagnosis = value;
                OnPropertyChanged("Diagnosis");
            }

        }
        public string Anamensis
        {
            get
            {
                return _anamensis;
            }
            set
            {
                _anamensis = value;
                OnPropertyChanged("Anamnesis");
            }
        }
        
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
            }
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
