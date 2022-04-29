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
        private List<RelocationAppointment> relocationAppointments;
        private RelocationAppointmentDataHandler relocationAppointmentDataHandler;

        public RelocationAppointmentRepository(RelocationAppointmentDataHandler relocationAppointmentDataHandler)
        {
            this.relocationAppointmentDataHandler = relocationAppointmentDataHandler;
            relocationAppointments = new List<RelocationAppointment>();
            LoadDataFromFile();
        }

        public List<RelocationAppointment> ReadAll()
        {
            return relocationAppointments;
        }

        public void Create(RelocationAppointment appointment)
        {
            relocationAppointments.Add(appointment);
            LoadDataToFile();
        }

        public void Update(RelocationAppointment appointment)
        {
            foreach (RelocationAppointment ra in relocationAppointments)
            {
                if (ra._Id == appointment._Id)
                {
                    ra._RoomEquipment = appointment._RoomEquipment;
                    ra._Scheduled = appointment._Scheduled;
                    ra._Allocated = appointment._Allocated;
                    ra._FromRoom = appointment._FromRoom;
                    ra._ToRoom = appointment._ToRoom;
                    LoadDataToFile();
                    return;
                }
            }
        }

        public void Delete(RelocationAppointment appointment)
        {
            relocationAppointments.Remove(appointment);
            LoadDataToFile();
        }


        public RelocationAppointment FindById(int id)
        {
            foreach (RelocationAppointment ra in relocationAppointments)
            {
                if (ra._Id == id)
                {
                    return ra;
                }
            }

            return null;
        }

        public List<TimeInterval> FindTakenIntervalsForRoom(int roomId)
        {
            List<TimeInterval> intervals = new List<TimeInterval>();
            foreach (RelocationAppointment relocationAppointment in relocationAppointments)
            {
                if (relocationAppointment._FromRoom._Id == roomId || relocationAppointment._ToRoom._Id == roomId)
                {
                    intervals.Add(relocationAppointment._Scheduled);
                }
            }

            return intervals;
        }


        private void LoadDataToFile()
        {
            relocationAppointmentDataHandler.Write(relocationAppointments);
        }

        private void LoadDataFromFile()
        {
            relocationAppointments = relocationAppointmentDataHandler.ReadAll();
        }

        /// <pdGenerated>default getter</pdGenerated>
        public List<RelocationAppointment> GetRelocationAppointment()
        {
            if (relocationAppointments == null)
                relocationAppointments = new List<RelocationAppointment>();
            return relocationAppointments;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetRelocationAppointment(System.Collections.ArrayList newRelocationAppointment)
        {
            RemoveAllRelocationAppointment();
            foreach (RelocationAppointment oRelocationAppointment in newRelocationAppointment)
                AddRelocationAppointment(oRelocationAppointment);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddRelocationAppointment(RelocationAppointment newRelocationAppointment)
        {
            if (newRelocationAppointment == null)
                return;
            if (this.relocationAppointments == null)
                this.relocationAppointments = new List<RelocationAppointment>();
            if (!this.relocationAppointments.Contains(newRelocationAppointment))
                this.relocationAppointments.Add(newRelocationAppointment);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveRelocationAppointment(RelocationAppointment oldRelocationAppointment)
        {
            if (oldRelocationAppointment == null)
                return;
            if (this.relocationAppointments != null)
                if (this.relocationAppointments.Contains(oldRelocationAppointment))
                    this.relocationAppointments.Remove(oldRelocationAppointment);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllRelocationAppointment()
        {
            if (relocationAppointments != null)
                relocationAppointments.Clear();
        }
    }
}