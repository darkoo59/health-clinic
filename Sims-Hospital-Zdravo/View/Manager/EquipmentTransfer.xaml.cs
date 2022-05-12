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
using Model;
using System.Collections.ObjectModel;
using Controller;

namespace Sims_Hospital_Zdravo.View.Manager
{
    /// <summary>
    /// Interaction logic for EquipmentTransfer.xaml
    /// </summary>
    public partial class EquipmentTransfer : Page
    {
        private EquipmentController equipmentController;
        private EquipmentTransferController equipmentTransferController;
        private RoomController roomController;

        private ObservableCollection<Room> rooms;
        private App app;

        public EquipmentTransfer()
        {
            app = Application.Current as App;
            this.equipmentController = app._equipmentController;
            this.equipmentTransferController = app._equipmentTransferController;
            this.roomController = app._roomController;
            InitializeComponent();

            rooms = roomController.ReadAll();
            ComboFromRoom.ItemsSource = rooms;
            ComboToRoom.ItemsSource = rooms;
        }


        private void RoomData_Changed(object sender, SelectionChangedEventArgs e)
        {
            SetComboEquipment();
            UpdateTimeIntervals();
        }

        private void SetComboEquipment()
        {
            Room room = (Room)ComboFromRoom.SelectedItem;
            ComboEquipment.ItemsSource = null;
            ComboEquipment.ItemsSource = room.RoomEquipment.FindAll(x => x.Equipment.Type == EquipmentType.Static);
            ComboEquipment.SelectedIndex = 0;
        }

        private void UpdateTimeIntervals()
        {
            try
            {
                List<TimeInterval> timeIntervals = equipmentTransferController.GetFreeTimeIntervals((Room)ComboFromRoom.SelectedItem, (Room)ComboToRoom.SelectedItem);
                ListIntervals.ItemsSource = timeIntervals;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ToRoomData_Changed(object sender, SelectionChangedEventArgs e)
        {
            UpdateTimeIntervals();
        }

        private void SaveApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidateBoxes();
                RoomEquipment eq = (RoomEquipment)ComboEquipment.SelectedItem;
                Room roomFrom = (Room)ComboFromRoom.SelectedItem;
                Room roomTo = (Room)ComboToRoom.SelectedItem;


                int minutes = Int32.Parse(IntervalDuration.Text);
                int quantity = Int32.Parse(Quantity.Text);
                DateTime start = (DateTime)IntervalStarts.Value;

                DateTime end = start.AddMinutes(minutes);
                equipmentTransferController.MakeRelocationAppointment(roomFrom.Id, roomTo.Id, eq.Equipment, quantity, new TimeInterval(start, end));
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Successfully made appointment! Do you want to continue transfering equipment?", "Equipment transfer", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    UpdateTimeIntervals();
                    SetComboEquipment();
                    return;
                }

                // Close();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        public void ValidateBoxes()
        {
            ValidateRoomsSelected();
            ValidateEquipment();
            ValidateQuantity();
            ValidateMinutes();
            ValidateDate();
        }


        private void ValidateRoomsSelected()
        {
            Room roomFrom = (Room)ComboFromRoom.SelectedItem;
            Room roomTo = (Room)ComboToRoom.SelectedItem;
            if (roomFrom == null || roomTo == null) throw new Exception("Source and Destination rooms should have selected value!");
        }

        private void ValidateQuantity()
        {
            int quantity;
            bool correct = Int32.TryParse(Quantity.Text, out quantity);
            if (!correct) throw new Exception("Quantity should be a number!");
            if (quantity <= 0) throw new Exception("Quantity should be bigger than 0 !");
        }

        private void ValidateMinutes()
        {
            int minutes;
            bool correct = Int32.TryParse(IntervalDuration.Text, out minutes);
            if (!correct) throw new Exception("Duration should be a number!");
            if (minutes <= 0) throw new Exception("Duration should be bigger than 0!");
        }

        private void ValidateEquipment()
        {
            RoomEquipment eq = (RoomEquipment)ComboEquipment.SelectedItem;
            if (eq == null) throw new Exception("No equipment selected!");
        }

        private void ValidateDate()
        {
            DateTime date;
            bool correct = DateTime.TryParse(IntervalStarts.Value.ToString(), out date);
            if (!correct) throw new Exception("Time in invalid format!");
        }
    }
}