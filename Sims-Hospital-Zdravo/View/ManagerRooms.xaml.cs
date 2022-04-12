using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for ManagerRooms.xaml
    /// </summary>
    public partial class ManagerRooms : Window
    {
        private RoomController roomController;
        public ManagerRooms(RoomController roomController)
        {
            InitializeComponent();
            this.roomController = roomController;
            this.DataContext = this;
            roomsTable.ItemsSource = this.roomController.ReadAll();
        }

        private void InsertRoom_Click(object sender, RoutedEventArgs e)
        {
            ManagerInsertRoom managerInRoom = new ManagerInsertRoom(roomController);
            managerInRoom.Show();

        }

        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            ManagerUpdateRoom managerUpdateRoom = new ManagerUpdateRoom(roomController) { DataContext = roomsTable.SelectedItem};
            managerUpdateRoom.Show();
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
            if(dialogResult == MessageBoxResult.Yes)
            {
                roomController.Delete((Room)roomsTable.SelectedItem);
            }
        }
    }
}
