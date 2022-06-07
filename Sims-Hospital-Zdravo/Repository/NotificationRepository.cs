﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;

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
            foreach (Notification not in _notifications)
            {
                if (not.Id == notification.Id)
                {
                    _notifications.Remove(not);
                    LoadDataToFile();
                    return;
                }
            }
        }

        public void DeleteById(int id)
        {
            Notification notification = FindById(id);
            Delete(notification);
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
            return _notifications.OfType<MedicineCreatedNotification>().Where(x => x.DoctorId == doctorId).Cast<Notification>().ToList();
        }

        public List<Notification> ReadAllDoctorFreeDaysNotifications(int doctorId)
        {
            LoadDataFromFiles();
            return _notifications.OfType<FreeDaysNotification>().Cast<Notification>().ToList().Cast<FreeDaysNotification>()
                .Where(notification => notification.FreeDaysRequest.Doctor._Id == doctorId)
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