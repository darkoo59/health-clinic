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
            if (notification._ValidationType == MedicineValidationType.MEDICINE_APPROVED)
            {
                return "Medicine " + notification._Medicine._Name + " has been approved!";
            }

            return "Medicine " + notification._Medicine._Name + " has beeen declined!";
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            app._taskScheduleTimer.RemoveObserver(this);
        }
    }
}