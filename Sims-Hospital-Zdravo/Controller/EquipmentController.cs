using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Service;
using Sims_Hospital_Zdravo.Service;

namespace Sims_Hospital_Zdravo.Controller
{
    class EquipmentController
    {
        private EquipmentService equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            this.equipmentService = equipmentService;
        }

        public ObservableCollection<Equipment> ReadAll()
        {
            return equipmentService.ReadAll();
        }

        public void Create(Equipment equipment)
        {
            equipmentService.Create(equipment);
        }

        public void Delete(Equipment equipment)
        {
            equipmentService.Delete(equipment);
        }

        public void Update(Equipment equipment)
        {
            equipmentService.Update(equipment);
        }

        public Equipment FindById(int id)
        {
            return equipmentService.FindById(id);
        }

        public void DeleteById(int id)
        {
            equipmentService.DeleteById(id);

        }
    }
}
