using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo.View.ViewPatient
{
    /// <summary>
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        public Frame frame;
        public Timer timer;
        public ChangePasswordPage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
            ValidateOld.Visibility = Visibility.Hidden;
            ValidateNew.Visibility = Visibility.Hidden;
            ValidateConfirm.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Old.Password == "")
                {
                    throw new Exception("Old");
                }
                else if (New.Password == "")
                {
                    throw new Exception("New");
                }
                else if (Confirm.Password == "")
                {
                    throw new Exception("Confirm");
                }
                else if (Confirm.Password != New.Password)
                {
                    throw new Exception("Mismatch");
                }
                frame.Content = new ProfilePage(frame);
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Old"))
                {
                    ValidateOld.Visibility = Visibility.Visible;
                    ValidateNew.Visibility = Visibility.Hidden;
                    ValidateConfirm.Visibility = Visibility.Hidden;
                    ValidateOld.Content = "Enter a value";
                }
                else if (ex.Message.Equals("New"))
                {
                    ValidateOld.Visibility = Visibility.Hidden;
                    ValidateNew.Visibility = Visibility.Visible;
                    ValidateConfirm.Visibility = Visibility.Hidden;
                    ValidateNew.Content = "Enter a value";
                }
                else if (ex.Message.Equals("Confirm"))
                {
                    ValidateOld.Visibility = Visibility.Hidden;
                    ValidateNew.Visibility = Visibility.Hidden;
                    ValidateConfirm.Visibility = Visibility.Visible;
                    ValidateConfirm.Content = "Enter a value";
                }
                else
                {
                    ValidateOld.Visibility = Visibility.Hidden;
                    ValidateNew.Visibility = Visibility.Hidden;
                    ValidateConfirm.Visibility = Visibility.Visible;
                    ValidateConfirm.Content = "Password mismatch";
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<IDemoCommand> commands = new List<IDemoCommand>
            {
                new FillTextFieldCommand(Old,"stara"),
                new FillTextFieldCommand(New,"nova"),
                new FillTextFieldCommand(Confirm,"nova")
            };
            timer = new Timer(1000);
            timer.Elapsed += (Object source, ElapsedEventArgs e1) => TimerCallback(commands);
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private void TimerCallback(List<IDemoCommand> commands)
        {
            if (commands.Count > 0)
            {
                commands[0].Execute();
                commands.RemoveAt(0);
            }
            else
            {
                timer.Stop();
                App.Current.Dispatcher.Invoke((Action)delegate { frame.Content = new ProfilePage(frame); });
            }
        }
    }
}
