using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TextBox searchField = parameter as TextBox;
            if (searchField != null)
            {
                searchField.Focus();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}