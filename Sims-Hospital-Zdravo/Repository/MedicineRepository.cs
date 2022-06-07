using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private MedicineDataHandler _medicineDataHandler;
        private List<Medicine> _medicines;

        public MedicineRepository()
        {
            this._medicineDataHandler = new MedicineDataHandler();
            this._medicines = new List<Medicine>();
            
        }

        public  List<Medicine> FindAll()
        {
            return _medicineDataHandler.ReadAll();
        }

        public void Delete(Medicine medicine)
        {
            _medicines.Remove(medicine);
            LoadDataToFiles();
        }
        public void Update(Medicine medicine)
        {
            Medicine medicine1 = FindById(medicine.Id);
            medicine1.Ingredients = medicine.Ingredients;

        }
        public void DeleteById(int id)
        {
            Medicine medicine = FindById(id);
            Delete(medicine);
        }

        public List<Medicine> FindByStatus(MedicineStatus medicineStatus)
        {
            List<Medicine> medicinesByStatus = new List<Medicine>();
            foreach (Medicine medicine in _medicines)
            {
                if (medicine.Status == medicineStatus)
                {
                    medicinesByStatus.Add(medicine);
                }
            }

            return medicinesByStatus;
        }

        public Medicine FindById(int id)
        {
            foreach (Medicine medicine in _medicines)
            {
                if (medicine.Id == id) return medicine;
            }

            return null;
        }

        public void Create(Medicine medicine) {

            LoadDataFromFile();
            _medicines.Add(medicine);
            LoadDataToFiles();
        }

        public void LoadDataFromFile()
        {
            this._medicines = _medicineDataHandler.ReadAll();
        }

        public void LoadDataToFiles()
        {
            _medicineDataHandler.Write(_medicines);
        }
    }
}