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
    public partial class EquipmentTransfer : Window
    {
        private EquipmentController equipmentController;
        private EquipmentTransferController equipmentTransferController;
        private RoomController roomController;

        private ObservableCollection<Room> rooms;

        public EquipmentTransfer(RoomController roomController, EquipmentController equipmentController,
            EquipmentTransferController equipmentTransferController)
        {
            this.equipmentController = equipmentController;
            this.equipmentTransferController = equipmentTransferController;
            this.roomController = roomController;
            InitializeComponent();

            rooms = roomController.ReadAll();
            ComboFromRoom.ItemsSource = rooms;
            ComboToRoom.ItemsSource = rooms;
        }


        private void RoomData_Changed(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)ComboFromRoom.SelectedItem;
            ComboEquipment.ItemsSource = room.RoomEquipment;
            UpdateTimeIntervals();
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
                Room roomFrom = (Room)ComboFromRoom.SelectedItem;
                Room roomTo = (Room)ComboToRoom.SelectedItem;
                RoomEquipment eq = (RoomEquipment)ComboEquipment.SelectedItem;
                int minutes = Int32.Parse(IntervalDuration.Text);
                int quantity = Int32.Parse(Quantity.Text);
                DateTime start = (DateTime)IntervalStarts.Value;
                DateTime end = start.AddMinutes(minutes);
                equipmentTransferController.MakeRelocationAppointment(roomFrom.Id, roomTo.Id, eq.Equipment, quantity, new TimeInterval(start, end));
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }
    }
}