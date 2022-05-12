using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Service;

namespace Sims_Hospital_Zdravo.Repository
{
    public class SuppliesAcquisitionRepository
    {
        private ObservableCollection<SuppliesAcquisition> _suppliesAcquisitions;
        private SuppliesAcquisitionDataHandler _suppliesAcquisitionDataHandler;
        
        public SuppliesAcquisitionRepository(SuppliesAcquisitionDataHandler dataHandler)
        {
            this._suppliesAcquisitionDataHandler = dataHandler;
            _suppliesAcquisitions = new ObservableCollection<SuppliesAcquisition>();
            LoadDataFromFile();
        }
        
        public void Create(SuppliesAcquisition suppliesAcquisition)
        {
            _suppliesAcquisitions.Add(suppliesAcquisition);
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

        public ObservableCollection<SuppliesAcquisition> ReadAll()
        {
            return _suppliesAcquisitions;
        }

        public SuppliesAcquisition FindById(int id)
        {
            foreach (SuppliesAcquisition acquisition in _suppliesAcquisitions)
            {
                if (acquisition.Id == id)
                    return acquisition;
            }
            return null;
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
