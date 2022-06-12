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
using Sims_Hospital_Zdravo.DTO;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils.FilterPipelines;
using Sims_Hospital_Zdravo.Utils.Filters;

namespace Sims_Hospital_Zdravo.View.Manager
{
    /// <summary>
    /// Interaction logic for ManagerEquipment.xaml
    /// </summary>
    public partial class ManagerEquipment : Page
    {
        private RoomController roomController;
        private Frame ManagerContent;
        private IFilterPipeline<RoomEquipment> roomEquipmentPipeline;
        public bool ShowConsumableEquipment { get; set; } = true;
        public bool ShowStaticEquipment { get; set; } = true;

        public ManagerEquipment()
        {
            roomController = new RoomController();

            InitializeComponent();

            DataContext = this;
            RoomPicker.ItemsSource = roomController.FindAll();
            RoomPicker.SelectedIndex = 0;

            EquipmentTable.ItemsSource = roomController.FilterRoomEquipment((Room)RoomPicker.SelectedItem, CreateFilterDTO());
            RetrieveMainFrame();
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            ManagerContent.Source = new Uri("Equipment/EquipmentTransfer.xaml", UriKind.Relative);
        }

        private void RoomChanged_Selection(object sender, SelectionChangedEventArgs e)
        {
            RefreshItems();
        }

        private void RefreshItems()
        {
            EquipmentTable.ItemsSource = roomController.FilterRoomEquipment((Room)RoomPicker.SelectedItem, CreateFilterDTO());
        }

        private RoomEquipmentFilterDTO CreateFilterDTO()
        {
            return new RoomEquipmentFilterDTO(ShowStaticEquipment, ShowConsumableEquipment, SearchBox.Text);
        }

        private void ChbValueChanged(object sender, RoutedEventArgs e)
        {
            RefreshItems();
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

        private void Search_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RefreshItems();
            }
        }
    }
}