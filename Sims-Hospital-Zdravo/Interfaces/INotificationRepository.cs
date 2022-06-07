using System.Collections.Generic;
using Model;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface INotificationRepository : IGenericCRUD<Notification>
    {
        Notification FindById(int id);
        List<Notification> ReadAllManagerMedicineNotifications();
        List<Notification> ReadAllDoctorMedicineNotifications(int doctorId);
        List<Notification> ReadAllDoctorFreeDaysNotifications(int doctorId);
        List<Notification> ReadAllManagerMeetingsNotifications(int managerId);
        List<Notification> ReadAllDoctorMeetingsNotifications(int doctorId);
        List<Notification> ReadAllSecretaryMeetingsNotifications(int secretaryId);
    }
}