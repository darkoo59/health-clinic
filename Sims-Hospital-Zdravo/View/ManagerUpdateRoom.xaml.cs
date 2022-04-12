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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo.View
{
    /// <summary>
    /// Interaction logic for ManagerUpdateRoom.xaml
    /// </summary>
    public partial class ManagerUpdateRoom : Window
    {
        private RoomController roomController;
        public ManagerUpdateRoom(RoomController roomController)
        {
            InitializeComponent();
            this.roomController = roomController;
            RoomTypeCmb.ItemsSource = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
        }

        private void SaveInsertedRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room room = new Room(Int32.Parse(FloorTxt.Text), Int32.Parse(RoomIdTxt.Text), (RoomType)RoomTypeCmb.SelectedValue);
                roomController.Update(room);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
