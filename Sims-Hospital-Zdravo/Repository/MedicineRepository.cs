using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler;
using Sims_Hospital_Zdravo.DataHandler;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;

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
            LoadDataFromFiles();
        }


        public  ref ObservableCollection<Medicine> ReadAllMedicines()
        {
            
                Console.WriteLine("je li ovde"+"ahhahahha");
                    
            return  ref this.medicines;
        }


        public void LoadDataFromFiles()
        {
            this.medicines = medicineDataHandler.ReadAll();
            
        }

        public void LoadDataToFiles()
        {
            medicineDataHandler.Write(medicines);
        }

    }
}
