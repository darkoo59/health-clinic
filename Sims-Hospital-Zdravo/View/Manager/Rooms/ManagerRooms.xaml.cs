using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using Sims_Hospital_Zdravo.View.Manager;
using Sims_Hospital_Zdravo.View.Manager.Dialogs;


namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for ManagerRooms.xaml
    /// </summary>
    public partial class ManagerRooms : Page
    {
        private RoomController roomController;
        private Frame ManagerContent;
        private App app;

        public static RoutedUICommand insertCommand = new RoutedUICommand("InsertCommand", "InsertCommand",
            typeof(RoutedCommand),
            new InputGestureCollection()
            {
                new KeyGesture(Key.I, ModifierKeys.Control)
            }
        );

        public ManagerRooms()
        {
            app = Application.Current as App;
            InitializeComponent();
            roomController = app._roomController;
            DataContext = this;
            RoomsTable.ItemsSource = roomController.ReadAll();
            RetrieveMainFrame();
        }

        private void InsertRoom_Click(object sender, RoutedEventArgs e)
        {
            OpenInsertRoom();
        }

        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            OpenUpdateRoom();
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            OpenDeleteRoom();
        }

        private void OpenInsertRoom()
        {
            RenovationRoomDialog roomDialog = new RenovationRoomDialog();
            if (roomDialog.ShowDialog() == false)
            {
                Room room = roomDialog.Room;
                if (room == null) return;
                room.Id = roomController.GenerateId();
                roomController.Create(room);
            }
        }


        private void OpenUpdateRoom()
        {
            if (RoomsTable.SelectedItem == null)
            {
                MessageBox.Show("Room should be selected!");
                return;
            }

            ManagerUpdateRoom managerUpdateRoom = new ManagerUpdateRoom(roomController) { DataContext = (Room)RoomsTable.SelectedItem };
            managerUpdateRoom.Show();
        }

        private void OpenDeleteRoom()
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


        private void RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    ManagerContent = ((ManagerMainWindow)win).ManagerContent;
                }
            }
        }
    }
}