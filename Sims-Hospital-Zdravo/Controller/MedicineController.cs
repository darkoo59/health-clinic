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
        private MedicineService _medicineService;

        public MedicineController(MedicineService medicineService)
        {
            this._medicineService = medicineService;
        }

        public ref ObservableCollection<Medicine> ReadAllMedicines()
        {
            return ref _medicineService.ReadAllMedicine();
        }

        public void CreateMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            _medicineService.CreateMedicineWithNotifyingDoctor(medicine, notification);
        }
    }
}