using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class Tutorial : Page
    {
        public ICommand PlayVideoCommand => new PlayVideoCommand(TutorialPlayer);
        public ICommand PauseVideoCommand => new PauseVideoCommand(TutorialPlayer);
        public ICommand RestartVideoCommand => new RestartVideoCommand(TutorialPlayer);

        public Tutorial()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            DataContext = this;
        }
    }

    public class PlayVideoCommand : ICommand
    {
        private MediaElement TutorialPlayer;

        public PlayVideoCommand(MediaElement tutorialPlayer)
        {
            this.TutorialPlayer = tutorialPlayer;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Executing" + TutorialPlayer);
            TutorialPlayer.Play();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class RestartVideoCommand : ICommand
    {
        private MediaElement TutorialPlayer;

        public RestartVideoCommand(MediaElement tutorialPlayer)
        {
            this.TutorialPlayer = tutorialPlayer;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TutorialPlayer.Close();
            TutorialPlayer.Play();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class PauseVideoCommand : ICommand
    {
        private MediaElement TutorialPlayer;

        public PauseVideoCommand(MediaElement tutorialPlayer)
        {
            this.TutorialPlayer = tutorialPlayer;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TutorialPlayer.Pause();
        }

        public event EventHandler CanExecuteChanged;
    }
}