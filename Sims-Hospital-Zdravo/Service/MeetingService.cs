using Model;
using Repository;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Service
{
    public class MeetingService
    {
        private IRoomRepository _roomRepository;
        private AccountRepository _accountRepository;
        private MeetingRepository _meetingRepository;
        private INotificationRepository _notificationRepository;

        public MeetingService(AccountRepository accRepo, MeetingRepository meetingRepo)
        {
            _roomRepository = new RoomRepository();
            _accountRepository = accRepo;
            _meetingRepository = meetingRepo;
            _notificationRepository = new NotificationRepository();
        }

        public void Create(Meeting meeting)
        {
            _meetingRepository.Create(meeting);
        }

        public void CreateMeetingWithNotifying(Meeting meeting, List<Notification> notifications)
        {
            Create(meeting);
            foreach (Notification notification in notifications)
                _notificationRepository.Create(notification);
        }

        public List<Room> ReadAllRooms()
        {
            return _roomRepository.FindAll().ToList();
        }

        public List<User> ReadAllPotentionalAttendees()
        {
            return (from User user in _accountRepository.ReadAll()
                where user._Role != RoleType.PATIENT
                select user).ToList();
        }
    }
}