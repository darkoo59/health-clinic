using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Service;

namespace Controller
{
    public class EquipmentController
    {
        private EquipmentService _equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            this._equipmentService = equipmentService;
        }

        public ObservableCollection<Equipment> ReadAll()
        {
            return _equipmentService.ReadAll();
        }

        public void Create(Equipment equipment)
        {
            _equipmentService.Create(equipment);
        }

        public void Delete(Equipment equipment)
        {
            _equipmentService.Delete(equipment);
        }

        public void Update(Equipment equipment)
        {
            _equipmentService.Update(equipment);
        }

        public Equipment FindById(int id)
        {
            return _equipmentService.FindById(id);
        }

        public void DeleteById(int id)
        {
            _equipmentService.DeleteById(id);
        }
    }
}