using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using Sims_Hospital_Zdravo.View.Manager;

namespace Sims_Hospital_Zdravo.View.UserControlls
{
    public partial class ManagerMenu : UserControl
    {
        private Frame ManagerContent;
        private Label HeaderLabel;
        private string[] menuitems = { "Equipment", "Renovations", "Rooms", "Medicines" };
        private int currentMenuItem = 1;

        public ManagerMenu()
        {
            InitializeComponent();
        }


        public void RotateMenu(int direction)
        {
            currentMenuItem += direction;
            if (currentMenuItem >= menuitems.Length)
            {
                currentMenuItem = 0;
            }

            if (currentMenuItem < 0)
            {
                currentMenuItem = menuitems.Length - 1;
            }

            SwitchMenu(menuitems[currentMenuItem]);
        }

        public void SetMenuItem(string text)
        {
            SetCurrentMenuItem(text);
            SwitchMenu(text);
            SetMenuItemTitle(text);
        }

        private void SwitchMenu(string text)
        {
            Border border = GetBorderByChildButtonContent(text);

            if (border != null)
            {
                SetAllBordersBackgroundsToDefault();
                border.Background = new SolidColorBrush(Color.FromRgb(47, 52, 61));
                SwitchPage(text);
                SetMenuItemTitle(text);
                ShowTooltipForButton(text);
            }
        }

        private void SetMenuItemTitle(string text)
        {
            HeaderLabel.Content = text;
        }

        private void SetCurrentMenuItem(string text)
        {
            switch (text)
            {
                case "Renovations":
                    currentMenuItem = 1;
                    break;
                case "Equipment":
                    currentMenuItem = 0;
                    break;
                case "Medicines":
                    currentMenuItem = 3;
                    break;
                case "Rooms":
                    currentMenuItem = 2;
                    break;
            }
        }


        private void SwitchPage(string text)
        {
            switch (text)
            {
                case "Renovations":
                    ManagerContent.Source = new Uri("Renovations/ManagerRenovations.xaml", UriKind.Relative);
                    break;
                case "Equipment":
                    ManagerContent.Source = new Uri("Equipment/ManagerEquipment.xaml", UriKind.Relative);
                    break;
                case "Medicines":
                    ManagerContent.Source = new Uri("Medicines/ManagerMedicines.xaml", UriKind.Relative);
                    break;
                case "Rooms":
                    ManagerContent.Source = new Uri("Rooms/ManagerRooms.xaml", UriKind.Relative);
                    break;
            }
        }

        private void ShowTooltipForButton(string text)
        {
            Button btn = GetButtonByText(text);
            if (btn == null) return;
            if (btn.ToolTip is ToolTip)
            {
                var castToolTip = (ToolTip)btn.ToolTip;
                castToolTip.IsOpen = true;
            }
            else
            {
                var toolTip = new ToolTip();
                toolTip.Content = btn.ToolTip;
                toolTip.StaysOpen = false;
                toolTip.PlacementTarget = btn;
                toolTip.Placement = PlacementMode.Bottom;
                toolTip.IsOpen = true;
                ScheduleToolTipRemoval(toolTip);
            }
        }

        private void ScheduleToolTipRemoval(ToolTip toolTip)
        {
            DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.5) };
            timer.Tick += delegate(object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                if (toolTip.IsOpen) toolTip.IsOpen = false;
            };
            timer.Start();
        }

        private Button GetButtonByText(string text)
        {
            switch (text)
            {
                case "Medicines": return BtnMedicines;
                case "Equipment": return BtnEquipment;
                case "Renovations": return BtnRenovations;
                case "Rooms": return BtnRooms;
                default: return null;
            }
        }

        private void SetAllBordersBackgroundsToDefault()
        {
            RenovationsBorder.Background = new SolidColorBrush(Color.FromRgb(39, 44, 51));
            EquipmentBorder.Background = new SolidColorBrush(Color.FromRgb(39, 44, 51));
            MedicinesBorder.Background = new SolidColorBrush(Color.FromRgb(39, 44, 51));
            RoomsBorder.Background = new SolidColorBrush(Color.FromRgb(39, 44, 51));
        }


        private Border GetBorderByChildButtonContent(string content)
        {
            switch (content)
            {
                case "Renovations": return RenovationsBorder;
                case "Equipment": return EquipmentBorder;
                case "Medicines": return MedicinesBorder;
                case "Rooms": return RoomsBorder;
                default: return null;
            }
        }


        private string GetButtonText(object sender)
        {
            Button b = sender as Button;
            return b.Content.ToString();
        }
    }
}