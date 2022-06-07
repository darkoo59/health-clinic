using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.ViewModel.Doctor
{
    public class ValidateMedicineViewModel
    {


        public List<Medicine> medicines { get; set; }
        private App app;
        private MedicineController medicineController;
        ValidateMedicineViewModel()
        {
            this.app = App.Current as App;
            this.medicineController = app._medicineController;
            this.medicines = medicineController.ReadAllMedicines().ToList();
        }



    }
}
