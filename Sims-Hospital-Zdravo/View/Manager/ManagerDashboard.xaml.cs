using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.View.Manager;

namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for ManagerDashboard.xaml
    /// </summary>
    public partial class ManagerDashboard : Window
    {
        private RoomController roomController;
        private EquipmentController equipmentController;
        private EquipmentTransferController equipmentTransferController;
        private RenovationController renovationController;
        private App app;

        public ManagerDashboard()
        {
            app = Application.Current as App;
            this.roomController = app._roomController;
            this.equipmentController = app._equipmentController;
            this.equipmentTransferController = app._equipmentTransferController;
            this.renovationController = app._renovationController;
            InitializeComponent();
        }

        private void Rooms_Click(object sender, RoutedEventArgs e)
        {
            ManagerRooms rooms = new ManagerRooms(this.roomController);
            rooms.Show();
        }

        private void Equipment_Click(object sender, RoutedEventArgs e)
        {
            ManagerEquipment managerEquipment = new ManagerEquipment(roomController, equipmentController, equipmentTransferController);
            managerEquipment.Show();
        }

        private void Renovations_Click(object sender, RoutedEventArgs e)
        {
            ManagerRenovations renovations = new ManagerRenovations();
            renovations.Show();
        }
    }
}