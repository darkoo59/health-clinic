using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
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
            _roomController = new RoomController();
            roomsForAdding = new ObservableCollection<Room>();
            InitializeComponent();
            DataContext = this;

            RoomsToBeAdded.ItemsSource = roomsForAdding;
            RenovationRooms.ItemsSource = _roomController.FindAll();
            this.KeyDown += new KeyEventHandler(GoBack);
        }

        private void GoBack(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                NavigationService.GoBack();
            }
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
                Validate();
                RoomRenovationType roomRenovationType = GetRoomRenovationType();
                TimeInterval time = FormTimeIntervalFromDates();
                string description = Description.Text;
                (Room, List<Room>) roomData = GetRoomData();
                AdvancedRenovationAppointment advancedRenovationAppointment =
                    new AdvancedRenovationAppointment(time, roomData.Item1, description, roomData.Item2, roomRenovationType, _renovationController.GenerateId());
                _renovationController.MakeAdvancedRenovationAppointment(advancedRenovationAppointment);
                MessageBox.Show("Renovation successfully scheduled");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private (Room, List<Room>) GetRoomDataForJoinRenovation()
        {
            Room room = null;
            List<Room> roomsForJoining = new List<Room>();
            roomsForJoining.AddRange(RenovationRooms.SelectedItems.Cast<Room>().ToArray());
            if (roomsForAdding.Count != 0)
            {
                room = roomsForAdding[0];
            }

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
            DateTime end = DateTime.Parse(EndDate.Text).AddHours(23).AddMinutes(59).AddSeconds(59);
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

        private void RenovationRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var roomData = GetRoomData();

            if (_isSplit)
            {
                List<TimeInterval> timeIntervals = _renovationController.GetTakenDateIntervals(roomData.Item1);
                TakenDates.ItemsSource = timeIntervals;
            }
            else
            {
                List<TimeInterval> timeIntervals = _renovationController.GetTakenDateIntervals(roomData.Item2);
                TakenDates.ItemsSource = timeIntervals;
            }
        }


        private void Validate()
        {
            ValidateTxtField(Description, "Description");
            ValidateListBox(RenovationRooms, "Renovation rooms");
            ValidateListBox(RoomsToBeAdded, "Rooms to be added");
            ValidateDatePicker(StartDate, "Start Date");
            ValidateDatePicker(EndDate, "End Date");
        }

        private void ValidateTxtField(TextBox txt, string name)
        {
            if (txt.Text.Equals(""))
            {
                throw new Exception("Field " + name + " shouldn't be empty!");
            }
        }

        private void ValidateListBox(ListBox list, string name)
        {
            if (list.Items.Count == 0)
            {
                throw new Exception("There should be at least one room in " + name + "!");
            }
        }

        private void ValidateDatePicker(DatePicker datePicker, string name)
        {
            if (datePicker.Text.Equals(""))
            {
                throw new Exception(name + " should have a value!");
            }
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