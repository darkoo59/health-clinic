using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace Sims_Hospital_Zdravo.Service
{
    class EquipmentService
    {
        private EquipmentRepository equipmentRepository;

        public EquipmentService(EquipmentRepository equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }
        public ObservableCollection<Equipment> ReadAll()
        {
            return equipmentRepository.ReadAll();
        }

        public void Create(Equipment equipment)
        {
            equipmentRepository.Create(equipment);
        }

        public void Delete(Equipment equipment)
        {
            equipmentRepository.Delete(equipment);
        }

        public void Update(Equipment equipment)
        {
            equipmentRepository.Update(equipment);
        }

        public Equipment FindById(int id)
        {
            return equipmentRepository.FindById(id);
        }

        public void DeleteById(int id)
        {
            equipmentRepository.DeleteById(id);

        }
    }

}
