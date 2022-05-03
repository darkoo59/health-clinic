using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    class MedicineRepository
    {
        MedicineDataHandler medicineDataHandler;
        ObservableCollection<Medicine> medicines;

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
        public void LoadDataFromFile()
        {
            this.medicines = medicineDataHandler.ReadAll();
        }
    }
}
