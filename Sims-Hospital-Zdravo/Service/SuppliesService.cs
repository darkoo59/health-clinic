using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Repository;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Model
{
    public class SuppliesService
    {
        private RoomRepository _roomRepository;
        private EquipmentRepository _equipmentRepository;
        private SuppliesAcquisitionRepository _suppliesAcquisitionRepository;

        public SuppliesService(RoomRepository roomRepo, EquipmentRepository equipmentRepo,SuppliesAcquisitionRepository suppliesRepo)
        {
            this._roomRepository = roomRepo;
            this._equipmentRepository = equipmentRepo;
            this._suppliesAcquisitionRepository = suppliesRepo;
        }


        public void Create(SuppliesAcquisition suppliesAcquisition)
        {
            _suppliesAcquisitionRepository.Create(suppliesAcquisition);
        }

        public void Update(SuppliesAcquisition suppliesAcquisition)
        {
            _suppliesAcquisitionRepository.Update(suppliesAcquisition);
        }

        public List<RoomEquipment> FindAllEquipment()
        {
            return this._roomRepository.FindAllEquipment();
        }

        public ObservableCollection<SuppliesAcquisition> ReadAllSuppliesAcquisitions()
        {
            return this._suppliesAcquisitionRepository.ReadAll();
        }

        public SuppliesAcquisition FindSuppliesAcquisitionById(int id)
        {
            return this._suppliesAcquisitionRepository.FindById(id);
        }

        public Equipment CreateNewEquipment(string name)
        {
            if (_equipmentRepository.FindByName(name) != null)
                return _equipmentRepository.FindByName(name);

            Equipment _equipment = new Equipment(GenerateEquipmentId(), name, EquipmentType.Consumable);
            _equipmentRepository.Create(_equipment);
            return _equipment;
        }

        public void FinishSuppliesAcquisition(int acquisitionId)
        {
            SuppliesAcquisition suppliesAcquisition = FindSuppliesAcquisitionById(acquisitionId);
            Room room = _roomRepository.FindByType(RoomType.WAREHOUSE);
            List<RoomEquipment> roomEquipmentsInWarehouse = room.RoomEquipment;
            List<String> equipmentNamesThatIsUpdatedAlready = new List<String>();
            foreach (RoomEquipment roomEquipmentToAdd in suppliesAcquisition.RoomEquipments)
            {
                foreach (RoomEquipment roomEquipmentInWarehouse in roomEquipmentsInWarehouse)
                {
                    if (roomEquipmentToAdd.Equipment.Id == roomEquipmentInWarehouse.Equipment.Id)
                    {
                        roomEquipmentInWarehouse.Quantity += roomEquipmentToAdd.Quantity;
                        equipmentNamesThatIsUpdatedAlready.Add(roomEquipmentToAdd.Equipment.Name);
                    }
                }
            }

            foreach (RoomEquipment roomEquipmentToAdd in suppliesAcquisition.RoomEquipments)
            {
                if (!equipmentNamesThatIsUpdatedAlready.Contains(roomEquipmentToAdd.Equipment.Name))
                    roomEquipmentsInWarehouse.Add(roomEquipmentToAdd);
            }

            room.RoomEquipment = roomEquipmentsInWarehouse;
            _roomRepository.Update(room);
        }

        public int GenerateSuppliesAcquistionId()
        {
            ObservableCollection<SuppliesAcquisition> supplies = _suppliesAcquisitionRepository.ReadAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (SuppliesAcquisition supplie in supplies)
            {
                ids.Add(supplie.Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }
        
        public int GenerateEquipmentId()
        {
            ObservableCollection<Equipment> equipments = _equipmentRepository.ReadAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Equipment equipment in equipments)
            {
                ids.Add(equipment.Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }
        
    }
}