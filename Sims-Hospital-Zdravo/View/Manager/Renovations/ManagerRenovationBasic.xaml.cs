using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.ViewModel;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerRenovationBasic : Page
    {
        public ManagerRenovationBasic()
        {
            this.DataContext = new RenovationBasicViewModel();
            InitializeComponent();
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
        }
    }
}