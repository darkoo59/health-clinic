using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Service
{
    public class EquipmentService
    {
        private EquipmentRepository _equipmentRepository;

        public EquipmentService(EquipmentRepository equipmentRepository)
        {
            this._equipmentRepository = equipmentRepository;
        }

        public ObservableCollection<Equipment> ReadAll()
        {
            return _equipmentRepository.ReadAll();
        }

        public void Create(Equipment equipment)
        {
            _equipmentRepository.Create(equipment);
        }

        public void Delete(Equipment equipment)
        {
            _equipmentRepository.Delete(equipment);
        }

        public void Update(Equipment equipment)
        {
            _equipmentRepository.Update(equipment);
        }

        public Equipment FindById(int id)
        {
            return _equipmentRepository.FindById(id);
        }

        public void DeleteById(int id)
        {
            _equipmentRepository.DeleteById(id);
        }
    }
}