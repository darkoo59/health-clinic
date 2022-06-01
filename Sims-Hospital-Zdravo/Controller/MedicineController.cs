using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
using Model;
using Sims_Hospital_Zdravo.Service;

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

        public void ResubmitMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            _medicineService.ResubmitMedicineWithNotifyingDoctor(medicine, notification);
        }

        public void ValidateMedicineWithNotifyindMenager(Medicine medicine, Notification notification)
        {
            _medicineService.ValidateMedicineWithNotifyingDoctor(medicine, notification);
        }
        

        public int GenerateId()
        {
            return _medicineService.GenerateId();
        }

        public void CheckIfPatientAllergicToMedicine(MedicalRecord medicalRecord)
        {
            _medicineService.CheckIfPatientAllergicToMedicine(medicalRecord);
        }
         public void CheckIfPatientAllergicToMedicineIngredients(MedicalRecord medicalRecord)
        {
            _medicineService.CheckIfPatientAllergicToMedicineIngredients(medicalRecord);

        }

        public void ReturnListOfMedicineToStart()
        {
            _medicineService.ReturnListOfMedicinesToStart();
        }
    }
}