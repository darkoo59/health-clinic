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

        private string Username;
        private string Password;

        public string _Username { get; set; }
        public string _Password { get; set; }

        private Address address;
        private int Id;
        private String Name;
        private String Surname;
        private DateTime BirthDate;
        private String Email;
        private String Jmbg;
        private String PhoneNumber;
        private RoleType Role;
        private List<DateTime> _cancels;
        private bool _blocked;

        public event PropertyChangedEventHandler PropertyChanged;

        public Address _Address { get; set; }
        public int _Id { get; set; }
        public String _Name
        {
            get
            {
                return Name;
            }
            set
            {
                this.Name = value;
                OnPropertyChanged();
            }
        }
        public String _Surname
        {
            get
            {
                return Surname;
            }
            set
            {
                this.Surname = value;
                OnPropertyChanged();
            }
        }
        public DateTime _BirthDate
        {
            get
            {
                return BirthDate;
            }
            set
            {
                this.BirthDate = value;
                OnPropertyChanged();
            }
        }
        public String _Email
        {
            get
            {
                return Email;
            }
            set
            {
                this.Email = value;
                OnPropertyChanged();
            }
        }
        public String _Jmbg { get; set; }
        public String _PhoneNumber {
            get
            {
                return PhoneNumber;
            }
            set
            {
                this.PhoneNumber = value;
                OnPropertyChanged();
            }
        }

        public RoleType _Role
        {
            get
            {
                return Role;
            }
            set
            {
                this.Role = value;
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
       
    }
}