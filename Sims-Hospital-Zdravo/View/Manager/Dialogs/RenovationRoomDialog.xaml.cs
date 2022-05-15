using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Model;

namespace Sims_Hospital_Zdravo.View.Manager.Dialogs
{
    public partial class RenovationRoomDialog : Window
    {
        public Room Room { get; set; }

        public RenovationRoomDialog()
        {
            InitializeComponent();
            RoomTypeCmb.ItemsSource = Enum.GetValues(typeof(RoomType)).Cast<RoomType>();
            this.KeyDown += new KeyEventHandler(GoBack);
        }

        private void GoBack(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                Close();
            }
        }

        private void SaveInsertedRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();
                this.Room = new Room(Int32.Parse(FloorTxt.Text), -1, (RoomType)RoomTypeCmb.SelectedValue, RoomNumberTxt.Text, Int32.Parse(QuadratureTxt.Text));
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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