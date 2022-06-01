using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum MedicineStatus
{
    PENDING,
    ABORTED,
    ACCEPTED
}

namespace Sims_Hospital_Zdravo.Model
{
    public class Medicine
    {
        private string _name;
        private int _id;
        private string allergens;
        private string _description;
        private string _strength;
        private MedicineStatus _status;
        private List<string> _ingredients;
        private List<Medicine> _substitution;
        private bool _notAllergic;

        public Medicine(string name, string strength, string allergens, string description)
        {
            this._name = name;
            this.allergens = allergens;
            this._description = description;
            this._strength = strength;
            _substitution = new List<Medicine>();
            this._status = MedicineStatus.PENDING;
            _ingredients = new List<string>();
            this._notAllergic = false;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        public string Allergens
        {
            get { return allergens; }
            set { allergens = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public string Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public MedicineStatus Status
        {
            get { return _status; }

            set { _status = value; }
        }

        public List<Medicine> Substitution
        {
            get { return _substitution; }

            set { _substitution = value; }
        }


        public List<string> Ingredients
        {
            get {  return _ingredients; }
            set { _ingredients = value; }
        }

        public bool NotAllergic
        {
            get
            {
                return _notAllergic;
            }
            set
            {
                _notAllergic = value;

            }
        }
        public string GetStringAllergic()
        {
            if (_notAllergic == false)
            {
                return " Allergic";
            }
            else
            {
                return "Not allergic";
            }
        }
        public override string ToString()
        {
            return _name + " " + _strength + " "+ GetStringAllergic();
        }
    }
}