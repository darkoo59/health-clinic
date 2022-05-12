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
    /// Interaction logic for ManagerInsertRoom.xaml
    /// </summary>
    public partial class ManagerInsertRoom : Window
    {
        private RoomController roomController;

        public ManagerInsertRoom(RoomController roomController)
        {
            InitializeComponent();
            this.roomController = roomController;
            RoomTypeCmb.ItemsSource = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
        }

        private void SaveInsertedRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
                Room room = new Room(Int32.Parse(FloorTxt.Text), roomController.GenerateId(), (RoomType)RoomTypeCmb.SelectedValue, RoomNumberTxt.Text, Int32.Parse(QuadratureTxt.Text));
                Console.WriteLine("This is room " + room);
                roomController.Create(room);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Validate()
        {
            ValidateNumber(FloorTxt.Text, "Floor");
            ValidateNumber(QuadratureTxt.Text, "Quadrature");
            ValidateComboBoxSelected();
        }

        private void ValidateComboBoxSelected()
        {
            if (RoomTypeCmb.SelectedIndex == -1)
                throw new Exception("Room type should be selected");
        }

        private void ValidateNumber(string text, string property)
        {
            int number;
            bool isValid = Int32.TryParse(text, out number);
            if (!isValid)
                throw new Exception(property + " should be number!");
        }
    }
}