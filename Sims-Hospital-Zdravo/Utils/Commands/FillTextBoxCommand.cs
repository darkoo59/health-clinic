using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sims_Hospital_Zdravo.Utils.Commands
{
    public class FillTextBoxCommand : IDemoCommand
    {
        private TextBox _textBox;
        private string _text;
        public FillTextBoxCommand(TextBox textBox, string text)
        {
            this.TextBox = textBox;
            this.Text = text;
        }

        public void Execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate { TextBox.Text = Text; });
        }
        public TextBox TextBox { get { return _textBox; } set { _textBox = value; } }
        public string Text { get { return _text; } set { _text = value; } }
    }
}
