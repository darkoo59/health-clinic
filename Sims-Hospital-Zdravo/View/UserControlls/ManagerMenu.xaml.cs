using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Sims_Hospital_Zdravo.View.Manager;

namespace Sims_Hospital_Zdravo.View.UserControlls
{
    public partial class ManagerMenu : UserControl
    {
        private Frame ManagerContent;
        private string[] menuitems = { "Equipment", "Renovations", "Rooms", "Surveys" };
        private int currentMenuItem = 1;

        public ManagerMenu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(UserControl_Loaded);
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs args)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is ManagerMainWindow)
            {
                ManagerContent = ((ManagerMainWindow)parentWindow).ManagerContent;
            }
        }

        private void SwitchMenu_Click(object sender, RoutedEventArgs e)
        {
            string buttonText = GetButtonText(sender);
            SwitchMenu(buttonText);
        }

        private void SwitchMenu(string text)
        {
            Border border = GetBorderByChildButtonContent(text);
            Console.WriteLine("Clicked on menu");

            if (border != null)
            {
                SetAllBordersBackgroundsToDefault();
                border.Background = new SolidColorBrush(Color.FromRgb(47, 52, 61));
                SwitchPage(text);
            }
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

        private void SwitchPage(string text)
        {
            switch (text)
            {
                case "Renovations":
                    ManagerContent.Source = new Uri("ManagerRenovations.xaml", UriKind.Relative);
                    break;
                case "Equipment":
                    ManagerContent.Source = new Uri("ManagerEquipment.xaml", UriKind.Relative);
                    break;
                case "Surveys":
                    break;
                case "Rooms":
                    ManagerContent.Source = new Uri("ManagerRooms.xaml", UriKind.Relative);
                    break;
            }
        }

        private void SetAllBordersBackgroundsToDefault()
        {
            RenovationsBorder.Background = new SolidColorBrush(Color.FromRgb(39, 44, 51));
            EquipmentBorder.Background = new SolidColorBrush(Color.FromRgb(39, 44, 51));
            SurveysBorder.Background = new SolidColorBrush(Color.FromRgb(39, 44, 51));
            RoomsBorder.Background = new SolidColorBrush(Color.FromRgb(39, 44, 51));
        }


        private Border GetBorderByChildButtonContent(string content)
        {
            switch (content)
            {
                case "Renovations": return RenovationsBorder;
                case "Equipment": return EquipmentBorder;
                case "Surveys": return SurveysBorder;
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