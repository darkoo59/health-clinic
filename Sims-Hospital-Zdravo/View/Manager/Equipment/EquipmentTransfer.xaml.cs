using System;
using System.Windows.Controls;
using Model;
using Sims_Hospital_Zdravo.ViewModel;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class EquipmentTransfer : Page
    {
        public EquipmentTransfer()
        {
            InitializeComponent();
            DataContext = new EquipmentTransferViewModel();
        }
    }
}