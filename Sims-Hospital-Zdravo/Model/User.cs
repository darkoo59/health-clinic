/***********************************************************************
 * Module:  User.cs
 * Author:  I
 * Purpose: Definition of the Class Model.User
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace Model
{

    public enum RoleType { MANAGER, SECRETARY, DOCTOR, PATIENT };
    public class User : INotifyPropertyChanged
    {
        public bool ChangePassword(string newPassowrd)
        {
            // TODO: implement
            return false;
        }

        private string _username;
        private string _password;

        public string Username { get; set; }
        public string Password { get; set; }

        private Address _address;
        private int _id;
        private String _name;
        private String _surname;
        private DateTime _birthDate;
        private String _email;
        private String _jmbg;
        private String _phoneNumber;
        private RoleType _role;
        private List<DateTime> _cancels;
        private bool _blocked;

        public event PropertyChangedEventHandler PropertyChanged;

        public Address Address { get; set; }
        public int Id { get; set; }
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
                OnPropertyChanged();
            }
        }
        public String Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                this._surname = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                this._birthDate = value;
                OnPropertyChanged();
            }
        }
        public String Email
        {
            get
            {
                return _email;
            }
            set
            {
                this._email = value;
                OnPropertyChanged();
            }
        }
        public String Jmbg { get; set; }
        public String PhoneNumber {
            get
            {
                return _phoneNumber;
            }
            set
            {
                this._phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public RoleType Role
        {
            get
            {
                return _role;
            }
            set
            {
                this._role = value;
                OnPropertyChanged();
            }
        }
        public List<DateTime> Cancels
        {
            get
            {
                return _cancels;
            }
            set
            {
                this._cancels = value;
            }
        }
        public bool Blocked
        {
            get
            {
                return _blocked;
            }
            set
            {
                this._blocked = value;
            }
        }
        private void OnPropertyChanged(string name="")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public override string ToString()
        {
            return this._role.ToString() + " " + this.Name + " " + this.Surname;
        }



    }
}