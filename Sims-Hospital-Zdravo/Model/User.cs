/***********************************************************************
 * Module:  User.cs
 * Author:  I
 * Purpose: Definition of the Class Model.User
 ***********************************************************************/

using System;

namespace Model
{
   public class User
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
        public Address _Address { get; set; }
        public int _Id { get; set; }
        public String _Name { get; set; }
        public String _Surname { get; set; }
        public DateTime _BirthDate { get; set; }
        public String _Email { get; set; }
        public String _Jmbg { get; set; }
        public String _PhoneNumber { get; set; }
    }
}