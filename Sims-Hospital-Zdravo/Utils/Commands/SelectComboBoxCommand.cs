using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sims_Hospital_Zdravo.Utils.Commands
{
    public class SelectComboBoxCommand : IDemoCommand
    {
        private ComboBox comboBox;
        private string text;
        public SelectComboBoxCommand(ComboBox comboBox, string text)
        {
            this.comboBox = comboBox;
            this.text = text;
        }
        public void Execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate { comboBox.SelectedItem = text; });
        }
    }
}
