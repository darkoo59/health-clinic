using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sims_Hospital_Zdravo.View.Manager;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class NavigateToCommand : ICommand
    {
        private Frame ManagerContent;

        public NavigateToCommand()
        {
            RetrieveMainFrame();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (ManagerContent == null)
            {
                RetrieveMainFrame();
            }

            string uri = parameter as string;
            ManagerContent.Source = new Uri(uri, UriKind.Relative);
        }

        public event EventHandler CanExecuteChanged;

        private void RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    ManagerContent = ((ManagerMainWindow)win).ManagerContent;
                }
            }
        }
    }
}