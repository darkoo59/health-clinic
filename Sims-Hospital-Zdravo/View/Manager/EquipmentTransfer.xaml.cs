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
            ComboEquipment.ItemsSource = room._RoomEquipment;
            UpdateTimeIntervals();
        }

        private void UpdateTimeIntervals()
        {
            try
            {

                int minutes = -1;
                bool isValidNumber = Int32.TryParse(IntervalDuration.Text, out minutes);

                if (!isValidNumber) return;


                List<TimeInterval> timeIntervals = equipmentTransferController.GetFreeTimeIntervals(minutes, (Room)ComboFromRoom.SelectedItem, (Room)ComboToRoom.SelectedItem);
                Console.WriteLine("There is " + timeIntervals.Count + " intervals");
                ListIntervals.ItemsSource = timeIntervals;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something bad happened");
            }
        }

        private void ToRoomData_Changed(object sender, SelectionChangedEventArgs e)
        {
            UpdateTimeIntervals();
        }

        private void Duration_Changed(object sender, TextChangedEventArgs e)
        {
            UpdateTimeIntervals();
        }

        private void SaveApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room roomFrom = (Room)ComboFromRoom.SelectedItem;
                Room roomTo = (Room)ComboFromRoom.SelectedItem;
                TimeInterval ti = (TimeInterval)ListIntervals.SelectedItem;
                RoomEquipment eq = (RoomEquipment)ComboEquipment.SelectedItem;
                int minutes = Int32.Parse(IntervalDuration.Text);
                equipmentTransferController.MakeRelocationAppointment(roomFrom._Id, roomTo._Id, eq._Equip, eq._Quantity, ti);
                //equipmentTransferController.TransferEquipmentToRoom(roomTo, eq._Equip, eq._Quantity, new TimeInterval(ti.Start, ti.Start.AddMinutes(minutes)));
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    
}
