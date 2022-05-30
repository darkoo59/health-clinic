using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class QuestionAndRate
    {
        private int _id;
        private string _question;
        private int _rate;

        public QuestionAndRate(int id, string question, int rate)
        {
            Id = id;
            Question = question;
            Rate = rate;
        }
        public int Id { get{return _id;} set{this._id = value;} }
        public string Question { get { return _question; } set { this._question = value; } }
        public int Rate { get { return _rate; } set { this._rate = value; } }
    }
}
