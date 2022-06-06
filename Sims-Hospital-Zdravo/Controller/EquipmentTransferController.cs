using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class EquipmentTransferController
    {
        private EquipmentTransferService _equipmentTransferService;

        public EquipmentTransferController(EquipmentTransferService equipmentTransferService)
        {
            _equipmentTransferService = equipmentTransferService;
        }

        public void FinishRelocationAppointment(int relocationAppointmentId)
        {
            _equipmentTransferService.FinishRelocationAppointment(relocationAppointmentId);
        }

        public void MakeRelocationAppointment(RelocationAppointment relocationAppointment)
        {
            _equipmentTransferService.MakeRelocationAppointment(relocationAppointment);
        }

        public List<RelocationAppointment> FindAll()
        {
            return _equipmentTransferService.FindAll();
        }

        public List<TimeInterval> GetTakenTimeIntervals(Room fromRoom, Room toRoom)
        {
            return _equipmentTransferService.FindReservedTimeForRooms(fromRoom, toRoom);
        }

        public int GenerateId()
        {
            return _equipmentTransferService.GenerateId();
        }
    }
}