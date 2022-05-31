using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Notifications.Wpf;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Login;
using Application = System.Windows.Application;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerMainWindow : Window, INotificationObserver
    {
        private App app;
        private NotificationController notificationController;
        private NotificationManager notificationManager;
        private AccountController accountController;

        public ManagerMainWindow()
        {
            app = Application.Current as App;
            notificationController = app._notificationController;
            accountController = app._accountController;
            notificationManager = new NotificationManager();

            app._taskScheduleTimer.AddObserver(this);
            InitializeComponent();

            ManagerContent.Source = new Uri("Renovations/ManagerRenovations.xaml", UriKind.Relative);
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }

        private void OnButtonKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && e.Key == Key.W)
            {
                ManagerMenu.RotateMenu(-1);
            }
            else if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && e.Key == Key.S)
            {
                ManagerMenu.RotateMenu(1);
            }
            else if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && e.Key == Key.D1)
            {
                ManagerMenu.SetMenuItem("Equipment");
            }
            else if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && e.Key == Key.D2)
            {
                ManagerMenu.SetMenuItem("Renovations");
            }
            else if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && e.Key == Key.D3)
            {
                ManagerMenu.SetMenuItem("Rooms");
            }
            else if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && e.Key == Key.D4)
            {
                ManagerMenu.SetMenuItem("Medicines");
            }
            else if (e.Key == Key.Escape)
            {
                if (ManagerContent.CanGoBack && !IsMainWindowPage(ManagerContent.Source.OriginalString))
                {
                    ManagerContent.GoBack();
                }
            }
        }

        private bool IsMainWindowPage(string uri)
        {
            switch (uri)
            {
                case "Sims-Hospital-Zdravo;component/view/manager/Rooms/ManagerRooms.xaml": return true;
                case "Sims-Hospital-Zdravo;component/view/manager/Equipment/ManagerEquipment.xaml": return true;
                case "Sims-Hospital-Zdravo;component/view/manager/Medicines/ManagerMedicines.xaml": return true;
                case "Sims-Hospital-Zdravo;component/view/manager/Renovations/ManagerRenovations.xaml": return true;
                default: return false;
            }
        }

        public void Logout()
        {
            accountController.Logout();
            LoginMainWindow loginMainWindow = new LoginMainWindow();
            loginMainWindow.Show();
            Close();
        }

        public void Notify(Notification notification)
        {
            ReviewMedicineNotification reviewMedicineNotification = notification as ReviewMedicineNotification;
            if (reviewMedicineNotification is null) return;

            notificationManager.Show(
                new NotificationContent { Title = "Medicine notification", Message = CreateNotificationMessageFromNotification(reviewMedicineNotification) },
                areaName: "WindowArea", expirationTime: TimeSpan.FromSeconds(30));
            notificationController.Delete(notification);
        }


        private string CreateNotificationMessageFromNotification(ReviewMedicineNotification notification)
        {
            if (notification.ValidationType == MedicineValidationType.MEDICINE_APPROVED)
            {
                return "Medicine " + notification.Medicine.Name + " has been approved!";
            }

            return "Medicine " + notification.Medicine.Name + " has beeen declined!";
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            app._taskScheduleTimer.RemoveObserver(this);
        }
    }
}