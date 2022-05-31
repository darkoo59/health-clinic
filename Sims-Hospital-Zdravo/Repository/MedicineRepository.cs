using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler;

namespace Sims_Hospital_Zdravo.Repository
{
    public class MedicineRepository
    {
        private MedicineDataHandler _medicineDataHandler;
        private ObservableCollection<Medicine> _medicines;

        public MedicineRepository(MedicineDataHandler medicineDataHandler)
        {
            this._medicineDataHandler = medicineDataHandler;
            this._medicines = new ObservableCollection<Medicine>();
            LoadDataFromFile();
        }

        public ref ObservableCollection<Medicine> ReadAll()
        {
            return ref this._medicines;
        }

        public void Delete(Medicine medicine)
        {
            _medicines.Remove(medicine);
            LoadDataToFiles();
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

        public void Create(Medicine medicine)
        {
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