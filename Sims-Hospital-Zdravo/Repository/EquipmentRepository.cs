/***********************************************************************
 * Module:  EquipmentRepository.cs
 * Author:  stjep
 * Purpose: Definition of the Class Repository.EquipmentRepository
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using DataHandler;
using System.Collections.ObjectModel;

namespace Repository
{
    public class EquipmentRepository
    {
        private ObservableCollection<Equipment> _equipment;
        private EquipmentDataHandler _equipmentDataHandler;

        public EquipmentRepository(EquipmentDataHandler equipmentDataHandler)
        {
            this._equipmentDataHandler = equipmentDataHandler;
            _equipment = new ObservableCollection<Equipment>();
            LoadDataFromFile();
        }

        public ObservableCollection<Equipment> ReadAll()
        {
            return _equipment;
        }

        public void Create(Equipment equipment)
        {
            this._equipment.Add(equipment);
            LoadDataToFile();
        }

        public void Delete(Equipment equipment)
        {
            this._equipment.Remove(equipment);
            LoadDataToFile();
        }

        public void Update(Equipment equipment)
        {
            foreach (Equipment eq in this._equipment)
            {
                if (eq.Id == equipment.Id)
                {
                    eq.Name = equipment.Name;
                    eq.Type = equipment.Type;
                    LoadDataToFile();
                    return;
                }
            }
        }

        public Equipment FindById(int id)
        {
            foreach (Equipment eq in _equipment)
            {
                if (eq.Id == id) return eq;
            }

            return null;
        }
        public Equipment FindByName(String name)
        {
            foreach (Equipment equipment in _equipment)
            {
                if (equipment.Name == name)
                    return equipment;
            }
            return null;
        }

        public void DeleteById(int id)
        {
            Equipment eq = FindById(id);
            Delete(eq);
            LoadDataToFile();
        }


        private void LoadDataFromFile()
        {
            _equipment = _equipmentDataHandler.ReadAll();
        }

        private void LoadDataToFile()
        {
            _equipmentDataHandler.Write(_equipment);
        }
    }
}