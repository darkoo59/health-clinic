using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sims_Hospital_Zdravo.Utils.Commands
{
    public class FillTextFieldCommand : IDemoCommand
    {
        private PasswordBox textBox;
        private string text;
        public FillTextFieldCommand(PasswordBox textBox, string text)
        {
            this.textBox = textBox;
            this.text = text;
        }

        public void Execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate { textBox.Password = text; });
        }
    }
}
