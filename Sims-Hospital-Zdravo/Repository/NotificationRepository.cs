﻿using System.Collections.Generic;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Repository
{
    public class NotificationRepository
    {
        private NotificationDataHandler _notificationDataHandler;
        private List<Notification> notifications;

        public NotificationRepository(NotificationDataHandler notificationDataHandler)
        {
            this._notificationDataHandler = notificationDataHandler;
            this.notifications = new List<Notification>();
        }

        public List<Notification> ReadAll()
        {
            return notifications;
        }

        public void Create(Notification notification)
        {
            notifications.Add(notification);
            LoadDataToFile();
        }

        public void Update(Notification notification)
        {
            Notification not = FindById(notification.Id);
            if (not == null) return;
            not.Content = notification.Content;
            LoadDataToFile();
        }

        public void Delete(Notification notification)
        {
            notifications.Remove(notification);
            LoadDataToFile();
        }

        public void DeleteById(int id)
        {
            Notification notification = FindById(id);
            Delete(notification);
        }


        public Notification FindById(int id)
        {
            foreach (Notification notification in notifications)
            {
                if (notification.Id == id)
                {
                    return notification;
                }
            }

            return null;
        }

        public List<Notification> ReadAllManagerMedicineNotifications()
        {
            List<Notification> managerNotifications = new List<Notification>();
            foreach (Notification notification in notifications)
            {
                if (typeof(MedicineApprovalNotification).IsInstanceOfType(notification))
                {
                    managerNotifications.Add(notification);
                }
            }

            return managerNotifications;
        }

        public void LoadDataFromFiles()
        {
            notifications = _notificationDataHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            _notificationDataHandler.Write(notifications);
        }
    }
}