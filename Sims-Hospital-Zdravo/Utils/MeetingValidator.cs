using System;
using System.Collections.Generic;
using System.Windows;
using Model;
using Repository;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Utils
{
    public class MeetingValidator
    {
        private IRoomRepository _roomRepository;
        private App app;
        public MeetingValidator()
        {
            app = Application.Current as App;
            _roomRepository = new RoomRepository();
        }
        
        public void ValidateMeeting(Meeting meeting)
        {
            ValidateRoomExists(meeting);
            ValidateAttendeeExists(meeting);
            ValidateDateExist(meeting);
        }
        
        private void ValidateRoomExists(Meeting meeting)
        {
            if (meeting.Room == null)
            {
                if(app._currentLanguage.Equals("en-US"))
                    throw new Exception("Room not selected!");
                else 
                    throw new Exception("Soba nije selektovana!");
            }
            foreach (Room rm in _roomRepository.FindAll())
            {
                if (rm.Id == meeting.Room.Id)
                {
                    return;
                }
            }

            throw new Exception("Room with given Id doesn't exist!");
        }
        
        private void ValidateAttendeeExists(Meeting meeting)
        {
            if (meeting.OptionalAttendees.Count == 0 && meeting.RequiredAttendees.Count == 0)
            {
                if(app._currentLanguage.Equals("en-US"))
                    throw new Exception("Please select at least one attendee!");
                else 
                    throw new Exception("Selektujte barem jednog zaposlenog!");
            }
        }
        
        private void ValidateDateExist(Meeting meeting)
        {
            if (meeting.Start == DateTime.MinValue)
            {
                if (app._currentLanguage.Equals("en-US"))
                    throw new Exception("Please select date!");
                else
                    throw new Exception("Molimo Vas da selektujete datum!");
            }
        }
    }
}