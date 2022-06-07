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
using System.Linq;
using Sims_Hospital_Zdravo.Interfaces;

namespace Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private List<Equipment> _equipment;
        private EquipmentDataHandler _equipmentDataHandler;

        public EquipmentRepository()
        {
            _equipmentDataHandler = new EquipmentDataHandler();
            _equipment = new List<Equipment>();
        }

        public List<Equipment> FindAll()
        {
            return _equipmentDataHandler.ReadAll();
        }

        public void Create(Equipment equipment)
        {
            LoadDataFromFiles();
            _equipment.Add(equipment);
            LoadDataToFile();
        }

        public void Delete(Equipment equipment)
        {
            LoadDataFromFiles();
            _equipment.Remove(equipment);
            LoadDataToFile();
        }

        public void Update(Equipment equipment)
        {
            LoadDataFromFiles();
            foreach (var eq in _equipment.Where(eq => eq.Id == equipment.Id))
            {
                eq.Update(equipment);
                LoadDataToFile();
                return;
            }
        }

        public Equipment FindById(int id)
        {
            LoadDataFromFiles();
            return _equipment.FirstOrDefault(eq => eq.Id == id);
        }

        public Equipment FindByName(String name)
        {
            LoadDataFromFiles();
            return _equipment.FirstOrDefault(equipment => equipment.Name == name);
        }

        public void DeleteById(int id)
        {
            Equipment eq = FindById(id);
            Delete(eq);
            LoadDataToFile();
        }


        private void LoadDataFromFiles()
        {
            _equipment = _equipmentDataHandler.ReadAll();
        }

        private void LoadDataToFile()
        {
            _equipmentDataHandler.Write(_equipment);
        }
    }
}