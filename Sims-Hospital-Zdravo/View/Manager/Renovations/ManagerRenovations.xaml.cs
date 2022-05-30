using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Controller;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Manager.Custom;
using Sims_Hospital_Zdravo.ViewModel;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerRenovations : Page
    {
        public ManagerRenovations()
        {
            this.DataContext = new RenovationsViewModel();
            InitializeComponent();
        }
    }
}