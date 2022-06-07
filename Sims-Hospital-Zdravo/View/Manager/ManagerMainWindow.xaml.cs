using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Notifications.Wpf;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Login;
using Sims_Hospital_Zdravo.View.UserControlls;
using Application = System.Windows.Application;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerMainWindow : Window, INotificationObserver
    {
        private App app;
        private NotificationController notificationController;
        private NotificationManager notificationManager;

        public ManagerMenu Menu => ManagerMenu;
        public Frame ManagerPager => ManagerContent;

        public ManagerMainWindow()
        {
            app = Application.Current as App;
            notificationController = new NotificationController();
            notificationManager = new NotificationManager();

            app._taskScheduleTimer.AddObserver(this);
            InitializeComponent();

            ManagerContent.Source = new Uri("Renovations/ManagerRenovations.xaml", UriKind.Relative);
        }

        public void Notify(Notification notification)
        {
            if (notification as ReviewMedicineNotification != null)
            {
                notificationManager.Show(
                    new NotificationContent { Title = "Medicine notification", Message = CreateNotificationMessageFromNotification((ReviewMedicineNotification)notification) },
                    areaName: "WindowArea", expirationTime: TimeSpan.FromSeconds(30));
                notificationController.Delete(notification);
            }
            else if (notification as MeetingCreatedNotifications != null)
            {
                MeetingCreatedNotifications meetingNotification = notification as MeetingCreatedNotifications;
                notificationManager.Show(
                    new NotificationContent { Title = "Meeting notification", Message = "You have new meeting at " + meetingNotification.MeetingStart.ToString() },
                    areaName: "WindowArea", expirationTime: TimeSpan.FromSeconds(30));
                notificationController.Delete(notification);
            }
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