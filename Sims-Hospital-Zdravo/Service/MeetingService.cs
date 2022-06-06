using Model;
using Repository;
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

        public MeetingService(IRoomRepository roomRepo, AccountRepository accRepo)
        {
            this._roomRepository = roomRepo;
            this._accountRepository = accRepo;
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