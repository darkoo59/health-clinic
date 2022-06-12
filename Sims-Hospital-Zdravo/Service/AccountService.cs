using Model;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo.Model
{
    public class AccountService
    {
        public IAccountRepository accountRepository;
        public AccountValidator validator;
        public AccountService(IAccountRepository accRepository)
        {
            this.accountRepository = accRepository;
            this.validator = new AccountValidator(accountRepository);
        }

        public void Create(User account)
        {
            accountRepository.Create(account);
        }

        public List<User> FindAll()
        {
            return accountRepository.FindAll();
        }

        public void Update(User account)
        {
            validator.ValidateAccountUpdate(account);
            accountRepository.Update(account);
        }

        public void Delete(User account)
        {
            accountRepository.Delete(account);
        }

        public User FindAccountById(int id)
        {
            return accountRepository.FindById(id);
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