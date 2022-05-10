using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
            ManagerContent.Source = new Uri("ManagerRenovations.xaml", UriKind.Relative);
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }

        private void OnButtonKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.Key == Key.W)
            {
                ManagerMenu.RotateMenu(-1);
            }
            else if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.Key == Key.S)
            {
                ManagerMenu.RotateMenu(1);
            }
        }
    }
}