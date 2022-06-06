using System.Collections.Generic;
using System.Linq;
using Model;
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
            return notifications.OfType<ReviewMedicineNotification>().Cast<Notification>().ToList();
        }

        public List<Notification> ReadAllSecretaryNotifications()
        {
            //return notifications.OfType
            return null;
        }
        public List<Notification> ReadAllDoctorMedicineNotifications(int doctorId)
        {
            return notifications.OfType<MedicineCreatedNotification>().Where(x => x.DoctorId == doctorId).Cast<Notification>().ToList();
        }

        public List<Notification> ReadAllDoctorFreeDaysNotifications(int doctorId)
        {
            List<Notification> notificationsToReturn = new List<Notification>();
            foreach (FreeDaysNotification notification in notifications.OfType<FreeDaysNotification>().Cast<Notification>().ToList())
            {
                if (notification.FreeDaysRequest.Doctor._Id == doctorId)
                    notificationsToReturn.Add(notification);
            }
            return notificationsToReturn;
        }

        public List<Notification> ReadAllManagerMeetingsNotifications(int managerId)
        {
            List<Notification> notificationsToReturn = new List<Notification>();
            foreach(MeetingCreatedNotifications notification in notifications.OfType<MeetingCreatedNotifications>().Cast<Notification>().ToList())
            {
                if (notification.RoleType == RoleType.MANAGER && notification.UserId == managerId)
                    notificationsToReturn.Add(notification);
            }
            return notificationsToReturn;
        }

        public List<Notification> ReadAllDoctorMeetingsNotifications(int doctorId)
        {
            List<Notification> notificationsToReturn = new List<Notification>();
            foreach (MeetingCreatedNotifications notification in notifications.OfType<MeetingCreatedNotifications>().Cast<Notification>().ToList())
            {
                if(notification.RoleType == RoleType.DOCTOR && notification.UserId == doctorId)
                    notificationsToReturn.Add(notification);
            }
            return notificationsToReturn;
        }

        public List<Notification> ReadAllSecretaryMeetingsNotifications(int secretaryId)
        {
            List<Notification> notificationsToReturn = new List<Notification>();
            foreach (MeetingCreatedNotifications notification in notifications.OfType<MeetingCreatedNotifications>().Cast<Notification>().ToList())
            {
                if (notification.RoleType == RoleType.SECRETARY && notification.UserId == secretaryId)
                    notificationsToReturn.Add(notification);
            }
            return notificationsToReturn;
        }

        public void LoadDataFromFiles()
        {
            notifications = _notificationDataHandler.ReadAll();
        }

        private void LoadDataToFile()
        {
            _notificationDataHandler.Write(notifications);
        }
    }
}