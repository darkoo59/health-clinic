using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class QuestionForSurvey
    {
        private string _text;
        private bool _one;
        private bool _two;
        private bool _three;
        private bool _four;
        private bool _five;
        private int _id;

        public QuestionForSurvey(string text, int id)
        {
            _id = id;
            _text = text;
            _one = false;
            _two = false;
            _three = false;
            _four = false;
            _five = false;
        }
        public int Id { get { return _id; } set { this._id = value; } }
        public string Text { get { return _text; }set { this._text = value; } }
        public bool One { get { return _one; } set { this._one = value; } }
        public bool Two { get { return _two; } set { this._two = value; } }
        public bool Three { get { return _three; } set { this._three = value; } }
        public bool Four { get { return _four; } set { this._four = value; } }
        public bool Five { get { return _five; } set { this._five = value; } }
    }
}
