using System.Windows.Input;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class Commands
    {
        public static ICommand GoBackCommand = new GoBackCommand();
        public static ICommand NavigateToCommand = new NavigateToCommand();
        public static ICommand RotateMenuCommand = new RotateMenuCommand();
        public static ICommand SetMenuItemCommand = new SetMenuItemCommand();
        public static ICommand LogoutCommand = new LogoutCommand();
        public static ICommand SearchCommand = new SearchCommand();
        public static ICommand CreatePdfCommand = new CreatePdfCommand();
    }
}