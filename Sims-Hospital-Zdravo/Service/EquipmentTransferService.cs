using Model;
using Repository;
using System.Collections.ObjectModel;
using Utils;
using System.Linq;
using System;

namespace Service
{
    class EquipmentTransferService
    {


        private EquipmentRepository equipmentRepository;
        private RelocationAppointmentRepository relocationAppointmentRepository;
        private RoomRepository roomRepository;
        private EquipmentTransferValidator validator;

        public EquipmentTransferService(EquipmentRepository equipmentRepository, RoomRepository roomRepository, RelocationAppointmentRepository relocationAppointmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
            this.roomRepository = roomRepository;
            this.relocationAppointmentRepository = relocationAppointmentRepository;
            validator = new EquipmentTransferValidator(roomRepository, equipmentRepository);
        }

        public void TransferFromWarehouse(int roomId, Equipment eq, int quantity, TimeInterval ti)
        {
            validator.validateTransferFromStorage(roomId, eq._Id, quantity);
            Room warehouse = roomRepository.FindByType(RoomType.WAREHOUSE);
            warehouse.RemoveEquipment(quantity, eq._Id);

            Room room = roomRepository.FindById(roomId);
            room.AddEquipment(quantity, eq._Id);

        }

        public void MakeAppointmentFromWarehouse(int roomId, Equipment eq, int quantity, TimeInterval ti)
        {
            Room warehouse = roomRepository.FindByType(RoomType.WAREHOUSE);
            warehouse.RemoveEquipment(quantity, eq._Id);

            Room room = roomRepository.FindById(roomId);

            RelocationAppointment relocationApp = new RelocationAppointment(warehouse, room, ti, new RoomEquipment(eq, quantity), GenerateId());
            relocationAppointmentRepository.Create(relocationApp);
        }


        private int GenerateId()
        {
            ObservableCollection<RelocationAppointment> relApp = relocationAppointmentRepository.ReadAll();
            Console.WriteLine(relApp.Count);
            if (relApp.Count == 0) return 1;
            RelocationAppointment last = relApp[relApp.Count - 1];
            return ++last._Id;
        }
    }
}
