using System.Collections.Generic;
using System.Linq;
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
            LoadDataFromFiles();
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
            return notifications.OfType<MedicineCreatedNotification>().Cast<Notification>().ToList();
        }

        private void LoadDataFromFiles()
        {
            notifications = _notificationDataHandler.ReadAll();
        }

        private void LoadDataToFile()
        {
            _notificationDataHandler.Write(notifications);
        }
    }
}