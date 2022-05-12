using Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Controller
{
    public class AccountController
    {
        public AccountService accountService;
        public AccountController(AccountService accService)
        {
            accountService = accService;
        }
        public void Create(User account)
        {
            accountService.Create(account);
        }
        public ref ObservableCollection<User> ReadAll()
        {
            return ref accountService.ReadAll();
        }

        public void Update(User account)
        {
            accountService.Update(account);
        }

        public void Delete(User account)
        {
            accountService.Delete(account);
        }

        public User FindAccountById(int id)
        {
            return accountService.FindAccountById(id);
        }

        public User GetAccountByUsernameAndPassword(String username,String password)
        {
            return accountService.GetAccountByUsernameAndPassword(username, password);
        }
        
        
        public User GetLoggedAccount()
        {
            return accountService.GetLoggedAccount();
        }

        public void AddLoggedAccount(String username, String password)
        {
            accountService.AddLoggedAccount(username, password);
        }
    }
}
