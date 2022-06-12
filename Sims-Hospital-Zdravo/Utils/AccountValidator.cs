using Model;
using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Utils
{
    public class AccountValidator
    {
        private IAccountRepository _accountReposiory;

        public AccountValidator(IAccountRepository accRepo)
        {
            _accountReposiory = accRepo;
        }

        public void ValidateAccountUpdate(User account)
        {
            if(account.Name == "" || account.Name == null) throw new Exception("Please add name first!");
            if(account.Surname == "" || account.Surname == null) throw new Exception("Please add surname first!");
            if(account.Email == "" || account.Email == null) throw new Exception("Please add email first!");
            if(account.Jmbg == "" || account.Jmbg == null) throw new Exception("Please add jmbg first!");
            if(account.PhoneNumber == "" || account.PhoneNumber == null) throw new Exception("Please add phone number first!");
            if(account.Username == "" || account.Username == null) throw new Exception("Please add username first!");
            if(account.Password == "" || account.Password == null) throw new Exception("Please add password first!");
            if(account.Jmbg.Length != 13) throw new Exception("Jmbg need to have 13 numbers!");
            if(!isDigits(account.Jmbg)) throw new Exception("Jmbg can have numbers only!");
            if(!isCredentialsUnique(account)) throw new Exception("Credentials already exist!");
        }

        bool isDigits(string s)
        {
            if (s == null || s == "") return false;

            for (int i = 0; i < s.Length; i++)
                if ((s[i] ^ '0') > 9)
                    return false;

            return true;
        }

        bool isCredentialsUnique(User acc)
        {
            List<User> accounts = _accountReposiory.FindAll();
            foreach(User user in accounts)
            {
                if (user.Username == acc.Username && user.Password == acc.Password && user.Id != acc.Id)
                    return false;
            }
            return true;
        }
    }
}
