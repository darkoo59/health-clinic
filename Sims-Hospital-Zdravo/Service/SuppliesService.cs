using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Repository;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Model
{
    public class SuppliesService
    {
        private IRoomRepository _roomRepository;
        private IEquipmentRepository _equipmentRepository;
        private ISuppliesAcquisitionRepository _suppliesAcquisitionRepository;

        public SuppliesService()
        {
            _roomRepository = new RoomRepository();
            _equipmentRepository = new EquipmentRepository();
            _suppliesAcquisitionRepository = new SuppliesAcquisitionRepository();
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

        public List<SuppliesAcquisition> ReadAllSuppliesAcquisitions()
        {
            return this._suppliesAcquisitionRepository.FindAll();
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
                foreach (var roomEquipmentInWarehouse in roomEquipmentsInWarehouse.Where
                             (roomEquipmentInWarehouse => roomEquipmentToAdd.Equipment.Id
                                                          == roomEquipmentInWarehouse.Equipment.Id))
                {
                    roomEquipmentInWarehouse.Quantity += roomEquipmentToAdd.Quantity;
                    equipmentNamesThatIsUpdatedAlready.Add(roomEquipmentToAdd.Equipment.Name);
                }
            }
            roomEquipmentsInWarehouse.AddRange(suppliesAcquisition.RoomEquipments.Where
                (roomEquipmentToAdd => !equipmentNamesThatIsUpdatedAlready.Contains
                    (roomEquipmentToAdd.Equipment.Name)));
            room.RoomEquipment = roomEquipmentsInWarehouse;
            _roomRepository.Update(room);
        }

        public int GenerateEquipmentId()
        {
            List<Equipment> equipments = _equipmentRepository.FindAll();
            int id = 0;
            List<int> ids = equipments.Select(equipment => equipment.Id).ToList();
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }
    }
}