using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Controller;
using Model;
using Sims_Hospital_Zdravo.View;
using Sims_Hospital_Zdravo.View.Manager.Dialogs;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        private App app;
        private RoomController roomController;
        public List<Room> Rooms { get; set; }
        public ICommand InsertRoomCommand => new InsertRoomCommand(roomController);
        public ICommand UpdateRoomCommand => new UpdateRoomCommand();
        public ICommand DeleteRoomCommand => new DeleteRoomCommand(roomController);

        public RoomViewModel()
        {
            app = Application.Current as App;
            roomController = app._roomController;
            Rooms = roomController.ReadAll().ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class InsertRoomCommand : ICommand
    {
        private RoomController roomController;

        public InsertRoomCommand(RoomController roomController)
        {
            this.roomController = roomController;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
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

        public event EventHandler CanExecuteChanged;
    }


    public class UpdateRoomCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                Room room = parameter as Room;
                if (room == null) throw new Exception("Room not selected");
                ManagerUpdateRoom managerUpdateRoom = new ManagerUpdateRoom()
                {
                    DataContext = room
                };
                managerUpdateRoom.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public event EventHandler CanExecuteChanged;
    }

    public class DeleteRoomCommand : ICommand
    {
        private RoomController roomController;

        public DeleteRoomCommand(RoomController roomController)
        {
            this.roomController = roomController;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                Room room = parameter as Room;
                if (room == null) throw new Exception("No room selected!");
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    roomController.Delete(room);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}