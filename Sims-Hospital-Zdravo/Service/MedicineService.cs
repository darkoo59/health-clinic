using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Repository;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
namespace Sims_Hospital_Zdravo.Service
{
    public class MedicineService
    {
        private MedicineRepository medicineRepository;

        public MedicineService(MedicineRepository medicineRepository)
        {

            this.medicineRepository = medicineRepository;
        }


        public ref ObservableCollection<Medicine> ReadAllMedicine()
        {
           
            return ref medicineRepository.ReadAll();

        }


    }
}
