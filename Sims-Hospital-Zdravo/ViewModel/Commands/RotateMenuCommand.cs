using System;
using System.Windows;
using System.Windows.Input;
using Sims_Hospital_Zdravo.View.Manager;
using Sims_Hospital_Zdravo.View.UserControlls;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class RotateMenuCommand : ICommand
    {
        private ManagerMenu menu;

        public RotateMenuCommand()
        {
            RetrieveMenu();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int direction = int.Parse(parameter.ToString());
            if (menu == null)
            {
                RetrieveMenu();
            }

            menu.RotateMenu(direction);
        }

        private void RetrieveMenu()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    menu = ((ManagerMainWindow)win).ManagerMenu;
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}