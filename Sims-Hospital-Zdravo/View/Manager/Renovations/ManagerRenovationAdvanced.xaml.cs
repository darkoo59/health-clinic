using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.View.Manager.Dialogs;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerRenovationAdvanced : Page
    {
        private bool _isSplit = true;
        private ObservableCollection<Room> roomsForAdding;
        private RenovationController _renovationController;
        private RoomController _roomController;
        private App app;


        public ManagerRenovationAdvanced()
        {
            app = Application.Current as App;
            _renovationController = app._renovationController;
            _roomController = app._roomController;
            roomsForAdding = new ObservableCollection<Room>();
            InitializeComponent();
            DataContext = this;

            RoomsToBeAdded.ItemsSource = roomsForAdding;
            RenovationRooms.ItemsSource = _roomController.ReadAll();
        }


        public bool IsSplit
        {
            get { return _isSplit; }

            set
            {
                if (_isSplit != value)
                {
                    _isSplit = value;
                    ChangeRoomRenovationType();
                }
            }
        }

        private void ChangeRoomRenovationType()
        {
            if (_isSplit)
            {
                RenovationRooms.SelectionMode = SelectionMode.Single;
            }
            else
            {
                RenovationRooms.SelectionMode = SelectionMode.Multiple;
            }
        }

        private void AddNewRoom_Click(object sender, RoutedEventArgs e)
        {
            RenovationRoomDialog roomDialog = new RenovationRoomDialog();
            if (roomDialog.ShowDialog() == false)
            {
                Room room = roomDialog.Room;
                if (room != null)
                {
                    roomsForAdding.Add(room);
                }
            }
        }

        private void ScheduleAdvancedRenovation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomRenovationType roomRenovationType = GetRoomRenovationType();
                TimeInterval time = FormTimeIntervalFromDates();
                string description = Description.Text;
                (Room, List<Room>) roomData = GetRoomData();
                _renovationController.MakeAdvancedRenovationAppointment(time, roomData.Item1, description, roomData.Item2, roomRenovationType);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private (Room, List<Room>) GetRoomDataForJoinRenovation()
        {
            List<Room> roomsForJoining = new List<Room>();
            roomsForJoining.AddRange(RenovationRooms.SelectedItems.Cast<Room>().ToArray());
            Room room = roomsForAdding[0];
            return (room, roomsForJoining);
        }

        private (Room, List<Room>) GetRoomDataForSplitRenovation()
        {
            Room room = (Room)RenovationRooms.SelectedItem;
            return (room, roomsForAdding.ToList());
        }

        private TimeInterval FormTimeIntervalFromDates()
        {
            DateTime start = DateTime.Parse(StartDate.Text);
            DateTime end = DateTime.Parse(EndDate.Text);
            return new TimeInterval(start, end);
        }

        private RoomRenovationType GetRoomRenovationType()
        {
            return _isSplit ? RoomRenovationType.SPLIT : RoomRenovationType.JOIN;
        }

        private (Room, List<Room>) GetRoomData()
        {
            if (_isSplit)
            {
                return GetRoomDataForSplitRenovation();
            }

            return GetRoomDataForJoinRenovation();
        }
    }


    public class AdvancedRenovationBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }

            return value;
        }
    }
}