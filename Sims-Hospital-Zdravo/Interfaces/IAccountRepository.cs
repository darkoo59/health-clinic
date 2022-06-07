using System;
using Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IAccountRepository:IGenericRepository<User>
    {
        void Create(User account);
        void Update(User account);
        void Delete(User account);
        
        User FindById(int id);
        User GetAccountByUsernameAndPassword(String username, String password);
        User GetLoggedAccount();
        void Login(string username, string password);
        void Logout();
        void BlockLoggedAccount();
    }
}