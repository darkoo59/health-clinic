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
        private MedicineDataHandler medicineDataHandler;
        private ObservableCollection<Medicine> medicines;

        public MedicineRepository(MedicineDataHandler medicineDataHandler)
        {
            this.medicineDataHandler = medicineDataHandler;
            this.medicines = new ObservableCollection<Medicine>();
            LoadDataFromFile();
        }

        public ref ObservableCollection<Medicine> ReadAll()
        {
            return ref this.medicines;
        }

        public void Delete(Medicine medicine)
        {
            medicines.Remove(medicine);
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
            foreach (Medicine medicine in medicines)
            {
                if (medicine._MedicineStatus == medicineStatus)
                {
                    medicinesByStatus.Add(medicine);
                }
            }

            return medicinesByStatus;
        }

        public Medicine FindById(int id)
        {
            foreach (Medicine medicine in medicines)
            {
                if (medicine._Id == id) return medicine;
            }

            return null;
        }

        public void Create(Medicine medicine)
        {
            medicines.Add(medicine);
            LoadDataToFiles();
        }

        public void LoadDataFromFile()
        {
            this.medicines = medicineDataHandler.ReadAll();
        }

        public void LoadDataToFiles()
        {
            medicineDataHandler.Write(medicines);
        }
    }
}