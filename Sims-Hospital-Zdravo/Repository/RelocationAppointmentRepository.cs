/***********************************************************************
 * Module:  RelocationAppointmentRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RelocationAppointmentRepository
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using DataHandler;


namespace Repository
{
    public class RelocationAppointmentRepository
    {
        private List<RelocationAppointment> _relocationAppointments;
        private RelocationAppointmentDataHandler _relocationAppointmentDataHandler;

        public RelocationAppointmentRepository(RelocationAppointmentDataHandler relocationAppointmentDataHandler)
        {
            this._relocationAppointmentDataHandler = relocationAppointmentDataHandler;
            _relocationAppointments = new List<RelocationAppointment>();
            LoadDataFromFile();
        }

        public List<RelocationAppointment> ReadAll()
        {
            return _relocationAppointments;
        }

        public void Create(RelocationAppointment appointment)
        {
            _relocationAppointments.Add(appointment);
            LoadDataToFile();
        }

        public void Update(RelocationAppointment appointment)
        {
            foreach (RelocationAppointment ra in _relocationAppointments)
            {
                if (ra.Id == appointment.Id)
                {
                    ra.RoomEquipment = appointment.RoomEquipment;
                    ra.Scheduled = appointment.Scheduled;
                    ra.FromRoom = appointment.FromRoom;
                    ra.ToRoom = appointment.ToRoom;
                    LoadDataToFile();
                    return;
                }
            }
        }

        public void Delete(RelocationAppointment appointment)
        {
            _relocationAppointments.Remove(appointment);
            LoadDataToFile();
        }


        public RelocationAppointment FindById(int id)
        {
            foreach (RelocationAppointment ra in _relocationAppointments)
            {
                if (ra.Id == id)
                {
                    return ra;
                }
            }

            return null;
        }

        public List<TimeInterval> FindTakenIntervalsForRoom(int roomId)
        {
            List<TimeInterval> intervals = new List<TimeInterval>();
            foreach (RelocationAppointment relocationAppointment in _relocationAppointments)
            {
                if (relocationAppointment.FromRoom.Id == roomId || relocationAppointment.ToRoom.Id == roomId)
                {
                    intervals.Add(relocationAppointment.Scheduled);
                }
            }

            return intervals;
        }


        private void LoadDataToFile()
        {
            _relocationAppointmentDataHandler.Write(_relocationAppointments);
        }

        private void LoadDataFromFile()
        {
            _relocationAppointments = _relocationAppointmentDataHandler.ReadAll();
        }
    }
}