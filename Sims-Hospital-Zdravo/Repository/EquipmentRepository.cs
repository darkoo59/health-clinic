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
        private ObservableCollection<Equipment> equipment;
        private EquipmentDataHandler equipmentDataHandler;

        public EquipmentRepository(EquipmentDataHandler equipmentDataHandler)
        {
            this.equipmentDataHandler = equipmentDataHandler;
            equipment = new ObservableCollection<Equipment>();
            LoadDataFromFile();
        }
        public ObservableCollection<Equipment> ReadAll()
        {
            return equipment;
        }

        public void Create(Equipment equipment)
        {
            this.equipment.Add(equipment);
            LoadDataToFile();

        }

        public void Delete(Equipment equipment)
        {
            this.equipment.Remove(equipment);
            LoadDataToFile();

        }

        public void Update(Equipment equipment)
        {
            foreach(Equipment eq in this.equipment)
            {
                if(eq._Id == equipment._Id)
                {
                    eq._Name = equipment._Name;
                    eq._Type = equipment._Type;
                    LoadDataToFile();
                    return;
                }
            }
        }

        public Equipment FindById(int id)
        {
            foreach(Equipment eq in equipment)
            {
                if (eq._Id == id) return eq;
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
            equipment = equipmentDataHandler.ReadAll();
        }

        private void LoadDataToFile()
        {
            equipmentDataHandler.Write(equipment);
        }




    }
}