using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class QuestionForSurvey : INotifyPropertyChanged
    {
        private string _text;
        private bool _one;
        private bool _two;
        private bool _three;
        private bool _four;
        private bool _five;

        public QuestionForSurvey(string text)
        {
            _text = text;
            _one = false;
            _two = false;
            _three = false;
            _four = false;
            _five = false;
        }
        public string Text { get { return _text; }set { this._text = value; OnPropertyChanged("_text"); } }
        public bool One { get { return _one; } set { this._one = value; OnPropertyChanged("_one"); } }
        public bool Two { get { return _two; } set { this._two = value; OnPropertyChanged("_two"); } }
        public bool Three { get { return _three; } set { this._three = value; OnPropertyChanged("_three"); } }
        public bool Four { get { return _four; } set { this._four = value; OnPropertyChanged("_four"); } }
        public bool Five { get { return _five; } set { this._five = value; OnPropertyChanged("_five"); } }
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
