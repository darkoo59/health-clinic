

using Model;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sims_Hospital_Zdravo.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private NotificationDataHandler _notificationDataHandler;
        private List<Notification> _notifications;

        public NotificationRepository()
        {
            _notificationDataHandler = new NotificationDataHandler();
            _notifications = new List<Notification>();
        }

        public List<Notification> ReadAll()
        {
            return _notificationDataHandler.ReadAll();
        }

        public void Create(Notification notification)
        {
            LoadDataFromFiles();
            _notifications.Add(notification);
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
            LoadDataFromFiles();
            _notifications.Remove(notification);
            LoadDataToFile();
        }

        public void DeleteById(int id)
        {
            Notification notification = FindById(id);
            _notifications.Remove(notification);
            LoadDataToFile();
        }

        public Notification FindById(int id)
        {
            LoadDataFromFiles();
            return _notifications.FirstOrDefault(notification => notification.Id == id);
        }

        public List<Notification> ReadAllManagerMedicineNotifications()
        {
            
            LoadDataFromFiles();
            return _notifications.OfType<ReviewMedicineNotification>().Cast<Notification>().ToList();
        }

        public List<Notification> ReadAllDoctorMedicineNotifications(int doctorId)
        {
            
            LoadDataFromFiles();
            List<Notification> notifications = new List<Notification>();
            foreach (Notification notification in _notifications) 
            {
                
                if(notification is MedicineCreatedNotification && ((MedicineCreatedNotification)notification).DoctorId == doctorId)
                {

                    notifications.Add(notification);
                }
            }
            return notifications;
            //return _notifications.OfType<MedicineCreatedNotification>()
                //.Where(x => x.DoctorId == doctorId).Cast<Notification>().ToList();
        }

        public List<Notification> ReadAllDoctorFreeDaysNotifications(int doctorId)
        {
            LoadDataFromFiles();
            return _notifications.OfType<FreeDaysNotification>().Cast<Notification>().ToList().Cast<FreeDaysNotification>()
                .Where(notification => notification.FreeDaysRequest.Doctor.Id == doctorId)
                .Cast<Notification>().ToList();
        }

        public List<Notification> ReadAllManagerMeetingsNotifications(int managerId)
        {
            LoadDataFromFiles();
            return _notifications.OfType<MeetingCreatedNotifications>().Cast<Notification>().ToList().Cast<MeetingCreatedNotifications>()
                .Where(notification => notification.RoleType == RoleType.MANAGER && notification.UserId == managerId)
                .Cast<Notification>().ToList();
        }

        public List<Notification> ReadAllDoctorMeetingsNotifications(int doctorId)
        {
            LoadDataFromFiles();
            return _notifications.OfType<MeetingCreatedNotifications>().Cast<Notification>().ToList().Cast<MeetingCreatedNotifications>()
                .Where(notification => notification.RoleType == RoleType.DOCTOR && notification.UserId == doctorId)
                .Cast<Notification>().ToList();
        }

        public List<Notification> ReadAllSecretaryMeetingsNotifications(int secretaryId)
        {
            LoadDataFromFiles();
            return _notifications.OfType<MeetingCreatedNotifications>().Cast<Notification>().ToList().Cast<MeetingCreatedNotifications>()
                .Where(notification => notification.RoleType == RoleType.SECRETARY && notification.UserId == secretaryId)
                .Cast<Notification>().ToList();
        }

        private void LoadDataFromFiles()
        {
            _notifications = _notificationDataHandler.ReadAll();
        }

        public List<Notification> ReadAllDoctorrequestForFreeDaysNotifications()
        {
            return null;
        }
      
        private void LoadDataToFile()
        {
            _notificationDataHandler.Write(_notifications);
        }

        public List<Notification> FindAll()
        {
            LoadDataFromFiles();
            return _notifications;
        }
    }
}