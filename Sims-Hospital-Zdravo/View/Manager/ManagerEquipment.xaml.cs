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
using Controller;
using Model;

namespace Sims_Hospital_Zdravo.View.Manager
{
    /// <summary>
    /// Interaction logic for ManagerEquipment.xaml
    /// </summary>
    public partial class ManagerEquipment : Page
    {
        private EquipmentController equipmentController;
        private EquipmentTransferController equipmentTransferController;

        private RoomController roomController;
        private Frame ManagerContent;
        private App app;

        public ManagerEquipment()
        {
            app = Application.Current as App;
            this.equipmentController = app._equipmentController;
            this.equipmentTransferController = app._equipmentTransferController;
            this.roomController = app._roomController;

            InitializeComponent();
            RoomPicker.ItemsSource = roomController.ReadAll();
            RoomPicker.SelectedIndex = 0;
            equipmentTable.ItemsSource = ((Room)RoomPicker.SelectedItem).RoomEquipment;

            RetrieveMainFrame();
        }

        private void InsertRoom_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            ManagerContent.Source = new Uri("EquipmentTransfer.xaml", UriKind.Relative);
        }

        private void RoomChanged_Selection(object sender, SelectionChangedEventArgs e)
        {
            equipmentTable.ItemsSource = ((Room)RoomPicker.SelectedItem).RoomEquipment;
        }

        private void RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    ManagerContent = ((ManagerMainWindow)win).ManagerContent;
                }
            }
        }
    }
}