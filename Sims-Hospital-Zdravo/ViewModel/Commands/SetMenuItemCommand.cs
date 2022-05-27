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
            try
            {
                string item = parameter as string;
                if (menu == null)
                {
                    RetrieveMenu();
                }

                if (menu == null)
                {
                    Console.WriteLine("null");
                }

                menu.SetMenuItem(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void RetrieveMenu()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    Console.WriteLine(((ManagerMainWindow)win).Menu);
                    if (((ManagerMainWindow)win).Menu != null)
                        menu = ((ManagerMainWindow)win).Menu;
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}