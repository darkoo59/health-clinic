using Controller;
using Model;
using System;
using System.Windows;


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
            RoomsTable.ItemsSource = this.roomController.ReadAll();
        }

        private void InsertRoom_Click(object sender, RoutedEventArgs e)
        {
            ManagerInsertRoom managerInRoom = new ManagerInsertRoom(roomController);
            managerInRoom.Show();
        }

        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsTable.SelectedItem == null)
            {
                MessageBox.Show("Room should be selected!");
                return;
            }

            ManagerUpdateRoom managerUpdateRoom = new ManagerUpdateRoom(roomController) { DataContext = (Room)RoomsTable.SelectedItem };
            managerUpdateRoom.Show();
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    roomController.Delete((Room)RoomsTable.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}