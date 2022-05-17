using Model;
using Sims_Hospital_Zdravo.DataHandler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class AccountRepository
    {
        public AccountDataHandler accHandler;
        public ObservableCollection<User> accounts;
        public User loggedAccount;

        public AccountRepository(AccountDataHandler accHandler)
        {
            this.accHandler = accHandler;
            this.accounts = new ObservableCollection<User>();
            loggedAccount = null;
            LoadDataFromFile();
        }

        public void Create(User account)
        {
            accounts.Add(account);
            LoadDataToFile();
        }

        public void Delete(User account)
        {
            accounts.Remove(account);
            LoadDataToFile();
        }

        public void Update(User newAccount)
        {
            foreach (User account in accounts)
            {
                if (account._Id == newAccount._Id)
                {
                    account._Password = newAccount._Password;
                    account._Username = newAccount._Username;
                    LoadDataToFile();
                    return;
                }
            }
        }


        public User FindAccountById(int id)
        {
            foreach (User acc in accounts)
            {
                if (acc._Id == id) return acc;
            }

            return null;
        }

        public ref ObservableCollection<User> ReadAll()
        {
            return ref this.accounts;
        }

        public User GetAccountByUsernameAndPassword(String username, String password)
        {
            foreach (User acc in accounts)
            {
                if (acc._Password.Equals(password) && acc._Username.Equals(username))
                {
                    return acc;
                }
            }

            return null;
        }

        public User GetLoggedAccount()
        {
            return loggedAccount;
        }
        public void blockUser()
        {
            loggedAccount.Blocked = true;
            LoadDataToFile();
        }

        public void Login(string username, string password)
        {
            foreach (User acc in accounts)
            {
                if (acc._Password.Equals(password) && acc._Username.Equals(username))
                {
                    loggedAccount = acc;
                    return;
                }
            }

            loggedAccount = null;
        }

        public void Logout()
        {
            loggedAccount = null;
        }

        public void LoadDataFromFile()
        {
            this.accounts = accHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            accHandler.Write(accounts);
        }
    }
}