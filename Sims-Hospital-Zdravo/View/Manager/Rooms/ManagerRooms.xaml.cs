using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.View.Manager;
using Sims_Hospital_Zdravo.View.Manager.Dialogs;
using Sims_Hospital_Zdravo.ViewModel;


namespace Sims_Hospital_Zdravo.View
{
    public partial class ManagerRooms : Page
    {
        public ManagerRooms()
        {
            this.DataContext = new RoomViewModel();
            InitializeComponent();
        }
    }
}