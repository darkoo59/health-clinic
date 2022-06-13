using Sims_Hospital_Zdravo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sims_Hospital_Zdravo.Utils.Commands
{
    public class SelectDataGridCommand : IDemoCommand
    {
        private DataGrid dataGrid;
        private int item;
        public SelectDataGridCommand(DataGrid dataGrid, int item)
        {
            this.dataGrid = dataGrid;
            this.item = item;
        }

        public void Execute()
        {
            App.Current.Dispatcher.Invoke((Action)delegate { dataGrid.SelectedIndex = item; });
        }
    }
}
