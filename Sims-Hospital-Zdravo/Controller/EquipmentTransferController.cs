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


        private EquipmentTransferService equipmentTransferService;

        public EquipmentTransferController(EquipmentTransferService equipmentTransferService)
        {
            this.equipmentTransferService = equipmentTransferService;
        }

        public void FinishRelocationAppointment(int relocationAppointmentId)
        {
            equipmentTransferService.FinishRelocationAppointment(relocationAppointmentId);
        }
        public void MakeRelocationAppointment(int fromRoomId, int toRoomId, Equipment eq, int quantity, TimeInterval ti)
        {
            equipmentTransferService.MakeRelocationAppointment(fromRoomId, toRoomId, eq, quantity, ti);
        }

        public ObservableCollection<RelocationAppointment> ReadAll()
        {
            return equipmentTransferService.ReadAll();
        }
        public List<TimeInterval> GetFreeTimeIntervals(int minutes, Room fromRoom, Room toRoom)
        {
            return equipmentTransferService.FindAvailableTimeForInterval(minutes, fromRoom, toRoom);
        }
    }
}
