using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Repository
{
    public class SuppliesAcquisitionRepository:ISuppliesAcquisitionRepository
    {
        private List<SuppliesAcquisition> _suppliesAcquisitions;
        private SuppliesAcquisitionDataHandler _suppliesAcquisitionDataHandler;
        
        public SuppliesAcquisitionRepository(SuppliesAcquisitionDataHandler dataHandler)
        {
            this._suppliesAcquisitionDataHandler = dataHandler;
            _suppliesAcquisitions = new List<SuppliesAcquisition>();
            LoadDataFromFile();
        }
        
        public void Create(SuppliesAcquisition suppliesAcquisition)
        {
            _suppliesAcquisitions.Add(suppliesAcquisition);
            LoadDataToFile();
        }

        public void Delete(SuppliesAcquisition obj)
        {
            LoadDataFromFile();
            _suppliesAcquisitions.Remove(obj);
            LoadDataToFile();
        }

        public void Update(SuppliesAcquisition suppliesAcquisition)
        {
            foreach(SuppliesAcquisition acquistion in _suppliesAcquisitions)
            {
                if(acquistion.Id == suppliesAcquisition.Id)
                {
                    acquistion.RoomEquipments = suppliesAcquisition.RoomEquipments;
                    acquistion.Time = suppliesAcquisition.Time;
                    LoadDataToFile();
                    return;
                }
            }
        }

        public void DeleteById(int id)
        {
            LoadDataFromFile();
            foreach (var acquisition in _suppliesAcquisitions.Where(acquisition => acquisition.Id == id))
            {
                _suppliesAcquisitions.Remove(acquisition);
                LoadDataToFile();
                return;
            }
        }

        public List<SuppliesAcquisition> FindAll()
        {
            LoadDataFromFile();
            return _suppliesAcquisitions;
        }

        public SuppliesAcquisition FindById(int id)
        {
            LoadDataFromFile();
            return _suppliesAcquisitions.FirstOrDefault(acquisition => acquisition.Id == id);
        }
        
        
        
        private void LoadDataToFile()
        {
            _suppliesAcquisitionDataHandler.Write(_suppliesAcquisitions);
        }

        private void LoadDataFromFile()
        {
            _suppliesAcquisitions = _suppliesAcquisitionDataHandler.ReadAll();
        }
    }
}
