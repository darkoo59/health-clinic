using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Service;

namespace Sims_Hospital_Zdravo.Controller
{
    public class SuppliesController
    {
        private SuppliesService _suppliesService;

        public SuppliesController(SuppliesService service)
        {
            this._suppliesService = service;
        }

        public void Update(SuppliesAcquisition suppliesAcquisition)
        {
            _suppliesService.Update(suppliesAcquisition);
        }
        
        
        public List<RoomEquipment> FindAllEquipment()
        {
            return this._suppliesService.FindAllEquipment();
        }
        
        public ObservableCollection<SuppliesAcquisition> ReadAllSuppliesAcquisitions()
        {
            return this._suppliesService.ReadAllSuppliesAcquisitions();
        }

        public void FinishSuppliesAcquisition(int acquisitionId)
        {
            this._suppliesService.FinishSuppliesAcquisition(acquisitionId);
        }

        public Equipment CreateNewEquipment(string name)
        {
            return _suppliesService.CreateNewEquipment(name);
        }

        public int GenerateEquipmentId()
        {
            return _suppliesService.GenerateEquipmentId();
        }

        public int GenerateSuppliesAcquistionId()
        {
            return _suppliesService.GenerateSuppliesAcquistionId();
        }

        public void Create(SuppliesAcquisition suppliesAcquisition)
        {
            _suppliesService.Create(suppliesAcquisition);
        }
    }
}