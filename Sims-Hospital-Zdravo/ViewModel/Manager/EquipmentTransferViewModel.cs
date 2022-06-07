using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Controller;
using Model;
using Sims_Hospital_Zdravo.ViewModel.Commands;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public class EquipmentTransferViewModel : INotifyPropertyChanged
    {
        private App app;
        private RoomController _roomController;
        private EquipmentTransferController _equipmentTransferController;
        private Room _fromRoom;
        private Room _toRoom;
        private RoomEquipment _roomEquipment;

        public List<TimeInterval> TakenIntervals { get; set; }

        public List<Room> Rooms { get; set; }
        public List<RoomEquipment> EquipmentForRoom { get; set; }

        public Room FromRoom
        {
            get => _fromRoom;
            set
            {
                _fromRoom = value;
                UpdateRoomEquipment();
                UpdateTakenIntervals();
            }
        }

        public Room ToRoom
        {
            get => _toRoom;
            set
            {
                _toRoom = value;
                UpdateTakenIntervals();
            }
        }

        public RoomEquipment RoomEquipment
        {
            get => _roomEquipment;
            set => _roomEquipment = value;
        }

        public int Quantity { get; set; } = 0;
        public int Duration { get; set; } = 0;
        public DateTime Starts { get; set; } = DateTime.Now;

        public ICommand ScheduleRelocationCommand => new ScheduleTransferCommand(this);

        public EquipmentTransferViewModel()
        {
            app = Application.Current as App;
            _roomController = new RoomController();
            _equipmentTransferController = app._equipmentTransferController;

            Rooms = _roomController.FindAll().ToList();
        }

        public void ScheduleEquipmentRelocation()
        {
            try
            {
                Validate();
                int id = _equipmentTransferController.GenerateId();
                RoomEquipment roomEquipment = new RoomEquipment(RoomEquipment.Equipment, Quantity);
                TimeInterval timeInterval = new TimeInterval(Starts, Starts.AddMinutes(Duration));
                RelocationAppointment relocationAppointment = new RelocationAppointment(FromRoom, ToRoom, timeInterval, roomEquipment, id);
                _equipmentTransferController.MakeRelocationAppointment(relocationAppointment);
                ShouldContinueWithTransfer();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show(e.Message);
            }
        }

        private void ShouldContinueWithTransfer()
        {
            MessageBoxResult dialogResult = MessageBox.Show("Successfully made appointment! Do you want to continue transfering equipment?", "Equipment transfer", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                UpdateTakenIntervals();
                UpdateRoomEquipment();
            }
            else
            {
                GoBackCommand goBackCommand = new GoBackCommand();
                goBackCommand.Execute(null);
            }
        }

        public void UpdateRoomEquipment()
        {
            EquipmentForRoom = FromRoom.GetAllEquipmentByType(EquipmentType.Static);
            OnPropertyChanged("EquipmentForRoom");
        }

        public void UpdateTakenIntervals()
        {
            TakenIntervals = _equipmentTransferController.GetTakenTimeIntervals(FromRoom, ToRoom);
            OnPropertyChanged("TakenIntervals");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Validate()
        {
            ValidateRoomsSelected();
            ValidateQuantity();
            ValidateMinutes();
            ValidateEquipment();
        }

        private void ValidateRoomsSelected()
        {
            if (_fromRoom == null || _toRoom == null) throw new Exception("Source and Destination rooms should have selected value!");
        }

        private void ValidateQuantity()
        {
            if (Quantity <= 0) throw new Exception("Quantity should be bigger than 0 !");
        }

        private void ValidateMinutes()
        {
            if (Duration <= 0) throw new Exception("Duration should be bigger than 0!");
        }

        private void ValidateEquipment()
        {
            if (RoomEquipment == null) throw new Exception("No equipment selected!");
        }
    }


    public class ScheduleTransferCommand : ICommand
    {
        private EquipmentTransferViewModel _equipmentTransferViewModel;

        public ScheduleTransferCommand(EquipmentTransferViewModel equipmentTransferViewModel)
        {
            _equipmentTransferViewModel = equipmentTransferViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _equipmentTransferViewModel.ScheduleEquipmentRelocation();
        }

        public event EventHandler CanExecuteChanged;
    }
}