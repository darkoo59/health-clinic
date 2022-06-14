using System;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Sims_Hospital_Zdravo.View.Manager;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class FullScreenCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ManagerMainWindow window = RetrieveMainFrame();
            if (window != null)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    window.WindowState = WindowState.Normal;
                    window.WindowStyle = WindowStyle.SingleBorderWindow;
                }
                else
                {
                    window.WindowState = WindowState.Maximized;
                    window.WindowStyle = WindowStyle.None;
                }
            }
        }

        public event EventHandler CanExecuteChanged;

        private ManagerMainWindow RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    return ((ManagerMainWindow)win);
                }
            }

            return null;
        }
    }
}