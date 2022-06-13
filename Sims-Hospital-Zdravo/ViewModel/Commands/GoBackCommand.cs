using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sims_Hospital_Zdravo.View.Manager;
using Sims_Hospital_Zdravo.View.Manager.Surveys;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class GoBackCommand : ICommand
    {
        private Frame ManagerContent;

        public GoBackCommand()
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

            if (ManagerContent.Source == null && ManagerContent.Content != null)
            {
                if (!(ManagerContent.Content is ManagerSurveys))
                {
                    ManagerContent.GoBack();
                }
            }
            else if (!IsMainWindowPage(ManagerContent.Source.OriginalString) && ManagerContent.CanGoBack)
            {
                ManagerContent.GoBack();
            }
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

        private bool IsMainWindowPage(string uri)
        {
            switch (uri)
            {
                case "Sims-Hospital-Zdravo;component/view/manager/Rooms/ManagerRooms.xaml": return true;
                case "Sims-Hospital-Zdravo;component/view/manager/Equipment/ManagerEquipment.xaml": return true;
                case "Sims-Hospital-Zdravo;component/view/manager/Medicines/ManagerMedicines.xaml": return true;
                case "Sims-Hospital-Zdravo;component/view/manager/Renovations/ManagerRenovations.xaml": return true;
                case "Sims-Hospital-Zdravo;component/view/manager/Surveys/ManagerSurveys.xaml": return true;
                default: return false;
            }
        }
    }
}