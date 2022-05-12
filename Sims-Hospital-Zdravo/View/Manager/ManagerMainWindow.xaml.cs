using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Notifications.Wpf;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Application = System.Windows.Application;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerMainWindow : Window, INotificationObserver
    {
        private App app;
        private NotificationController notificationController;
        private NotificationManager notificationManager;

        public ManagerMainWindow()
        {
            app = Application.Current as App;
            this.notificationController = app._notificationController;
            notificationManager = new NotificationManager();
            app._taskScheduleTimer.AddObserver(this);
            InitializeComponent();

            ManagerContent.Source = new Uri("Renovations/ManagerRenovations.xaml", UriKind.Relative);
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }

        private void OnButtonKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.Key == Key.W)
            {
                ManagerMenu.RotateMenu(-1);
            }
            else if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.Key == Key.S)
            {
                ManagerMenu.RotateMenu(1);
            }
        }

        public void Notify(Notification notification)
        {
            notificationManager.Show(
                new NotificationContent { Title = "Medicine notification", Message = notification.Content },
                areaName: "WindowArea", expirationTime: TimeSpan.FromSeconds(30));
            notificationController.Delete(notification);
        }
    }
}