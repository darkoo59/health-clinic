using Model;
using Sims_Hospital_Zdravo.DataHandler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Repository
{
    public class AccountRepository:IAccountRepository
    {
        public AccountDataHandler accHandler;
        public List<User> accounts;
        public User loggedAccount;

        public AccountRepository()
        {
            this.accHandler = new AccountDataHandler();
            this.accounts = new List<User>();
            loggedAccount = null;
            LoadDataFromFile();
        }

        public void Create(User account)
        {
            LoadDataFromFile();
            accounts.Add(account);
            LoadDataToFile();
        }

        public void Delete(User account)
        {
            LoadDataFromFile();
            accounts.Remove(account);
            LoadDataToFile();
        }

        public void Update(User newAccount)
        {
            LoadDataFromFile();
            foreach (var account in accounts.Where(account => account._Id == newAccount._Id))
            {
                account._Password = newAccount._Password;
                account._Username = newAccount._Username;
                LoadDataToFile();
                return;
            }
        }
        
        public User FindById(int id)
        {
            LoadDataFromFile();
            foreach (User acc in accounts)
            {
                if (acc._Id == id) return acc;
            }

            return null;
        }

        public List<User> FindAll()
        {
            LoadDataFromFile();
            return this.accounts;
        }

        public User GetAccountByUsernameAndPassword(String username, String password)
        {
            LoadDataFromFile();
            return accounts.FirstOrDefault(acc => acc._Password.Equals(password) && acc._Username.Equals(username));
        }

        public User GetLoggedAccount()
        {
            LoadDataFromFile();
            return loggedAccount;
        }

        public void Login(string username, string password)
        {
            LoadDataFromFile();
            foreach (var acc in accounts.Where(acc => acc._Password.Equals(password) && acc._Username.Equals(username)))
            {
                loggedAccount = acc;
                return;
            }

            loggedAccount = null;
        }

        public void Logout()
        {
            LoadDataFromFile();
            loggedAccount = null;
        }

        public void BlockLoggedAccount()
        {
            LoadDataFromFile();
            loggedAccount.Blocked = true;
            LoadDataToFile();
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