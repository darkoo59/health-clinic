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
using Controller;

namespace Sims_Hospital_Zdravo.View.Manager
{
    /// <summary>
    /// Interaction logic for ManagerEquipment.xaml
    /// </summary>
    public partial class ManagerEquipment : Window
    {

        //private EquipmentController equipmentController;
        //private EquipmentTransferController equipmentTransferController;
        /*
         EquipmentController equipmentController, EquipmentTransferController equipmentTransferController,
         
         */
        private RoomController roomController;
        public ManagerEquipment(RoomController roomController)
        {
            //this.equipmentController = equipmentController;
            //this.equipmentTransferController = equipmentTransferController;
            this.roomController = roomController;
            InitializeComponent();
            //equipmentTable.ItemsSource = equipmentController.ReadAll();
        }

        private void InsertRoom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            //equipmentController, equipmentTransferController,
            EquipmentTransfer equipmentTransfer = new EquipmentTransfer(roomController);
            equipmentTransfer.Show();


        }
    }
}
