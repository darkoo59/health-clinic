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
            this._equipmentTransferService = equipmentTransferService;
        }

        public void FinishRelocationAppointment(int relocationAppointmentId)
        {
            _equipmentTransferService.FinishRelocationAppointment(relocationAppointmentId);
        }

        public void MakeRelocationAppointment(int fromRoomId, int toRoomId, Equipment eq, int quantity, TimeInterval ti)
        {
            _equipmentTransferService.MakeRelocationAppointment(fromRoomId, toRoomId, eq, quantity, ti);
        }

        public List<RelocationAppointment> ReadAll()
        {
            return _equipmentTransferService.ReadAll();
        }

        public List<TimeInterval> GetFreeTimeIntervals(Room fromRoom, Room toRoom)
        {
            return _equipmentTransferService.FindReservedTimeForRooms(fromRoom, toRoom);
        }
    }
}