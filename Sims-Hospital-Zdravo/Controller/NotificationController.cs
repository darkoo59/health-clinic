using System.Collections.Generic;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Controller
{
    public class NotificationController
    {
        private NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            this._notificationService = notificationService;
        }

        public List<Notification> ReadAll()
        {
            return _notificationService.ReadAll();
        }

        public Notification FindById(int id)
        {
            return _notificationService.FindById(id);
        }

        public void Delete(Notification notification)
        {
            _notificationService.Delete(notification);
        }

        public void DeleteById(int id)
        {
            _notificationService.DeleteById(id);
        }

        public void Create(Notification notification)
        {
            _notificationService.Create(notification);
        }

        public List<Notification> ReadAllManagerMedicineNotifications()
        {
            return _notificationService.ReadAllManagerMedicineNotifications();
        }

        public List<Notification> ReadAllDoctorMedicineNotifications(int doctorId)
        {
            return _notificationService.ReadAllDoctorMedicineNotifications(doctorId);
        }

        public List<Notification> ReadAllManagerMeetingsNotifications(int managerId)
        {
            return _notificationService.ReadAllManagerMeetingsNotifications(managerId);
        }

        public List<Notification> ReadAllDoctorMeetingsNotifications(int doctorId)
        {
            return _notificationService.ReadAllDoctorMeetingsNotifications(doctorId);
        }

        public List<Notification> ReadAllSecretaryMeetingsNotifications(int secretaryId)
        {
            return _notificationService.ReadAllSecretaryMeetingsNotifications(secretaryId);
        }

        public int GenerateId()
        {
            return _notificationService.GenerateId();
        }
    }
}