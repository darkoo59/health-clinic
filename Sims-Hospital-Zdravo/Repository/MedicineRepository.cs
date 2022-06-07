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
            _medicineDataHandler = new MedicineDataHandler();
            _medicines = new List<Medicine>();
        }

        public List<Medicine> FindAll()
        {
            return _medicineDataHandler.ReadAll();
        }

        public void Delete(Medicine medicine)
        {
            LoadDataFromFile();
            _medicines.Remove(medicine);
            LoadDataToFiles();
        }

        public void Update(Medicine medicine)
        {
        }

        public void DeleteById(int id)
        {
            Medicine medicine = FindById(id);
            Delete(medicine);
        }

        public List<Medicine> FindByStatus(MedicineStatus medicineStatus)
        {
            LoadDataFromFile();
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
            LoadDataFromFile();
            foreach (Medicine medicine in _medicines)
            {
                if (medicine.Id == id) return medicine;
            }

            return null;
        }

        public void Create(Medicine medicine)
        {
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