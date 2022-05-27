using System;
using System.Windows;
using System.Windows.Input;
using Sims_Hospital_Zdravo.View.Manager;
using Sims_Hospital_Zdravo.View.UserControlls;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class SetMenuItemCommand : ICommand
    {
        private ManagerMenu menu;

        public SetMenuItemCommand()
        {
            RetrieveMenu();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string item = parameter as string;
            if (menu == null)
            {
                RetrieveMenu();
            }

            menu.SetMenuItem(item);
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