/***********************************************************************
 * Module:  RelocationAppointmentRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.RelocationAppointmentRepository
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using DataHandler;
using Sims_Hospital_Zdravo.Interfaces;


namespace Repository
{
    public class RelocationAppointmentRepository : IRelocationRepository
    {
        private List<RelocationAppointment> _relocationAppointments;
        private RelocationAppointmentDataHandler _relocationAppointmentDataHandler;

        public RelocationAppointmentRepository()
        {
            _relocationAppointmentDataHandler = new RelocationAppointmentDataHandler();
            _relocationAppointments = new List<RelocationAppointment>();
        }

        public List<RelocationAppointment> FindAll()
        {
            return _relocationAppointmentDataHandler.ReadAll();
        }

        public void Create(RelocationAppointment appointment)
        {
            LoadDataFromFiles();
            _relocationAppointments.Add(appointment);
            LoadDataToFile();
        }

        public void Update(RelocationAppointment appointment)
        {
            LoadDataFromFiles();
            foreach (var ra in _relocationAppointments.Where(ra => ra.Id == appointment.Id))
            {
                ra.Update(appointment);
                LoadDataToFile();
                return;
            }
        }

        public void DeleteById(int id)
        {
            RelocationAppointment relocationAppointment = FindById(id);
            _relocationAppointments.Remove(relocationAppointment);
            LoadDataToFile();
        }

        public void Delete(RelocationAppointment appointment)
        {
            LoadDataFromFiles();
            _relocationAppointments.Remove(appointment);
            LoadDataToFile();
        }


        public RelocationAppointment FindById(int id)
        {
            LoadDataFromFiles();
            return _relocationAppointments.FirstOrDefault(ra => ra.Id == id);
        }

        public List<TimeInterval> FindTakenIntervalsForRoom(int roomId)
        {
            LoadDataFromFiles();
            return (from relocationAppointment in _relocationAppointments
                    where relocationAppointment.FromRoom.Id == roomId
                          || relocationAppointment.ToRoom.Id == roomId
                    select relocationAppointment.Scheduled)
                .ToList();
        }


        private void LoadDataToFile()
        {
            _relocationAppointmentDataHandler.Write(_relocationAppointments);
        }

        private void LoadDataFromFiles()
        {
            _relocationAppointments = _relocationAppointmentDataHandler.ReadAll();
        }
    }
}