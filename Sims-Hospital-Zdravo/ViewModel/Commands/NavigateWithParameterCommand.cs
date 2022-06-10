using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sims_Hospital_Zdravo.View.Manager;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class NavigateWithParameterCommand : ICommand
    {
        private Frame ManagerContent;
        private Page page;

        public NavigateWithParameterCommand(Page page)
        {
            RetrieveMainFrame();
            this.page = page;
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

            ManagerContent.Content = page;
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