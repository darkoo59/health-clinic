using Model;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
    public class AccountService
    {
        public AccountRepository accountRepository;

        public AccountService(AccountRepository accRepository)
        {
            this.accountRepository = accRepository;
        }

        public void Create(User account)
        {
            accountRepository.Create(account);
        }

        public ref ObservableCollection<User> ReadAll()
        {
            return ref accountRepository.ReadAll();
        }

        public void Update(User account)
        {
            accountRepository.Update(account);
        }

        public void Delete(User account)
        {
            accountRepository.Delete(account);
        }

        public User FindAccountById(int id)
        {
            return accountRepository.FindAccountById(id);
        }

        public User GetAccountByUsernameAndPassword(String username, String password)
        {
            return accountRepository.GetAccountByUsernameAndPassword(username, password);
        }


        public User GetLoggedAccount()
        {
            return accountRepository.GetLoggedAccount();
        }

        public void Login(String username, String password)
        {
            accountRepository.Login(username, password);
        }

        public void Logout()
        {
            accountRepository.Logout();
        }
    }
}