using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Service;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Controller
{
    public class MedicineController
    {
        private MedicineService medicineService;

        public MedicineController(MedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        public ref ObservableCollection<Medicine> ReadAllMedicines()
        {
            Console.WriteLine("ajdeeee");
            return ref medicineService.ReadAllMedicine();
        }

    }
}
