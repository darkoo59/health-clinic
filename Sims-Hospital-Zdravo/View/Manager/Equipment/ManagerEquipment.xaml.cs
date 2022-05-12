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
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils.Factories;
using Sims_Hospital_Zdravo.Utils.FilterPipelines;
using Sims_Hospital_Zdravo.Utils.Filters;

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
        private IFilterPipeline<RoomEquipment> roomEquipmentPipeline;
        private RoomEquipmentFilterFactory roomEquipmentFilterFactory;
        public bool ShowConsumableEquipment { get; set; }
        public bool ShowStaticEquipment { get; set; }

        public ManagerEquipment()
        {
            app = Application.Current as App;
            this.equipmentController = app._equipmentController;
            this.equipmentTransferController = app._equipmentTransferController;
            this.roomController = app._roomController;
            ShowConsumableEquipment = true;
            ShowStaticEquipment = true;
            roomEquipmentFilterFactory = new RoomEquipmentFilterFactory();
            roomEquipmentPipeline = roomEquipmentFilterFactory.CreateEquipmentFilterPipeline(ShowStaticEquipment, ShowConsumableEquipment);

            InitializeComponent();
            DataContext = this;
            RoomPicker.ItemsSource = roomController.ReadAll();
            RoomPicker.SelectedIndex = 0;

            EquipmentTable.ItemsSource = roomEquipmentPipeline.FilterAll(((Room)RoomPicker.SelectedItem).RoomEquipment);


            RetrieveMainFrame();
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
            EquipmentTable.ItemsSource = roomEquipmentPipeline.FilterAll(((Room)RoomPicker.SelectedItem).RoomEquipment);
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

        private void ChbValueChanged(object sender, RoutedEventArgs e)
        {
            roomEquipmentPipeline = roomEquipmentFilterFactory.CreateEquipmentFilterPipeline(ShowStaticEquipment, ShowConsumableEquipment);
            RefreshItems();
        }
    }
}