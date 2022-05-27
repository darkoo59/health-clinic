using System;
using System.Windows;
using System.Windows.Input;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.View.Login;
using Sims_Hospital_Zdravo.View.Manager;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class LogoutCommand : ICommand
    {
        private App app;
        private AccountController accountController;

        public LogoutCommand()
        {
            app = Application.Current as App;
            accountController = app._accountController;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            accountController.Logout();
            LoginMainWindow loginMainWindow = new LoginMainWindow();
            loginMainWindow.Show();
            CloseMainFrame();
        }

        private void CloseMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    win.Close();
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}